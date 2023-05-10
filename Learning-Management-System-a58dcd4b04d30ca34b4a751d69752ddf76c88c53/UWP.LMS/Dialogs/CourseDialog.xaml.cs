using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CourseDialog : ContentDialog
    {
        private bool isEditMode;
        private CourseService courseService;

        public CourseDialog(ObservableCollection<Course> courses)
        {
            InitializeComponent();
            DataContext = new Course();
            courseService = CourseService.Current;
            this.courseService.courseList = courses;
            isEditMode = false;
        }

        public CourseDialog(ObservableCollection<Course> courses, Course course)
        {
            InitializeComponent();
            DataContext = course;
            courseService = CourseService.Current;
            this.courseService.courseList = courses;
            isEditMode = true;
            Title = "Edit Course";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                courseService.Update(DataContext as Course);
            }
            else
            {
                courseService.Add(DataContext as Course);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
