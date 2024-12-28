using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record StudentForCreationDto
    {
        [Required(ErrorMessage = "Matriculation number is a required field.")]
        public string? MatriculationNumber { get; set; }
        public Guid LevelId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}