using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Library.Models
{
    public class Person : CourseDetail
    {
        public int Id { get; set; }

        public Person()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public string ShortString()
        {
            if (this is Student student)
            {
                return $"{Name} ({student.Classification})";
            }
            else if (this is TeachingAssistant)
            {
                return $"{Name} (Teaching Assistant)";
            }
            else if (this is Instructor)
            {
                return $"{Name} (Instructor)";
            }
            else
            {
                return null;
            }
        }
    }
}