using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    public partial class DetailCardView : ContentView
    {
        private readonly Note _note;
        private readonly NoteDetailViewModel vm = new();
        public DetailCardView(Note note)
        {
            _note = note;
            var mainViewModel = new MainViewModel();

            Grid importanceGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },  
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
            }.Fill();

            Grid partnerGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
            }.Fill();

            var infosLayoutGrid = new Grid
            {
                Children =
                {
                    new VerticalStackLayout
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            Children =
                            {
                                new DetailRowForLabelOnlyView(vm.TopicLabel, note.Topic, true, false).Fill(),
                                new Divider().Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new DetailRowForLabelOnlyView(vm.TimestampLabel, note.CreatedAt.ToString(), false, true).Fill(),
                                new Divider().Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new DetailRowForLabelOnlyView(vm.LocationLabel, note.Location, false, false).Fill(),
                                new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
                                importanceGrid.Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
                                partnerGrid.Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
                                new DetailRowForLabelOnlyView(vm.DescriptionLabel, note.Description, false, false).Fill()
                            }
                        }.Fill()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            }.Padding(UIStyles.COMMON_PADDINGS_SMALL).Fill();

            Label importanceLabel = new Label { Text = vm.ImportanceLabel }.Start();
            BoxView importanceBoxView = new BoxView
            {
                Color = mainViewModel.PriorityToColor(note.Importance),
                HeightRequest = UIStyles.PRIORITY_BOXVIEW_HEIGHT,
                WidthRequest = UIStyles.PRIORITY_BOXVIEW_WIDTH,
                CornerRadius = UIStyles.PRIORITY_BOXVIEW_CORNER_RADIUS,
                BackgroundColor = Colors.White
            }.End().CenterVertical().Margins(left: UIStyles.COMMON_MARGINS_MEDIUM);

            Label partnerLabel = new Label { Text = vm.Partner }.Start();
            VerticalStackLayout partnerLayout = new VerticalStackLayout
                                    {
                                        new Label { Text = note.PartnerName },
                                        new Label { Text = note.PartnerCompany },
                                        new Label { Text = note.PartnerRole },
                                        new Label { Text = note.Email },
                                        new Label { Text = note.Phone },
                                    }.End();

            importanceGrid.Add(importanceLabel, 0, 0);
            importanceGrid.Add(importanceBoxView, 1, 0);
            partnerGrid.Add(partnerLabel, 0, 0);
            partnerGrid.Add(partnerLayout, 1, 0);

            VerticalStackLayout detailMainLayout = new()
            {
                Children =
                {
                    infosLayoutGrid,
                    new Button { Text = vm.SendNotePerMailButtonLabel }.BackgroundColor(UIStyles.BackgroundLightGrey).TextColor(Colors.Black).Margin(new Thickness(UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_MARGINS_MEDIUM, UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_BUTTON_MARGINS)).Assign(out Button sendPerMailButton),
                    new Button { Text = vm.EditButtonLabel }.BackgroundColor(UIStyles.Primary).TextColor(Colors.White).Margins(top: UIStyles.COMMON_MARGINS_VERY_SMALL).Assign(out Button editButton)
                }
            };

            editButton.Clicked += OnEdit;

            Content = new Frame
            {
                Content = detailMainLayout,
                CornerRadius = UIStyles.FRAME_CORNER_RADIUS,
                Padding = new Thickness(UIStyles.COMMON_PADDINGS_SMALL),
                BackgroundColor = UIStyles.CardColor
            }.Fill();
        }

        private void OnEdit(object? sender, EventArgs e)
        {
            var navigationParams = new Dictionary<string, object>
            {
                { "SelectedElement", _note }
            };
            Shell.Current.GoToAsync(nameof(NoteEditPage), navigationParams);
        }
    }
}
