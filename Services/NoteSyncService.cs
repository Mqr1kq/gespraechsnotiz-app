using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Services
{
    public class NoteSyncService
    {
        private readonly HttpClient _httpClient = new();

        private const string FirebaseUrl = "https://gespraechsnotiz-app-default-rtdb.europe-west1.firebasedatabase.app/notes.json";

        public async Task UploadAllNotesAsync()
        {
            var notes = LoadSavedNotes();
            foreach (var note in notes)
            {
                await UploadNoteAsync(note);
            }
        }

        public async Task UploadNoteAsync(Note note)
        {
            var json = JsonSerializer.Serialize(note);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(FirebaseUrl, content);
        }

        private List<Note> LoadSavedNotes()
        {
            var savedNotes = new List<Note>();
            var keys = Preferences.Get("SavedListKeys", "").Split(',').Where(x => !string.IsNullOrWhiteSpace(x));

            foreach (var key in keys)
            {
                var json = Preferences.Get(key, null);
                if (json != null)
                {
                    var note = JsonSerializer.Deserialize<Note>(json);
                    if (note != null) savedNotes.Add(note);
                }
            }

            return savedNotes;
        }
    }

}
