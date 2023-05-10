using Canvas.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Library.Models
{
    public class AssignmentGroup : CourseDetail
    {
        public decimal Weight { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public decimal TotalPoints { get; set; }
        private static int currentId = 0;
        private static List<int> deletedId;

        public static int Current
        {
            get
            {
                return currentId++;
            }
        }

        public static List<int> Deleted
        {
            get
            {
                if (deletedId == null)
                {
                    deletedId = new List<int>();
                }
                return deletedId;
            }
        }

        public AssignmentGroup()
        {
            Name = string.Empty;
            Weight = 100;
            Assignments = new ObservableCollection<Assignment>();
            TotalPoints = 0;
        }

        public override string ToString()
        {
            return $"{Name} ({Weight})";
        }

        public string DetailedDisplay
        {
            get
            {
                return $"{this}";
            }
        }

        public void Add(Assignment assignment)
        {
            if (deletedId == null || deletedId.Count == 0)
            {
                assignment.Id = Current;
            }
            else
            {
                var firstId = Deleted.FirstOrDefault();
                Deleted.RemoveAt(0);
                assignment.Id = firstId;
            }
            Assignments.Add(assignment);
            TotalPoints += assignment.TotalAvailablePoints;
        }

        public void Remove(Assignment assignment)
        {
            deletedId.Add(assignment.Id);
            Assignments.Remove(assignment);
        }

        public void Update(Assignment assignment)
        {
            var asIndex = Assignments.IndexOf(assignment);
            Assignments.RemoveAt(asIndex);
            Assignments.Insert(asIndex, assignment);
        }
    }
}