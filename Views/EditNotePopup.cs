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
    public partial class EditNotePopup : Popup
    {
        private readonly Entry _topicEntry;
        private readonly Entry _timestampEntry;
        private readonly Entry _locationEntry;
        private readonly Entry _nameEntry;
        private readonly Picker _importanceEntry;
        private readonly Entry _descriptionEntry;
        private readonly Entry _companyEntry;
        private readonly Entry _roleEntry;
        private readonly Entry _emailPicker;
        private readonly Entry _phonePicker;
        public event EventHandler<Note> OnEdited;
        public EditNotePopup(Note note)
        {
            var vm = new MainViewModel();

            //_topicEntry = new Entry { Text = currentFilterCriteria.ServerName ?? string.Empty }.Placeholder(vm.ServerNameLabel);
            //_errorTypeEntry = new Entry { Text = currentFilterCriteria.ErrorType ?? string.Empty }.Placeholder(vm.ErrorTypeLabel);
            //_subjectEntry = new Entry { Text = currentFilterCriteria.Subject ?? string.Empty }.Placeholder(vm.SubjectLabel);
            //_timestampEntry = new Entry { Text = currentFilterCriteria.Timestamp ?? string.Empty }.Placeholder(vm.TimestampLabel);
            //_messageTypePicker = new Picker()
            //{
            //    Items = { vm.MessageTypeErrorLabel, vm.MessageTypeInfoLabel, vm.ListAllLabel },
            //    Title = vm.MessageTypeLabel,
            //    SelectedIndex = currentFilterCriteria.MessageType == MessageTypeFilter.Fehlernachrichten ? 0 :
            //                    currentFilterCriteria.MessageType == MessageTypeFilter.Informationsnachrichten ? 1 : 2
            //};
            //_statusPicker = new Picker()
            //{
            //    Items = { vm.StatusReadLabel, vm.StatusUnreadLabel, vm.ListAllLabel },
            //    Title = vm.StatusLabel,
            //    SelectedIndex = currentFilterCriteria.Status == Status.Gelesen ? 0 : currentFilterCriteria.Status == Status.Ungelesen ? 1 : 2
            //};

            //Content = new VerticalStackLayout
            //{
            //    WidthRequest = UIStyles.FILTER_POPUP_WIDTH,
            //    Spacing = UIStyles.COMMON_MARGINS_SMALL,
            //    Children =
            //    {
            //        new Label { Text = vm.FilterLabel }.Font(size: UIStyles.SIZE_SMALL).Bold(),
            //        _serverNameEntry,
            //        _errorTypeEntry,
            //        _subjectEntry,
            //        _timestampEntry,
            //        _messageTypePicker,
            //        _statusPicker,
            //        new Button { Text = vm.FilterButtonLabel }
            //        .Margin(new Thickness(UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_MARGINS_MEDIUM, UIStyles.COMMON_BUTTON_MARGINS, UIStyles.COMMON_BUTTON_MARGINS))
            //        .Assign(out Button editButton)
            //    }
            //}
            //.Paddings(UIStyles.COMMON_PADDINGS_BIG, UIStyles.COMMON_PADDINGS_MEDIUM, UIStyles.COMMON_PADDINGS_BIG, UIStyles.COMMON_PADDINGS_MEDIUM)
            //.BackgroundColor(UIStyles.CardColor);

            //editButton.Clicked += OnEditButtonClicked!;
        }

        //private void OnEditButtonClicked(object sender, EventArgs e)
        //{
        //    var selectedStatus = (Status)Enum.Parse(typeof(Status), _statusPicker.SelectedItem.ToString()!);
        //    var selectedMessageType = (MessageTypeFilter)Enum.Parse(typeof(MessageTypeFilter), _messageTypePicker.SelectedItem.ToString()!);
        //    FilterApplied?.Invoke(this, new FilterCriteriaEventArgs(
        //        _serverNameEntry.Text,
        //        _errorTypeEntry.Text,
        //        _subjectEntry.Text,
        //        _timestampEntry.Text,
        //        selectedMessageType,
        //        selectedStatus));
        //    Close();
        //}
    }
}
