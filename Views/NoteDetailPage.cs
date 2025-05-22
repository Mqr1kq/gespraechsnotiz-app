using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    [QueryProperty(nameof(SelectedElement), "SelectedElement")]
    public class NoteDetailPage : ContentPage
    {
        private readonly NoteDetailViewModel vm = new();
        private Note _selectedElement;
        public Note SelectedElement
        {
            get => _selectedElement;
            set
            {
                _selectedElement = value;
                LoadDetails();
            }
        }

        public NoteDetailPage() => Title = vm.Title;

        private void LoadDetails()
        {
            Content = _selectedElement == null
                ? new Label
                {
                    Text = vm.ErrorText,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }.Font(size: UIStyles.SIZE_MEDIUM)
                : new ScrollView
                {
                    Content = new VerticalStackLayout
                    {
                        Spacing = UIStyles.COMMON_MARGINS_BIG,
                        Children = { new DetailCardView(_selectedElement) }
                    }.BackgroundColor(UIStyles.BackgroundDarkGrey).Padding(UIStyles.COMMON_PADDINGS_VERY_BIG)
                };
        }
        //public NoteDetailPage(NoteDetailViewModel viewModel)
        //{
        //    Title = "Note";
        //    BindingContext = viewModel;

        //    var topicLabel = new Label { FontAttributes = FontAttributes.Bold };
        //    var topicValue = new Label();
        //    topicValue.SetBinding(Label.TextProperty, "Note.Topic");

        //    var locationLabel = new Label { Text = "Location", FontAttributes = FontAttributes.Bold };
        //    var locationValue = new Label();
        //    locationValue.SetBinding(Label.TextProperty, "Note.Location");

        //    var importanceLabel = new Label { Text = "Importance", FontAttributes = FontAttributes.Bold };
        //    var importanceIndicator = new BoxView
        //    {
        //        WidthRequest = 20,
        //        HeightRequest = 10,
        //        HorizontalOptions = LayoutOptions.Start,
        //        Color = viewModel.Note.IsImportant ? Colors.Red : Colors.Transparent
        //    };

        //    var dateLabel = new Label { Text = "Date & Time", FontAttributes = FontAttributes.Bold };
        //    var dateValue = new Label();
        //    dateValue.SetBinding(Label.TextProperty, new Binding("Note.CreatedAt", stringFormat: "{0:dd.MM.yyyy, HH:mm:ss}"));

        //    var partnerLabel = new Label { Text = "Partner", FontAttributes = FontAttributes.Bold };
        //    var name = new Label();
        //    name.SetBinding(Label.TextProperty, "Note.PartnerName");

        //    var company = new Label();
        //    company.SetBinding(Label.TextProperty, "Note.PartnerCompany");

        //    var role = new Label();
        //    role.SetBinding(Label.TextProperty, "Note.PartnerRole");

        //    var phone = new Label();
        //    phone.SetBinding(Label.TextProperty, "Note.Phone");

        //    var email = new Label();
        //    email.SetBinding(Label.TextProperty, "Note.Email");

        //    var descLabel = new Label { Text = "Description", FontAttributes = FontAttributes.Bold };
        //    var desc = new Label();
        //    desc.SetBinding(Label.TextProperty, "Note.Description");

        //    var sendButton = new Button { Text = "Send per Mail" };
        //    sendButton.Clicked += async (s, e) =>
        //    {
        //        var message = new EmailMessage
        //        {
        //            Subject = viewModel.Note.Topic,
        //            Body = viewModel.Note.Description,
        //            To = new List<string> { viewModel.Note.Email }
        //        };

        //        await Email.Default.ComposeAsync(message);
        //    };

        //    var editButton = new Button
        //    {
        //        Text = "Edit",
        //        BackgroundColor = Colors.Blue,
        //        TextColor = Colors.White
        //    };

        //    Content = new ScrollView
        //    {
        //        Content = new VerticalStackLayout
        //        {
        //            Padding = 20,
        //            Children =
        //            {
        //                topicLabel,
        //                topicValue,
        //                locationLabel,
        //                locationValue,
        //                importanceLabel,
        //                importanceIndicator,
        //                dateLabel,
        //                dateValue,
        //                partnerLabel,
        //                name,
        //                company,
        //                role,
        //                phone,
        //                email,
        //                descLabel,
        //                desc,
        //                sendButton,
        //                editButton
        //            }
        //        }
        //    };
        //}
    }
}
