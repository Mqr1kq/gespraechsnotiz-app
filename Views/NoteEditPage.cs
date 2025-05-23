using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.Services;
using Gespraechsnotiz_App.ViewModels;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    [QueryProperty(nameof(SelectedNoteJson), "SelectedNoteJson")]
    public class NoteEditPage : ContentPage
    {
        private bool _isEditMode;
        private Note _note;
        private NoteEditViewModel vm = new NoteEditViewModel();

        public string SelectedNoteJson
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _note = JsonSerializer.Deserialize<Note>(Uri.UnescapeDataString(value));
                    _isEditMode = true;
                    Title = new NoteEditViewModel().TitleEdit;
                    LoadNoteData();
                }
            }
        }

        private readonly Entry _topicEntry;
        private readonly Entry _partnerNameEntry;
        private readonly Entry _partnerCompanyEntry;
        private readonly Entry _partnerRoleEntry;
        private readonly Entry _phoneEntry;
        private readonly Entry _emailEntry;
        private readonly Editor _descriptionEditor;
        private readonly Picker _importancePicker;

        public NoteEditPage()
        {
            BackgroundColor = UIStyles.BackgroundDarkGrey;
            Padding = UIStyles.COMMON_PADDINGS_VERY_BIG;
            _note = new Note();
            Title = vm.TitleCreate;

            _topicEntry = new Entry().Placeholder(vm.TopicLabel);
            _partnerNameEntry = new Entry().Placeholder(vm.PartnerNameLabel);
            _partnerCompanyEntry = new Entry().Placeholder(vm.PartnerCompanyLabel);
            _partnerRoleEntry = new Entry().Placeholder(vm.PartnerRoleLabel);
            _phoneEntry = new Entry().Placeholder(vm.PhoneLabel);
            _emailEntry = new Entry().Placeholder(vm.EmailLabel);
            _descriptionEditor = new Editor { AutoSize = EditorAutoSizeOption.TextChanges }.Placeholder(vm.DescriptionLabel);
            _importancePicker = new Picker { Title = vm.ImportanceLabel, ItemsSource = Enum.GetNames(typeof(Importance)).ToList() };

            // Input restrictions
            _emailEntry.TextChanged += (s, e) =>
            {
                var entry = s as Entry;
                if (entry != null)
                {
                    string allowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.@_-";
                    entry.Text = new string(entry.Text.Where(c => allowed.Contains(c)).ToArray());
                }
            };

            _phoneEntry.TextChanged += (s, e) =>
            {
                var entry = s as Entry;
                if (entry != null)
                {
                    entry.Text = new string(entry.Text.Where(c => char.IsDigit(c) || c == '+').ToArray());
                }
            };

            var saveButton = new Button { Text = vm.SaveButtonLabel }.BackgroundColor(UIStyles.Primary).TextColor(Colors.White);
            var cancelButton = new Button { Text = vm.CancelButtonLabel }.BackgroundColor(UIStyles.BackgroundDarkGrey).TextColor(Colors.Black);
            saveButton.Clicked += OnSaveClicked!;
            cancelButton.Clicked += OnCancelClicked!;

            Content = new ScrollView
            {
                Content = new ContentView
                {
                    Content = new Frame
                    {
                        BackgroundColor = UIStyles.CardColor,
                        CornerRadius = UIStyles.FRAME_CORNER_RADIUS,
                        Padding = UIStyles.COMMON_PADDINGS_MEDIUM,
                        Content = new VerticalStackLayout
                        {
                            Spacing = UIStyles.COMMON_MARGINS_SMALL,
                            Children =
                            {
                                _topicEntry,
                                _partnerNameEntry,
                                _partnerCompanyEntry,
                                _partnerRoleEntry,
                                _phoneEntry,
                                _emailEntry,
                                _importancePicker,
                                _descriptionEditor,
                                saveButton,
                                cancelButton
                            }
                        }
                    }
                }
            };
        }

        private void LoadNoteData()
        {
            _topicEntry.Text = _note.Topic;
            _partnerNameEntry.Text = _note.PartnerName;
            _partnerCompanyEntry.Text = _note.PartnerCompany;
            _partnerRoleEntry.Text = _note.PartnerRole;
            _phoneEntry.Text = _note.Phone;
            _emailEntry.Text = _note.Email;
            _descriptionEditor.Text = _note.Description;
            _importancePicker.SelectedItem = _note.Importance.ToString();
        }

        private void OnCancelClicked(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            _note.Topic = _topicEntry.Text;
            _note.Location = await GetLocationAsync();
            _note.PartnerName = _partnerNameEntry.Text;
            _note.PartnerCompany = _partnerCompanyEntry.Text;
            _note.PartnerRole = _partnerRoleEntry.Text;
            _note.Phone = _phoneEntry.Text;
            _note.Email = _emailEntry.Text;
            _note.Description = _descriptionEditor.Text;
            _note.Importance = Enum.TryParse(_importancePicker.SelectedItem?.ToString(), out Importance imp) ? imp : Importance.Mittel;

            if (!_isEditMode)
            {
                _note.Id = Preferences.Get("LastElementId", 0) + 1;
                Preferences.Set("LastElementId", _note.Id);
            }

            string json = JsonSerializer.Serialize(_note);
            Preferences.Set($"ListElement_{_note.Id}", json);

            if (Preferences.Get("SyncEnabled", false))
            {
                var syncService = new NoteSyncService();
                await syncService.UploadNoteAsync(_note);
            }

            var savedKeys = Preferences.Get("SavedListKeys", "");
            var keys = savedKeys.Split(',').Where(k => !string.IsNullOrWhiteSpace(k)).ToList();
            if (!keys.Contains($"ListElement_{_note.Id}"))
            {
                keys.Add($"ListElement_{_note.Id}");
                Preferences.Set("SavedListKeys", string.Join(",", keys));
            }

            NoteEvents.RaiseNoteListChanged();
            await Shell.Current.GoToAsync("..");
        }

        private async Task<string> GetLocationAsync()
        {
            string result = "";
            try
            {
                var geolocation = await Geolocation.GetLastKnownLocationAsync();

                if (geolocation == null)
                {
                    geolocation = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }
                if (geolocation != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(geolocation);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                       result = $"{placemark.Thoroughfare} {placemark.SubThoroughfare}, {placemark.PostalCode} {placemark.Locality}";
                    }
                    else
                    {
                        result = vm.AddressNotFoundErrorDescription;
                    }
                }
            }
            catch (Exception ex)
            {
                result = vm.GeolocationNotFoundErrorDescription;
            }

            return result;
        }

        private bool ValidateInputs()
        {
            // Topic required
            if (string.IsNullOrWhiteSpace(_topicEntry.Text))
            {
                DisplayAlert(vm.ErrorLabel, vm.TopicErrorDescription, vm.OKLabel);
                return false;
            }

            // Email validation
            var email = _emailEntry.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(email) && !System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                DisplayAlert(vm.ErrorLabel, vm.EmailErrorDescription, vm.OKLabel);
                return false;
            }

            // Phone number validation
            var phone = _phoneEntry.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(phone) && !System.Text.RegularExpressions.Regex.IsMatch(phone,
                @"^\+?[0-9]{6,15}$"))
            {
                DisplayAlert(vm.ErrorLabel, vm.PhoneErrorDescription, vm.OKLabel);
                return false;
            }

            return true;
        }

    }
}
