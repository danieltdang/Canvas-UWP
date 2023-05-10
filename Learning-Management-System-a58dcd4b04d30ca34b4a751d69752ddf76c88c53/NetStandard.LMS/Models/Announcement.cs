using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStandard.LMS.Models
{
    public class Announcement : CourseDetail
    {
        public string Message { get; set; }

        public Announcement() 
        { 
            Name = string.Empty;
            Message = string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}:" +
                   $"\n{Message}";
        }
    }
}