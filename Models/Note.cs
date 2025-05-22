using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Gespraechsnotiz_App.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Topic { get; set; }
        public string Location { get; set; }
        public string PartnerName { get; set; }
        public string PartnerCompany { get; set; }
        public string PartnerRole { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Importance Importance { get; set; }
    }

    public enum Importance
    {
        Low,
        Medium,
        High
    }
}
