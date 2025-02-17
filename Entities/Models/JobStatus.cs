using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class JobStatus
    {
        [Column("JobStatusId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "JobName is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the JobName is 60 characters.")]
        public string JobName { get; set; }

        public bool IsRunning { get; set; }

        public DateTime LastRunTime { get; set; }

        public DateTime? CompletedTime { get; set; }
    }
}
