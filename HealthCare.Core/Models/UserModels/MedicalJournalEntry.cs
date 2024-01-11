using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.UserModels
{
    public class MedicalJournalEntry
    {
        public int Id { get; set; }

        public string Entry {  get; set; }

        public DateTime DateTime { get; set; }

        public Patient Patient { get; set; }

        public CareGiver CareGiver { get; set; }
    }
}
