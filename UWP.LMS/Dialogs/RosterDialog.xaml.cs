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
    public sealed partial class RosterDialog : ContentDialog
    {
        private PersonService personService;
        private ObservableCollection<Person> People { get; set; }
        private Person selectedPerson { get; set; }
        public Course Course { get; set; }

        public RosterDialog(Course course)
        {
            InitializeComponent();
            personService = PersonService.Current;
            People = new ObservableCollection<Person>(personService.personList.Where(person => !course.Roster.Contains(person)));
            Course = course;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Course.Add(selectedPerson);
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
