using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Canvas.UWP.ViewModels
{
    public class CourseViewModel
    {
        public static Course selectedCourse { get; set; }
        private CourseService courseService;
        private PersonService personService;

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Course> Courses {
            get
            {
                if (MainViewModel.loggedInPerson is Instructor)
                {
                    return courseService.courseList;
                }
                else if (MainViewModel.loggedInPerson is TeachingAssistant || MainViewModel.loggedInPerson is Student)
                {
                    return courseService.CoursesWithPerson(MainViewModel.loggedInPerson);
                }
                else
                {
                    return null;
                }
            }
        }

        public ObservableCollection<Course> AllCourses
        {
            get
            {
                return courseService.CoursesWithPerson(MainViewModel.loggedInPerson, false);
            }
        }

        public void Remove()
        {
            courseService.Remove(selectedCourse);
        }

        public ObservableCollection<Course> Search(string query, bool currentSemester = true) 
        {
            List<Course> searchResults = new List<Course>();
            if (currentSemester)
            {
                searchResults = Courses.Where(course => course.Name.ToLower().Contains(query.ToLower())
                                                         || course.Code.ToLower().Contains(query.ToLower())
                                                         || course.Location.ToLower().Contains(query.ToLower())
                                                         || course.Description.ToLower().Contains(query.ToLower())).ToList();
            }
            else
            {
                searchResults = AllCourses.Where(course => course.Name.ToLower().Contains(query.ToLower())
                                                                                                        || course.Code.ToLower().Contains(query.ToLower())
                                                                                                        || course.Location.ToLower().Contains(query.ToLower())
                                                                                                        || course.Description.ToLower().Contains(query.ToLower())).ToList();
            }
            var courses = new ObservableCollection<Course>(searchResults);
            return courses;
        }
    }
}