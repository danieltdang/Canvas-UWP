using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            if (MainViewModel.personType == 0)
            {
                ViewType.Text = "Instructor Login";
                SearchBox.PlaceholderText = "Search Instructors";
            }
            else if (MainViewModel.personType == 1)
            {
                ViewType.Text = "Teaching Assistant Login";
                SearchBox.PlaceholderText = "Search Teaching Assistants";
            }
            else if (MainViewModel.personType == 2)
            {
                ViewType.Text = "Student Login";
                SearchBox.PlaceholderText = "Search Students";
            }

            PersonBox.ItemsSource = (DataContext as MainViewModel).People;
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty((DataContext as MainViewModel).Query))
            {
                var searchResults = (DataContext as MainViewModel).Search();
                PersonBox.ItemsSource = searchResults;
            }
            else
            {
                PersonBox.ItemsSource = (DataContext as MainViewModel).People;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson != null)
            {
                Frame.Navigate(typeof(CoursePage));
            }
        }
    }
}
