using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App
{
    public static class NoteEvents
    {
        public static event EventHandler NoteListChanged;

        public static void RaiseNoteListChanged()
        {
            NoteListChanged?.Invoke(null, EventArgs.Empty);
        }
    }

}
