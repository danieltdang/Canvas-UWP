using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetStandard.LMS.Models
{
    public class Student : Person
    {
        public decimal GPA { get; set; }
        public StudentClassification Classification { get; set; }
        public Dictionary<int, decimal> Grades { get; set; }

        public Student() 
        {
            Name = string.Empty;
            GPA = 0;
            Classification = StudentClassification.Freshman;
            Grades = new Dictionary<int, decimal>();
        }

        public override string ToString()
        {
            return $"{Name} ({Classification})" +
                   $"\nGPA: {GPA:0.00}";
        }

        public string GradeDisplay(int assignmentId)
        {
            if (!Grades.ContainsKey(assignmentId))
            {
                Grades[assignmentId] = 0;
            }
            return $"{Name} ({Classification})" +
                   $"\nGrade: {Grades[assignmentId]}";
        }

        public string DetailedDisplay
        {
            get
            {
                return $"{Name} ({Classification})" +
                       $"\nGPA: {GPA:0.00}";
            }
        }

        public void Grade(Assignment assignment, decimal grade)
        {
            Grades[assignment.Id] = grade;
        }

        public enum StudentClassification
        {
            Freshman, Sophomore, Junior, Senior
        }
    }
}