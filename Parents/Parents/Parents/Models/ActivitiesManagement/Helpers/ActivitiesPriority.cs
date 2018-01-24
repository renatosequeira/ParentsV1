using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Models.ActivitiesManagement.Helpers
{
    public class ActivitiesPriority
    {
        [PrimaryKey]
        public int ActivityPriorityId { get; set; }
        public string PriorityDescription { get; set; }
        public string PriorityImage { get; set; }
    }
}
