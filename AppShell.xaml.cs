using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.ViewModels;
using Gespraechsnotiz_App.Views;
using MauiIcons.Material;

namespace Gespraechsnotiz_App
{
    public partial class AppShell : Shell
    {
        private readonly Label TitleLabel;
        public AppShell()
        {
            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
            Routing.RegisterRoute(nameof(NoteEditPage), typeof(NoteEditPage));
            FlyoutBehavior = FlyoutBehavior.Disabled;
            Title = "Gesprächsnotiz-App";

            var mainViewModel = new MainViewModel();

            TitleLabel = new Label { Text = "Notizen", WidthRequest = UIStyles.PAGE_TITLE_LAYOUT_WIDTH }.Start().CenterVertical().Font(size: 20).Bold().TextColor(Colors.Black);

            var settingsIcon = mainViewModel.CreateRoundedButtonWithClickEffect(
                icon: MaterialIcons.Settings,
                size: UIStyles.SIZE_BIG,
                effectColor: Colors.LightGrey,
                marginRight: UIStyles.COMMON_BUTTON_MARGINS,
                marginLeft: UIStyles.COMMON_BUTTON_MARGINS,
                action: () => OnOpenSettings());
            settingsIcon.HorizontalOptions = LayoutOptions.End;

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
            headerBar.Add(settingsIcon, 1, 0);

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

            Navigated += OnNavigated!;
        }

        private void OnOpenSettings()
        {
            Current.GoToAsync(nameof(NoteDetailPage));
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            var currentPage = CurrentPage;
            if (currentPage != null)
            {
                TitleLabel.Text = currentPage.Title; //updating title dynamically
            }
        }
    }
}
