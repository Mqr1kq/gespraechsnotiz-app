using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    public class NoteEditPage : ContentPage
    {
        public NoteEditPage(Note note = null)
        {
            note ??= new Note();

            var topicEntry = new Entry { Placeholder = "Topic" };
            topicEntry.SetBinding(Entry.TextProperty, "Topic");

            var locationEntry = new Entry { Placeholder = "Location" };
            locationEntry.SetBinding(Entry.TextProperty, "Location");

            var nameEntry = new Entry { Placeholder = "Name" };
            nameEntry.SetBinding(Entry.TextProperty, "PartnerName");

            var companyEntry = new Entry { Placeholder = "Company" };
            companyEntry.SetBinding(Entry.TextProperty, "PartnerCompany");

            var roleEntry = new Entry { Placeholder = "Role" };
            roleEntry.SetBinding(Entry.TextProperty, "PartnerRole");

            var phoneEntry = new Entry { Placeholder = "Phone" };
            phoneEntry.SetBinding(Entry.TextProperty, "Phone");

            var emailEntry = new Entry { Placeholder = "Email" };
            emailEntry.SetBinding(Entry.TextProperty, "Email");

            var descriptionEditor = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
            descriptionEditor.SetBinding(Editor.TextProperty, "Description");

            var saveButton = new Button { Text = "Save", BackgroundColor = Colors.Green, TextColor = Colors.White };
            saveButton.Clicked += async (s, e) =>
            {
                // Speichern via NoteService (noch zu implementieren)
            };

            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Padding = 20,
                    Children =
                    {
                        topicEntry, locationEntry, nameEntry, companyEntry, roleEntry,
                        phoneEntry, emailEntry, descriptionEditor, saveButton
                    }
                }
            };

            BindingContext = note;
        }
    }
}
