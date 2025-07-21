// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using TranslateCmdPal.Util;

namespace TranslateCmdPal;

public partial class TranslateCmdPalCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;
    private readonly SettingsManager _settingsManager = new();


    public TranslateCmdPalCommandsProvider()
    {
        DisplayName = "TranslateCmdPal";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Settings = _settingsManager.Settings;

        _commands = [
            new CommandItem(new TranslateCmdPalPage(_settingsManager)){
                Title = DisplayName,
                MoreCommands =
                [
                    new CommandContextItem(Settings.SettingsPage)
                ]
            }
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }
}
