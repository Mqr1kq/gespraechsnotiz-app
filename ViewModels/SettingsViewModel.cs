using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.ViewModels
{
    public partial class SettingsViewModel
    {
        public string Title { get { return "Einstellungen"; } }
        public string SyncSwitchLabel { get { return "Synchronisation aktivieren"; } }
        public string SyncButtonLabel { get { return "Jetzt synchronisieren"; } }
        public string SuccessLabel { get { return "Erfolg"; } }
        public string SuccessDescription { get { return "Synchronisiert mit Firebase."; } }
        public string OKLabel { get { return "OK"; } }
    }
}
