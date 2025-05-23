using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.ViewModels;
using Gespraechsnotiz_App.Views;
using MauiIcons.Material;

namespace Gespraechsnotiz_App
{
    public partial class AppShell : Shell
    {
        private readonly Label TitleLabel;
        private View SettingsIcon;
        public AppShell()
        {
            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
            Routing.RegisterRoute(nameof(NoteEditPage), typeof(NoteEditPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            FlyoutBehavior = FlyoutBehavior.Disabled;
            Title = "Gesprächsnotiz-App";

            var mainViewModel = new MainViewModel();

            TitleLabel = new Label { Text = "Notizen", WidthRequest = UIStyles.PAGE_TITLE_LAYOUT_WIDTH }.Start().CenterVertical().Font(size: 20).Bold().TextColor(Colors.Black);

            SettingsIcon = mainViewModel.CreateRoundedButtonWithClickEffect(
                icon: MaterialIcons.Settings,
                size: UIStyles.SIZE_BIG,
                effectColor: Colors.LightGrey,
                marginRight: UIStyles.COMMON_BUTTON_MARGINS,
                marginLeft: UIStyles.COMMON_BUTTON_MARGINS,
                action: () => OnOpenSettings());
            SettingsIcon.HorizontalOptions = LayoutOptions.End;

            var headerBar = new Grid
            {
                ColumnSpacing = UIStyles.COMMON_MARGINS_SMALL,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
            }.FillHorizontal();

            headerBar.Add(TitleLabel, 0, 0);
            headerBar.Add(SettingsIcon, 1, 0);

            SetNavBarHasShadow(this, true);
            SetTitleView(this, headerBar);

            Items.Add(new ShellContent
            {
                Title = "Notizen",
                ContentTemplate = new DataTemplate(() => new MainPage()),
                Route = nameof(MainPage),
            });
            Items.Add(new ShellContent
            {
                Title = "Details",
                ContentTemplate = new DataTemplate(() => new NoteDetailPage()),
                Route = nameof(NoteDetailPage),
            });
            Items.Add(new ShellContent
            {
                Title = "Notiz bearbeiten",
                ContentTemplate = new DataTemplate(() => new NoteEditPage()),
                Route = nameof(NoteEditPage),
            });
            Items.Add(new ShellContent
            {
                Title = "Einstellungen",
                ContentTemplate = new DataTemplate(() => new SettingsPage(null)),
                Route = nameof(SettingsPage),
            });

            Navigated += OnNavigated!;
        }

        private void OnOpenSettings()
        {
            Current.GoToAsync(nameof(SettingsPage));
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            var currentPage = CurrentPage;
            if (currentPage != null)
            {
                TitleLabel.Text = currentPage.Title; //updating title dynamically

                if (currentPage.Title == "Einstellungen")
                    SettingsIcon.IsVisible = false;
                else
                    SettingsIcon.IsVisible = true;
            }
        }
    }
}
