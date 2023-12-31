using System.ComponentModel.DataAnnotations;

namespace HealthCare.Core.Models.Appointment
{
    public enum ServiceEnum
    {
        [Display(Name = "General checkup")]
        GeneralCheckup,
		[Display(Name = "Vaccination")]
		Vaccinaction,
        [Display(Name = "Emergency care")]
        EmergencyCare,
        [Display(Name = "X-ray")]
        XRay,
        [Display(Name = "Eye exam")]
        EyeExam,
    }
}