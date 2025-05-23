using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using MauiIcons.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    public partial class ListElementCardView : ContentView
    {
        private readonly MainViewModel vm = new();
        public event Action<Note>? MessageDeleted;
        public ListElementCardView(Note listElement)
        {
            var deleteIcon = vm.CreateRoundedButtonWithClickEffect(
                icon: MaterialIcons.Delete,
                size: UIStyles.SIZE_VERY_BIG,
                effectColor: Colors.LightGrey,
                marginRight: UIStyles.COMMON_MARGINS_VERY_SMALL,
                marginLeft: UIStyles.COMMON_BUTTON_MARGINS,
                action: () => DeleteMessage(listElement));

            var openDetailPageIcon = vm.CreateRoundedButtonWithClickEffect(
                icon: MaterialIcons.OpenInNew,
                size: UIStyles.SIZE_VERY_BIG,
                effectColor: Colors.LightGrey,
                marginRight: UIStyles.COMMON_BUTTON_MARGINS,
                marginLeft: UIStyles.COMMON_BUTTON_MARGINS,
                action: () => OpenDetailsPage(listElement));

            var cardGridLayout = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(UIStyles.MESSAGE_TYPE_COLOR_LINE_WIDTH) },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                Children =
                {
                    deleteIcon.Column(0).Row(0),

                    new BoxView
                    {
                        Color = UIStyles.Primary,
                        WidthRequest = UIStyles.MESSAGE_TYPE_COLOR_LINE_WIDTH
                    }.Margins(right: UIStyles.COMMON_MARGINS_VERY_SMALL).Column(1).Row(0),

                    new VerticalStackLayout
                    {
                        Children =
                        {
                            new Label { Text = listElement.Topic }.FontSize(size: UIStyles.SIZE_VERY_SMALL).Bold(),
                            new Label { Text = listElement.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss"), LineBreakMode = LineBreakMode.CharacterWrap }.Font(size: UIStyles.SIZE_VERY_SMALL),
                            new Label { Text = listElement.PartnerName, LineBreakMode = LineBreakMode.CharacterWrap }.Font(size: UIStyles.SIZE_VERY_SMALL),
                            new Label { Text = listElement.Location, LineBreakMode = LineBreakMode.CharacterWrap }.Font(size: UIStyles.SIZE_VERY_SMALL),
                            new HorizontalStackLayout
                            {
                                new Label { Text = vm.ImportanceLabel }.Font(size: UIStyles.SIZE_VERY_SMALL),
                                new BoxView
                                {
                                        Color = vm.PriorityToColor(listElement.Importance),
                                        HeightRequest = UIStyles.PRIORITY_BOXVIEW_HEIGHT,
                                        WidthRequest = UIStyles.PRIORITY_BOXVIEW_WIDTH,
                                        CornerRadius = UIStyles.PRIORITY_BOXVIEW_CORNER_RADIUS,
                                        BackgroundColor = Colors.White
                                }.CenterHorizontal().CenterVertical().Margins(left: UIStyles.COMMON_MARGINS_MEDIUM).Assign(out BoxView importanceView),
                                new Label { Text = listElement.Importance.ToString(), IsVisible = false }.Font(size: UIStyles.SIZE_VERY_SMALL).Margins(left: UIStyles.COMMON_MARGINS_VERY_SMALL).Assign(out Label tooltip)
                            },
                        }
                    }.Margins(left: UIStyles.COMMON_MARGINS_VERY_SMALL).Column(2).Row(0),
                    openDetailPageIcon.Column(3).Row(0)
                }
            }.Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                tooltip.IsVisible = !tooltip.IsVisible;
            };
            importanceView.GestureRecognizers.Add(tapGesture);

            Content = new Frame
            {
                Content = cardGridLayout,
                CornerRadius = UIStyles.FRAME_CORNER_RADIUS,
                Padding = new Thickness(UIStyles.COMMON_PADDINGS_SMALL),
                BackgroundColor = UIStyles.CardColor
            };
        }

        private async void DeleteMessage(Note message)
        {
            bool answer = await App.Current?.MainPage?.DisplayAlert(vm.DeleteLabel, vm.DeleteMessageDescription, vm.YesLabel, vm.NoLabel)!;
            if (answer == true) MessageDeleted?.Invoke(message);
        }

        private void OpenDetailsPage(Note message)
        {
            var navigationParams = new Dictionary<string, object>
            {
                { "SelectedElement", message }
            };

            Shell.Current.GoToAsync(nameof(NoteDetailPage), navigationParams);
        }

    }
}
