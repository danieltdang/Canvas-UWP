using Canvas.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Canvas.Library.Models
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CreditHours { get; set; }
        public string Semester { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }

        public Course()
        {
            Code = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Location = string.Empty;
            Semester = string.Empty;
            CreditHours = 3;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Roster = new ObservableCollection<Person>();
            AssignmentGroups = new ObservableCollection<AssignmentGroup>();
            Modules = new ObservableCollection<Module>();
            Announcements = new ObservableCollection<Announcement>();
        }

        public override string ToString()
        {
            return $"[{Semester}] ({Code}) {Name}" +
                   $"\n{Location}" +
                   $"\n{Description}";
        }

        public string ShortString()
        {
            return $"[{Semester}] ({Code}) {Name}";
        }

        public ObservableCollection<CourseDetail> ModulesAndContent
        {
            get
            {
                var list = new ObservableCollection<CourseDetail>();
                foreach (var module in Modules)
                {
                    list.Add(module);
                    foreach (var content in module.Content)
                    {
                        list.Add(content);
                    }
                }
                return list;
            }
        }

        public ObservableCollection<CourseDetail> AgAndAssignments
        {
            get
            {
                var list = new ObservableCollection<CourseDetail>();
                foreach (var ag in AssignmentGroups)
                {
                    list.Add(ag);
                    foreach (var assignment in ag.Assignments)
                    {
                        list.Add(assignment);
                    }
                }
                return list;
            }
        }

        public ObservableCollection<Assignment> AllAssignments
        {
            get
            {
                var list = new ObservableCollection<Assignment>();
                foreach (var ag in AssignmentGroups)
                {
                    foreach (var assignment in ag.Assignments)
                    {
                        list.Add(assignment);
                    }
                }
                return list;
            }
        }

        public void CalculateSemester()
        {
            if (StartDate.Month >= 1 && StartDate.Month <= 4 && EndDate.Month >= 1 && EndDate.Month <= 4)
            {
                Semester = $"Spring {EndDate.ToString("yyyy")}";
            }
            else if (StartDate.Month >= 5 && StartDate.Month <= 7 && EndDate.Month >= 5 && EndDate.Month <= 7)
            {
                Semester = $"Summer {EndDate.ToString("yyyy")}";
            }
            else if (StartDate.Month >= 8 && StartDate.Month <= 12 && EndDate.Month >= 8 && EndDate.Month <= 12)
            {
                Semester = $"Fall {EndDate.ToString("yyyy")}";
            }
        }

        public void Add(Person person)
        {
            Roster.Add(person);
            if (person is Student student)
            {
                foreach (var ag in AssignmentGroups)
                {
                    foreach (var assignment in ag.Assignments)
                    {
                        student.Grades.Add(assignment.Id, 0);
                    }
                }
            }
        }

        public void Add(AssignmentGroup ag)
        {
            AssignmentGroups.Add(ag);
        }

        public void Add(Announcement an)
        {
            Announcements.Add(an);
        }

        public void Add(Module module)
        {
            Modules.Add(module);
        }

        public void Remove(AssignmentGroup ag)
        {
            AssignmentGroups.Remove(ag);
        }

        public void Remove(Assignment assignment)
        {
            foreach (var ag in AssignmentGroups)
            {
                if (ag.Assignments.Contains(assignment))
                {
                    ag.Assignments.Remove(assignment);
                    break;
                }
            }
        }

        public void Remove(Announcement an)
        {
            Announcements.Remove(an);
        }

        public void Remove(Module module)
        {
            Modules.Remove(module);
        }

        public void Remove(ContentItem content)
        {
            foreach (var module in Modules)
            {
                if (module.Content.Contains(content))
                {
                    module.Content.Remove(content);
                    break;
                }
            }
        }

        public void Update(Announcement an)
        {
            var anIndex = Announcements.IndexOf(an);
            Announcements.RemoveAt(anIndex);
            Announcements.Insert(anIndex, an);
        }

        public void Update(Module module)
        {
            var moIndex = Modules.IndexOf(module);
            Modules.RemoveAt(moIndex);
            Modules.Insert(moIndex, module);
        }

        public void Update(AssignmentGroup ag)
        {
            var agIndex = AssignmentGroups.IndexOf(ag);
            AssignmentGroups.RemoveAt(agIndex);
            AssignmentGroups.Insert(agIndex, ag);
        }
    }
}