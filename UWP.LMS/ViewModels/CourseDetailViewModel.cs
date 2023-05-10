using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Module = Canvas.Library.Models.Module;

namespace Canvas.UWP.ViewModels
{
    public class CourseDetailViewModel : INotifyPropertyChanged
    {
        public Course selectedCourse { get; set; }
        public CourseDetail selectedCourseItem { get; set; }
        public static int selectedOption { get; set; }
        public static int selectedSubOption { get; set; }
        public Announcement selectedAnnouncement { get; set; }
        public static Module currentModule;
        public static AssignmentGroup currentAg;
        public static Assignment selectedAssignment { get; set; }
        public Person selectedPerson { get; set; }
        private CourseService courseService;
        private PersonService personService;

        public CourseDetailViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            selectedOption = 0;
            selectedSubOption = 0;
            selectedCourse = CourseViewModel.selectedCourse;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshModules()
        {
            NotifyPropertyChanged(nameof(Modules));
        }

        public void RefreshAssignments()
        {
            NotifyPropertyChanged(nameof(AssignmentGroups));
        }

        public void RefreshStudents()
        {
            NotifyPropertyChanged(nameof(Students));
        }

        public ObservableCollection<Announcement> Announcements
        {
            get
            {
                return selectedCourse.Announcements;
            }
            set
            {
                selectedCourse.Announcements = value;
            }
        }

        public ObservableCollection<CourseDetail> Modules
        {
            get
            {
                return selectedCourse.ModulesAndContent;
            }
        }

        public ObservableCollection<Module> ModulesOnly
        {
            get
            {
                return selectedCourse.Modules;
            }
            set
            {
                selectedCourse.Modules = value;
            }
        }

        public Module CurrentModule
        {
            get
            {
                if (ModulesOnly != null)
                {
                    currentModule = ModulesOnly.FirstOrDefault(module => module.Content.Contains(selectedCourseItem as ContentItem));
                    return currentModule;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                currentModule = value;
            }
        }

        public ObservableCollection<CourseDetail> AssignmentGroups
        {
            get
            {
                return selectedCourse.AgAndAssignments;
            }
        }

        public ObservableCollection<AssignmentGroup> AssignmentGroupsOnly
        {
            get
            {
                return selectedCourse.AssignmentGroups;
            }
            set
            {
                selectedCourse.AssignmentGroups = value;
            }
        }

        public AssignmentGroup CurrentAg
        {
            get
            {
                if (AssignmentGroupsOnly != null)
                {
                    currentAg = AssignmentGroupsOnly.FirstOrDefault(ag => ag.Assignments.Contains(selectedCourseItem as Assignment));
                    return currentAg;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                currentAg = value;
            }
        }

        public ObservableCollection<Person> People
        {
            get
            {
                    return selectedCourse.Roster;
            }
            set
            {
                selectedCourse.Roster = value;
            }
        }

        public ObservableCollection<Person> Students
        {
            get
            {
                return new ObservableCollection<Person>(People.Where(person => person is Student));
            }
        }

        public void Remove()
        {
            if (selectedOption == 0)
            {
                Announcements.Remove(selectedAnnouncement);
            }
            else if (selectedOption == 1)
            {
                if (selectedCourseItem is Module) 
                {
                    selectedCourse.Remove(selectedCourseItem as Module);
                }
                else if (selectedCourseItem is ContentItem)
                {
                    selectedCourse.Remove(selectedCourseItem as ContentItem);
                }
            }
            else if (selectedOption == 2)
            {
                if (selectedCourseItem is AssignmentGroup)
                {
                    selectedCourse.Remove(selectedCourseItem as AssignmentGroup);
                }
                else if (selectedCourseItem is Assignment)
                {
                    selectedCourse.Remove(selectedCourseItem as Assignment);
                }
            }
            else if (selectedOption == 3)
            {
                People.Remove(selectedPerson);
            }
        }

        public ObservableCollection<CourseDetail> Search(string query)
        {
            if (selectedOption == 0)
            {
                var searchResults = Announcements.Where(announcement => announcement.Name.ToLower().Contains(query.ToLower())
                                                                     || announcement.Message.ToLower().Contains(query.ToLower())).ToList();
                return new ObservableCollection<CourseDetail>(searchResults);
            }
            else if (selectedOption == 1)
            {
                var searchResults = Modules.Where(module => module.Name.ToLower().Contains(query.ToLower())).ToList();
                return new ObservableCollection<CourseDetail>(searchResults);
            }
            else if (selectedOption == 2)
            {
                if (selectedSubOption == 0)
                {
                    var searchResults = AssignmentGroups.Where(ag => ag.Name.ToLower().Contains(query.ToLower())).ToList();
                    return new ObservableCollection<CourseDetail>(searchResults);
                }
                else if (selectedSubOption == 1)
                {
                    var searchResults = Students.Where(student => student.Name.ToLower().Contains(query.ToLower())).ToList();
                    return new ObservableCollection<CourseDetail>(searchResults);
                }
            }
            else if (selectedOption == 3)
            {
                var searchResults = People.Where(person => person.Name.ToLower().Contains(query.ToLower())).ToList();
                return new ObservableCollection<CourseDetail>(searchResults);
            }

            return null;
        }

        public void UpdateGrade(Student student)
        {
            courseService.CalculateGPA(student);
        }
    }
}