using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnnouncementDialog : ContentDialog
    {
        private bool isEditMode;

        public AnnouncementDialog(ObservableCollection<Announcement> announcements)
        {
            InitializeComponent();
            DataContext = new Announcement();
            CourseViewModel.selectedCourse.Announcements = announcements;
            isEditMode = false;
        }

        public AnnouncementDialog(ObservableCollection<Announcement> announcements, Announcement an)
        {
            InitializeComponent();
            DataContext = an;
            CourseViewModel.selectedCourse.Announcements = announcements;
            isEditMode = true;
            Title = "Edit Announcement";
        }
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                CourseViewModel.selectedCourse.Update(DataContext as Announcement);
            }
            else
            {
                CourseViewModel.selectedCourse.Add(DataContext as Announcement);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
