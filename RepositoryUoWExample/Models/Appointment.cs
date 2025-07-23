using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryUoWExample.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date & Time")]
        public DateTime AppointmentDateTime { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }
    }

}
