using HealthCare.Core.Models.Appointment;
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

public static partial class ServiceEnumExtensions
{
    public static string GetDisplayName(this ServiceEnum enumValue)
    {
        var displayAttribute = typeof(ServiceEnum)
            .GetField(enumValue.ToString())
            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}