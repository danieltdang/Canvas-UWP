using Canvas.Library.Models;
using System;
using System.Linq;
using Canvas.UWP.Dialogs;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CourseDetailPage : Page
    {
        
        public CourseDetailPage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
            ViewType.Text = $"[{CourseViewModel.selectedCourse.Code}] {CourseViewModel.selectedCourse.Name}";
            CourseDetailFrame.Navigate(typeof(AnnouncementPage));
        }

        private void Option_Changed(object sender, RoutedEventArgs e)
        {
            if (CourseDetailViewModel.selectedOption == 0)
            {
                CourseDetailFrame.Navigate(typeof(AnnouncementPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (CourseDetailViewModel.selectedOption == 1)
            {
                CourseDetailFrame.Navigate(typeof(ModulePage), null, new SuppressNavigationTransitionInfo());
            }
            else if (CourseDetailViewModel.selectedOption == 2)
            {
                CourseDetailFrame.Navigate(typeof(AssignmentPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (CourseDetailViewModel.selectedOption == 3)
            {
                CourseDetailFrame.Navigate(typeof(RosterPage), null, new SuppressNavigationTransitionInfo());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CoursePage));
        }
    }
}
