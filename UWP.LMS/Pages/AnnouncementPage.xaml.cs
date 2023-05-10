using Canvas.Library.Models;
using System;
using Canvas.UWP.Dialogs;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnnouncementPage : Page
    {
        public AnnouncementPage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                ItemBox.ItemsSource = (DataContext as CourseDetailViewModel).Search(SearchBox.Text);
            }
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else
            {
                var diag = new AnnouncementDialog((DataContext as CourseDetailViewModel).Announcements);
                await diag.ShowAsync();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else
            {
                (DataContext as CourseDetailViewModel).Remove();
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else if (!((DataContext as CourseDetailViewModel).selectedAnnouncement is null))
            {
                var diag = new AnnouncementDialog((DataContext as CourseDetailViewModel).Announcements, (DataContext as CourseDetailViewModel).selectedAnnouncement);
                await diag.ShowAsync();
            }
        }
    }
}
