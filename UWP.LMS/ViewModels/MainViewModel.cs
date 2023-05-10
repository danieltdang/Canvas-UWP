using Canvas.Library.Models;
using Canvas.Library.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Canvas.UWP.ViewModels
{
    public class MainViewModel
    {
        public static int personType { get; set; }
        public static Person loggedInPerson { get; set; }
        public string Query { get; set; }
        private CourseService courseService;
        private PersonService personService;

        public MainViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;

            if (personService.personList.Count == 0)
            {
                Instructor admin = new Instructor
                {
                    Name = "Admin",
                };

                personService.Add(admin);

                // Testing Objects
                Instructor instructor = new Instructor
                {
                    Name = "Professor Mills",
                };
                TeachingAssistant teachingAssistant = new TeachingAssistant
                {
                    Name = "Quan Pham",
                };
                Person student = new Student
                {
                    Name = "Daniel Dang",
                    Classification = Student.StudentClassification.Sophomore
                };

                Course course1 = new Course
                {
                    Name = "Full-Stack Application Development with C#",
                    Code = "COP4870",
                    Location = "HCB420",
                    Description = "COP Description",
                    CreditHours = 3
                };
                Course course2 = new Course
                {
                    Name = "Computer Organization I",
                    Code = "CDA3100",
                    Location = "BLMY69",
                    Description = "CDA Description",
                    CreditHours = 3
                };
                Course course3 = new Course
                {
                    Name = "Unix Tools",
                    Code = "COP4342",
                    Location = "MCH1337",
                    Description = "Unix Description",
                    CreditHours = 3
                };
                Course course4 = new Course
                {
                    Name = "Calculus with Analytic Geometry II",
                    Code = "MAC2312",
                    Location = "LOV2023",
                    Description = "MAC Description",
                    CreditHours = 4
                };

                Assignment a1 = new Assignment
                {
                    Name = "Assignment 1",
                    Description = "Description of Assignment 1",
                    TotalAvailablePoints = 100,
                    DueDate = DateTime.Today
                };

                Assignment a2 = new Assignment
                {
                    Name = "Assignment 2",
                    Description = "Description of Assignment 2",
                    TotalAvailablePoints = 100,
                    DueDate = DateTime.Today
                };

                AssignmentGroup ag1 = new AssignmentGroup
                {
                    Name = "Assignment Group 1",
                    Weight = 50
                };

                Assignment a3 = new Assignment
                {
                    Name = "Assignment 3",
                    Description = "Description of Assignment 3",
                    TotalAvailablePoints = 100,
                    DueDate = DateTime.Today
                };

                AssignmentGroup ag2 = new AssignmentGroup
                {
                    Name = "Assignment Group 2",
                    Weight = 50
                };

                Announcement anno1 = new Announcement
                {
                    Name = "Announcement 1",
                    Message = "This is a test announcement"
                };

                Announcement anno2 = new Announcement
                {
                    Name = "Announcement 2",
                    Message = "This is a test announcement 2"
                };

                Module module = new Module
                {
                    Name = "Module 1",
                    Description = "Description for Module 1",
                };

                PageItem pageItem = new PageItem
                {
                    Name = "Page Item",
                    Description = "Page Item Description",
                    HTMLBody = "index.html"
                };

                FileItem fileItem = new FileItem
                {
                    Name = "File Item",
                    Description = "File Item Description",
                    Path = "/Downloads"
                };

                AssignmentItem assignmentItem = new AssignmentItem
                {
                    Name = "Assignment Item",
                    Description = "Assignment Item Description",
                    Assignment = a1
                };

                personService.Add(instructor);
                personService.Add(teachingAssistant);
                personService.Add(student);

                course1.Add(module);
                module.Add(pageItem);
                module.Add(fileItem);
                module.Add(assignmentItem);
                course1.Add(ag1);
                ag1.Add(a1);
                ag1.Add(a2);
                course1.Add(ag2);
                ag2.Add(a3);
                course1.Add(anno1);
                course1.Add(anno2);

                courseService.Add(course1);
                courseService.Add(course2);
                courseService.Add(course3);
                courseService.Add(course4);

                course1.Add(student);
                course1.Add(teachingAssistant);
                course1.Add(instructor);
                course2.Add(student);
                course3.Add(student);
                course4.Add(student);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Person> People
        {
            get
            {
                if (personType == 0)
                {
                    var list = new ObservableCollection<Person>();
                    foreach (var instructors in personService.personList.Where(people => people is Instructor))
                    {
                        list.Add(instructors);
                    }
                    return list;
                }
                else if (personType == 1)
                {
                    var list = new ObservableCollection<Person>();
                    foreach (var teachingAssistant in personService.personList.Where(people => people is TeachingAssistant))
                    {
                        list.Add(teachingAssistant);
                    }
                    return list;
                }
                else if (personType == 2)
                {
                    var list = new ObservableCollection<Person>();
                    foreach (var student in personService.personList.Where(people => people is Student))
                    {
                        list.Add(student);
                    }
                    return list;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public ObservableCollection<Person> Search()
        {
            var searchResults = People.Where(people => people.Name.ToLower().Contains(Query.ToLower())).ToList();
            var selected = new ObservableCollection<Person>(searchResults);
            return selected;
        }
    }
}