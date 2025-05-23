using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using Gespraechsnotiz_App.Views;
using MauiIcons.Core;
using MauiIcons.Material;
using System.Text.Json;

namespace Gespraechsnotiz_App
{
    public partial class MainPage : ContentPage, IQueryAttributable
    {
        private readonly MainViewModel vm = new();
        private VerticalStackLayout _outerVerticalStackLayout;
        private List<Note> _notesList;
        private HorizontalStackLayout _createNoteLayout;

        public MainPage()
        {
            Title = vm.Title;
            _notesList = LoadSavedElements();
            Content = BuildUI();

            NoteEvents.NoteListChanged += OnNoteListChanged;
        }

        private View BuildUI()
        {
            //FillListWithFirstItem();
            _outerVerticalStackLayout = new VerticalStackLayout
            {
                Children =
                {
                    new HorizontalStackLayout
                    {
                        vm.CreateRoundedButtonWithClickEffect(
                            icon: MaterialIcons.NoteAdd,
                            size: UIStyles.SIZE_VERY_BIG,
                            effectColor: Colors.LightGrey,
                            marginRight: UIStyles.COMMON_MARGINS_VERY_SMALL,
                            marginLeft: UIStyles.COMMON_BUTTON_MARGINS,
                            action: () => OnCreateNote()),
                        new Label { Text = vm.CreateNewNoteLabel }.CenterVertical().CenterHorizontal().Font(size: UIStyles.SIZE_VERY_SMALL)
                    }.Assign(out HorizontalStackLayout createNoteLayout)
                }
            }.BackgroundColor(Colors.LightGray).Paddings(UIStyles.COMMON_PADDINGS_VERY_BIG, UIStyles.COMMON_PADDINGS_MEDIUM, UIStyles.COMMON_PADDINGS_VERY_BIG, UIStyles.COMMON_PADDINGS_VERY_BIG);

            _createNoteLayout = createNoteLayout;
            RefreshListDisplay();

            return new ScrollView { Content = _outerVerticalStackLayout, BackgroundColor = UIStyles.BackgroundDarkGrey };
        }

        private void SaveMessageToLocalStorage(Note message)
        {
            string jsonElement = JsonSerializer.Serialize(message);
            Preferences.Set($"ListElement_{message.Id}", jsonElement);

            var savedKeys = Preferences.Get("SavedListKeys", string.Empty);
            var keys = savedKeys.Split(',').Where(key => !string.IsNullOrWhiteSpace(key)).ToList();

            if (!keys.Contains($"ListElement_{message.Id}"))
            {
                keys.Add($"ListElement_{message.Id}");
                Preferences.Set("SavedListKeys", string.Join(",", keys));
            }
        }

        private int GetNextElementId()
        {
            int lastId = Preferences.Get("LastElementId", 0);
            int newId = lastId + 1;
            Preferences.Set("LastElementId", newId);
            return newId;
        }

        //private void FillListWithFirstItem()
        //{
        //    Note newNote = new Note
        //    {
        //        Id = GetNextElementId(),
        //        Topic = "Meeting mit Kunde A",
        //        Location = "BBS Meppen",
        //        PartnerName = "Hannah Lamp",
        //        PartnerCompany = "Firma GmbH",
        //        PartnerRole = "Sekretärin",
        //        Phone = "0123456789",
        //        Email = "hl@gmail.com",
        //        Description = "Kurze Notiz...",
        //        Importance = Importance.Low
        //    };
        //    _notesList.Add(newNote);
        //    SaveMessageToLocalStorage(newNote);
        //}

        private List<Note> LoadSavedElements()
        {
            var savedMessages = new List<Note>();
            var savedKeys = Preferences.Get("SavedListKeys", string.Empty);
            var keys = savedKeys.Split(',').Where(key => !string.IsNullOrWhiteSpace(key));

            foreach (var key in keys)
            {
                var jsonElement = Preferences.Get(key, string.Empty);
                if (!string.IsNullOrWhiteSpace(jsonElement))
                {
                    var message = JsonSerializer.Deserialize<Note>(jsonElement);
                    if (message != null) savedMessages.Add(message);
                }
            }
            return savedMessages;
        }

        private void RefreshListDisplay()
        {
            if (_outerVerticalStackLayout != null) _outerVerticalStackLayout.Children.Clear();
            _outerVerticalStackLayout?.Children.Add(_createNoteLayout);
            foreach (var message in _notesList)
            {
                var messageCard = new ListElementCardView(message);
                messageCard.MessageDeleted += OnMessageDeleted;
                _outerVerticalStackLayout?.Children.Add(new VerticalStackLayout
                {
                    Children = { messageCard },
                }.Margins(bottom: UIStyles.COMMON_MARGINS_BIG));
            }
        }

        private async void OnCreateNote() => await Shell.Current.GoToAsync(nameof(NoteEditPage));

        private void OnMessageDeleted(Note message)
        {
            _notesList.Remove(message);
            Preferences.Remove($"ListElement_{message.Id}");

            var savedKeys = Preferences.Get("SavedListKeys", "");
            var keys = savedKeys.Split(',').Where(key => !string.IsNullOrWhiteSpace(key));

            foreach (var key in keys)
            {
                if (key == $"ListElement_{message.Id}") key.Remove(0);
            }
            RefreshListDisplay();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Reload", out var reloadValue) && reloadValue?.ToString() == "true")
            {
                _notesList = LoadSavedElements();
                RefreshListDisplay();
            }
        }

        private void OnNoteListChanged(object sender, EventArgs e)
        {
            _notesList = LoadSavedElements();
            RefreshListDisplay();
        }
    }
}
