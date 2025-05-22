using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.ViewModels
{
    [INotifyPropertyChanged]
    public partial class NoteEditViewModel
    {
        public string Title { get { return "Notiz bearbeiten"; } }
    }
}
