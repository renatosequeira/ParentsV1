using Newtonsoft.Json;
using Parents.Domain.TasksManagement.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Parents.Domain.TasksManagement
{
    public class TaskFunction
    {
        [Key]
        public int TaskId { get; set; }

        public string TaskDescription { get; set; }

        public string TaskOwner { get; set; } //should be the set to the current user

        public int? ParentId { get; set; }
        [JsonIgnore]
        public virtual Parent TaskResponsible { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskCreationDate { get; set; }

        public bool TaskStatus { get; set; }

        public Nullable<DateTime> TaskConclusionDate { get; set; }

        public int? TaskFamilyId { get; set; }
        [JsonIgnore]
        public virtual TaskFamily TaskFamily { get; set; }

        [DataType(DataType.MultilineText)]
        public string TaskRemarks { get; set; }
    }
}
