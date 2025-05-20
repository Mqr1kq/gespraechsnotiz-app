using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gespraechsnotiz_App.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public ICommand AddNoteCommand { get; }

        public MainViewModel()
        {
            AddNoteCommand = new Command(() =>
            {
                // Navigation zur NoteEditPage einbauen
            });

            // Beispieldaten
            Notes.Add(new Note
            {
                Topic = "Meeting mit Kunde A",
                Location = "BBS Meppen",
                PartnerName = "Hannah Lamp",
                PartnerCompany = "Firma GmbH",
                PartnerRole = "Sekretärin",
                Phone = "0123456789",
                Email = "hl@gmail.com",
                Description = "Kurze Notiz...",
                IsImportant = true
            });
        }
    }
}
