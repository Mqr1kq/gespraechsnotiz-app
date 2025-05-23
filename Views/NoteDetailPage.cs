using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.Models;
using Gespraechsnotiz_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_selectedElement != null)
            {
                // Reload the note from storage using the ID
                var updatedNote = LoadNoteById(_selectedElement.Id);
                if (updatedNote != null)
                {
                    _selectedElement = updatedNote;
                    LoadDetails();
                }
            }
        }

        private Note LoadNoteById(int id)
        {
            var savedKeys = Preferences.Get("SavedListKeys", string.Empty);
            var keys = savedKeys.Split(',').Where(key => !string.IsNullOrWhiteSpace(key));

            foreach (var key in keys)
            {
                var jsonElement = Preferences.Get(key, string.Empty);
                if (!string.IsNullOrWhiteSpace(jsonElement))
                {
                    var note = JsonSerializer.Deserialize<Note>(jsonElement);
                    if (note != null && note.Id == id)
                        return note;
                }
            }
            return null;
        }
    }
}
