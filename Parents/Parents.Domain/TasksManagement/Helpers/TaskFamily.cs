using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.TasksManagement.Helpers
{
    public class TaskFamily
    {
        [Key]
        public int TaskFamilyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("TaskFamily_TaskFamilyDescription_Index", IsUnique = true)]
        public string TaskFamilyDescription { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
