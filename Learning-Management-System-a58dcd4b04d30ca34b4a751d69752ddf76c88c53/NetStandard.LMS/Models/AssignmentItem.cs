using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStandard.LMS.Models
{
    public class AssignmentItem : ContentItem
    {
        public Assignment Assignment { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                   $"\n\t{Assignment.Name}";
        }
    }
}