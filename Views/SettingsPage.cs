using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.Services;
using Gespraechsnotiz_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gespraechsnotiz_App.Views
{
    public class SettingsPage : ContentPage
    {
        private readonly NoteSyncService _syncService;
        private readonly Switch _syncSwitch;

        public SettingsPage(NoteSyncService syncService)
        {
            var vm = new SettingsViewModel();
            _syncService = syncService;

            Title = vm.Title;

            _syncSwitch = new Switch
            {
                IsToggled = Preferences.Get("SyncEnabled", false),
                ThumbColor = UIStyles.Primary,
                OnColor = UIStyles.LightPrimary
            };

            _syncSwitch.Toggled += (s, e) =>
                Preferences.Set("SyncEnabled", e.Value);

            Content = new VerticalStackLayout
            {
                Padding = UIStyles.COMMON_PADDINGS_MEDIUM,
                Spacing = UIStyles.COMMON_PADDINGS_VERY_BIG,
                Children =
                {
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label { Text = vm.SyncSwitchLabel }.CenterVertical().Font(size: UIStyles.SIZE_SMALL),
                            _syncSwitch.CenterVertical().Margins(left: UIStyles.COMMON_MARGINS_MEDIUM),
                        }
                    }.Fill().Paddings(left: UIStyles.COMMON_PADDINGS_SMALLEST),
                    new Button
                    {
                        Text = vm.SyncButtonLabel,
                        Command = new Command(async () =>
                        {
                            await _syncService.UploadAllNotesAsync();
                            await DisplayAlert(vm.SuccessLabel, vm.SuccessDescription, vm.OKLabel);
                        })
                    }.BackgroundColor(UIStyles.Primary)
                }
            };
        }
    }
}
