using System.ComponentModel.DataAnnotations;

namespace RepositoryUoWExample.Models
{

        public class Patient
        {
            [Key]
            public int PatientId { get; set; }

            [Required]
            public string Name { get; set; }

            public DateTime DateOfBirth { get; set; }

            public string Gender { get; set; }

            public string ContactNumber { get; set; }

        public string Remarks { get; set; } = "N/A";

        public ICollection<Appointment> Appointments { get; set; }
        public Patient()
        {
            Appointments = new List<Appointment>();
        }
    }
    }


