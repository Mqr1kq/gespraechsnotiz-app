using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    public partial class DetailCardView : ContentView
    {
        private Entry _emailEntry;
        private Button _confirmSendButton;
        private Button _cancelSendButton;
        private Frame _sendPerMailCard;

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
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
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
                                new DetailRowForLabelOnlyView(vm.TimestampLabel, note.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss"), false, true).Fill(),
                                new Divider().Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new DetailRowForLabelOnlyView(vm.LocationLabel, note.Location, false, false).Fill(),
                                new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
                                importanceGrid.Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
                                partnerGrid.Paddings(top: UIStyles.COMMON_PADDINGS_SMALL, bottom: UIStyles.COMMON_PADDINGS_SMALL),
                                note.Description == null ? new HorizontalStackLayout{ IsVisible = false } : new Divider().Paddings(top : UIStyles.COMMON_PADDINGS_SMALL, bottom : UIStyles.COMMON_PADDINGS_SMALL),
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
            Button importanceBoxView = new Button
            {
                BackgroundColor = mainViewModel.PriorityToColor(note.Importance),
                HeightRequest = UIStyles.PRIORITY_BOXVIEW_HEIGHT,
                WidthRequest = UIStyles.PRIORITY_BOXVIEW_WIDTH,
                CornerRadius = UIStyles.PRIORITY_BOXVIEW_CORNER_RADIUS,
            }.End().CenterVertical().Margins(left: UIStyles.COMMON_MARGINS_MEDIUM);
            Label tooltip = new Label { Text = note.Importance.ToString(), IsVisible = false }.Font(size: UIStyles.SIZE_VERY_SMALL).Margins(left: UIStyles.COMMON_MARGINS_VERY_SMALL);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                tooltip.IsVisible = !tooltip.IsVisible;
            };
            importanceBoxView.GestureRecognizers.Add(tapGesture);

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
            importanceGrid.Add(tooltip, 2, 0);
            partnerGrid.Add(partnerLabel, 0, 0);
            partnerGrid.Add(partnerLayout, 1, 0);

            VerticalStackLayout detailMainLayout = new VerticalStackLayout()
            {
                Children =
                {
                    infosLayoutGrid,
                    new Button { Text = vm.SendNotePerMailButtonLabel }.BackgroundColor(UIStyles.BackgroundDarkGrey).TextColor(Colors.Black).Margin(new Thickness(UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_MARGINS_MEDIUM, UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_BUTTON_MARGINS)).Assign(out Button sendPerMailButton),
                    new Button { Text = vm.EditButtonLabel }.BackgroundColor(UIStyles.Primary).TextColor(Colors.White).Margins(top: UIStyles.COMMON_MARGINS_VERY_SMALL).Assign(out Button editButton)
                }
            };

            editButton.Clicked += OnEdit;
            sendPerMailButton.Clicked += OnSendPerMail;

            _emailEntry = new Entry { Keyboard = Keyboard.Email }.Placeholder(vm.EmailReceiverLabel).BackgroundColor(Colors.White).TextColor(Colors.Black).Margin(UIStyles.COMMON_PADDINGS_SMALL);
            _confirmSendButton = new Button { Text = vm.SendButtonLabel }.BackgroundColor(UIStyles.Primary).TextColor(Colors.White);
            _cancelSendButton = new Button { Text = vm.CancelButtonLabel }.BackgroundColor(UIStyles.BackgroundDarkGrey).TextColor(Colors.Black);

            var emailInputLayout = new VerticalStackLayout
            {
                Spacing = UIStyles.COMMON_MARGINS_VERY_SMALL,
                Children = { _emailEntry, _confirmSendButton, _cancelSendButton },
            };

            _sendPerMailCard = new Frame
            {
                IsVisible = false,
                Content = emailInputLayout,
                CornerRadius = UIStyles.FRAME_CORNER_RADIUS,
                Padding = new Thickness(UIStyles.COMMON_PADDINGS_SMALL),
                BackgroundColor = UIStyles.CardColor
            }.Fill();

            _confirmSendButton.Clicked += OnSendNoteEmail;
            _cancelSendButton.Clicked += (s, e) => _sendPerMailCard.IsVisible = false;


            Content = new VerticalStackLayout
            {
                Spacing = UIStyles.COMMON_MARGINS_SMALL,
                Children = {
                    new Frame
                    {
                        Content = detailMainLayout,
                        CornerRadius = UIStyles.FRAME_CORNER_RADIUS,
                        Padding = new Thickness(UIStyles.COMMON_PADDINGS_SMALL),
                        BackgroundColor = UIStyles.CardColor
                    }.Fill(),
                    _sendPerMailCard
                }
            };
        }

        private void OnSendPerMail(object? sender, EventArgs e)
        {
            _sendPerMailCard.IsVisible = true;
        }

        private async void OnSendNoteEmail(object? sender, EventArgs e)
        {
            string email = _emailEntry.Text?.Trim();

            if (!ValidateEmail())
                return;

            string subject = Uri.EscapeDataString($"Gesprächsnotiz: {_note.Topic}");
            string body = Uri.EscapeDataString(
                $"Thema: {_note.Topic}\n" +
                $"Zeitpunkt: {_note.CreatedAt:dd.MM.yyyy HH:mm:ss}\n" +
                $"Ort: {_note.Location}\n" +
                $"Wichtigkeit: {_note.Importance}\n\n" +
                $"Gesprächspartner:\n" +
                $"- Name: {_note.PartnerName}\n" +
                $"- Unternehmen: {_note.PartnerCompany}\n" +
                $"- Rolle: {_note.PartnerRole}\n" +
                $"- Telefon: {_note.Phone}\n" +
                $"- E-Mail: {_note.Email}\n\n" +
                $"Notiz:\n{_note.Description}"
            );

            string mailtoUri = $"mailto:{email}?subject={subject}&body={body}";

            try
            {
                await Launcher.Default.OpenAsync(mailtoUri);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(vm.ErrorLabel, $"E-Mail konnte nicht geöffnet werden: {ex.Message}", vm.OKLabel);
            }

            _sendPerMailCard.IsVisible = false;
        }

        private bool ValidateEmail()
        {
            var email = _emailEntry.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(email) && !System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Application.Current.MainPage.DisplayAlert(vm.ErrorLabel, vm.EmailErrorDescription, vm.OKLabel);
                return false;
            }
            return true;
        }

        private void OnEdit(object? sender, EventArgs e)
        {
            string serializedNote = Uri.EscapeDataString(JsonSerializer.Serialize(_note));
            Shell.Current.GoToAsync($"{nameof(NoteEditPage)}?SelectedNoteJson={serializedNote}");
        }
    }
}
