using TranslateCmdPal.Model;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Threading.Tasks;

namespace TranslateCmdPal.Util
{

    public partial class ResultCopyCommand : InvokableCommand
    {
        private readonly SettingsManager _settingsManager;

        public TranslationEntity Arguments { get; set; }

        public ResultCopyCommand(TranslationEntity arguments, SettingsManager settingsManager)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments), "TranslationResult cannot be null");
            }

            if (settingsManager == null)
            {
                throw new ArgumentNullException(nameof(settingsManager), "SettingsManager cannot be null");
            }

            Arguments = arguments;

            _settingsManager = settingsManager;

            Name = "Copy";
        }

        public override CommandResult Invoke()
        {
            var task = ExecuteAsync();
            task.Wait();

            return CommandResult.Hide();
        }

        private async Task ExecuteAsync()
        {
            ClipboardHelper.SetText(Arguments.TranslatedText);

            if (_settingsManager.ShowHistory != Properties.Resource.history_none)
            {
                _settingsManager.SaveHistory(Arguments);
            }
        }
    }
}
