using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.ViewModels
{
    public class NoteDetailViewModel
    {
        public Note Note { get; set; }

        public NoteDetailViewModel(Note note)
        {
            Note = note;
        }
    }
}
