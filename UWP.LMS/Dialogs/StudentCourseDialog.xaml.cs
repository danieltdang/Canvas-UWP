using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentCourseDialog : ContentDialog
    {
        private CourseService courseService;
        private ObservableCollection<Course> Courses { get; set; }
        private Course selectedCourse { get; set; }
        public Student Student { get; set; }

        public StudentCourseDialog(Student student)
        {
            InitializeComponent();
            courseService = CourseService.Current;
            Courses = new ObservableCollection<Course>(courseService.courseList.Where(course => !course.Roster.Contains(student)));
            Student = student;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            selectedCourse.Add(Student);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /*private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                CourseBox.ItemsSource = Courses.Where(course => course.Name.ToLower().Contains(SearchBox.Text.ToLower())
                                                             || course.Code.ToLower().Contains(SearchBox.Text.ToLower())
                                                             || course.Location.ToLower().Contains(SearchBox.Text.ToLower())
                                                             || course.Description.ToLower().Contains(SearchBox.Text.ToLower())).ToList();
            }
        }*/
    }
}
