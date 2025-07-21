// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using TranslateCmdPal.DTO;
using TranslateCmdPal.Enum;
using TranslateCmdPal.Job;
using TranslateCmdPal.Model;
using TranslateCmdPal.Util;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslateCmdPal;

internal sealed partial class TranslateCmdPalPage : DynamicListPage, IDisposable
{
    private List<ListItem> _allItems;
    private readonly SettingsManager _settingsManager;
    private static Task<TranslationResultDTO> translationTask = null;

    public TranslateCmdPalPage(SettingsManager settingsManager)
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "TranslateCmdPal";
        Name = "Open";
        _settingsManager = settingsManager;
        _allItems = _settingsManager.LoadHistory();
    }

    public override async void UpdateSearchText(string oldSearch, string newSearch)
    {
        if (string.IsNullOrWhiteSpace(newSearch))
        {
            _allItems = _settingsManager.LoadHistory();
            RaiseItemsChanged(_allItems.Count);
            return;
        }

        if (newSearch == oldSearch)
        {
            return;
        }

        if (translationTask != null && !translationTask.IsCompleted)
        {
            return;
        }

        var (targetCode, text) = InputInterpreter.Parse(newSearch, LangCode.Parse(_settingsManager.DefaultTargetLang));

        try
        {
            translationTask = JobHttp.Translation(targetCode, text, _settingsManager.DeepLAPIKey);
            var result = await translationTask;

            _allItems.Clear();
            foreach (var item in result.Translations)
            {
                var translation = new TranslationEntity
                {
                    OriginalText = text,
                    OriginalLangCode = item.DetectedSourceLanguage,
                    TranslatedText = item.Text.TrimStart(),
                    TargetLangCode = result.TargetLangCode,
                    Timestamp = DateTime.Now
                };

                _allItems.Add(new ListItem(new ResultCopyCommand(translation, _settingsManager))
                {
                    Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png"),
                    Title = translation.TranslatedText,
                    Subtitle = translation.OriginalText,
                    Tags = [new Tag($"{translation.OriginalLangCode} -> {translation.TargetLangCode}")],
                });
            }
            RaiseItemsChanged(_allItems.Count);
        }
        catch (Exception)
        {
            _allItems.Clear();
            RaiseItemsChanged(_allItems.Count);
        }
    }

    public override IListItem[] GetItems()
    {
        return [.. _allItems];
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}