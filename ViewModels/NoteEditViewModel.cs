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
        public string TitleEdit { get { return "Notiz bearbeiten"; } }
        public string TitleCreate { get { return "Notiz erstellen"; } }
        public string TopicLabel { get { return "Thema"; } }
        public string Partner { get { return "Gesprächspartner"; } }
        public string PartnerNameLabel { get { return "Name"; } }
        public string DescriptionLabel { get { return "Beschreibung"; } }
        public string TimestampLabel { get { return "Datum & Uhrzeit"; } }
        public string PartnerRoleLabel { get { return "Rolle"; } }
        public string EmailLabel { get { return "E-Mail"; } }
        public string PhoneLabel { get { return "Telefonnummer"; } }
        public string PartnerCompanyLabel { get { return "Firma"; } }
        public string ImportanceLabel { get { return "Dringlichkeit"; } }
        public string SaveButtonLabel { get { return "Speichern"; } }
        public string CancelButtonLabel { get { return "Abbrechen"; } }
        public string ErrorLabel { get { return "Fehler"; } }
        public string EmailErrorDescription { get { return "Bitte eine gültige E-Mail-Adresse eingeben."; } }
        public string PhoneErrorDescription { get { return "Bitte eine gültige Telefonnummer eingeben (nur Ziffern und optional +)."; } }
        public string TopicErrorDescription { get { return "Das Thema ist erforderlich."; } }
        public string OKLabel { get { return "OK"; } }
        public string GeolocationNotFoundErrorDescription { get { return "Standort konnte nicht ermittelt werden."; } }
        public string AddressNotFoundErrorDescription { get { return "Adresse konnte nicht ermittelt werden."; } }
    }
}
