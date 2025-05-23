using CommunityToolkit.Mvvm.ComponentModel;
using Gespraechsnotiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.ViewModels
{
    [INotifyPropertyChanged]
    public partial class NoteDetailViewModel
    {
        public string Title { get { return "Details"; } }
        public string TopicLabel { get { return "Thema"; } }
        public string LocationLabel { get { return "Standort"; } }
        public string Partner { get { return "Gesprächspartner"; } }
        public string PartnerNameLabel { get { return "Name"; } }
        public string DescriptionLabel { get { return "Beschreibung"; } }
        public string TimestampLabel { get { return "Datum & Uhrzeit"; } }
        public string PartnerRoleLabel { get { return "Rolle"; } }
        public string EmailLabel { get { return "E-Mail"; } }
        public string ErrorText { get { return "No details available."; } }
        public string PhoneLabel { get { return "Telefonnummer"; } }
        public string PartnerCompanyLabel { get { return "Firma"; } }
        public string ImportanceLabel { get { return "Dringlichkeit"; } }
        public string SendNotePerMailButtonLabel { get { return "Sende Notiz Per Mail"; } }
        public string EditButtonLabel { get { return "Bearbeiten"; } }
        public string ErrorLabel { get { return "Fehler"; } }
        public string EmailErrorDescription { get { return "Bitte eine gültige E-Mail-Adresse eingeben."; } }
        public string OKLabel { get { return "OKLabel"; } }
        public string SendButtonLabel { get { return "Senden"; } }
        public string CancelButtonLabel { get { return "Abbrechen"; } }
        public string EmailReceiverLabel { get { return "Empfänger-E-Mail-Adresse"; } }
    }
}
