using NetStandard.LMS.Models;
using System;
using UWP.LMS.Dialogs;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonPage : Page
    {
        public PersonPage()
        {
            InitializeComponent();
            DataContext = new PersonViewModel();
            if (MainViewModel.loggedInPerson is Instructor)
            {
                ViewType.Text = $"Instructor" +
                                $"\n{MainViewModel.loggedInPerson.Name}";
            }
            else if (MainViewModel.loggedInPerson is TeachingAssistant)
            {
                ViewType.Text = $"Teaching Assistant" +
                                $"\n{MainViewModel.loggedInPerson.Name}";
            }
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                PeopleBox.ItemsSource = (DataContext as PersonViewModel).Search(SearchBox.Text);
            }
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var diag = new PersonTypeDialog((DataContext as PersonViewModel).People);
            await diag.ShowAsync();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PersonViewModel).Remove();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!((DataContext as PersonViewModel).selectedPerson is null))
            {
                if ((DataContext as PersonViewModel).selectedPerson is Instructor)
                {
                    var diag = new InstructorDialog((DataContext as PersonViewModel).People, (DataContext as PersonViewModel).selectedPerson as Instructor);
                    await diag.ShowAsync();
                }
                else if ((DataContext as PersonViewModel).selectedPerson is TeachingAssistant)
                {
                    var diag = new TeachingAssistantDialog((DataContext as PersonViewModel).People, (DataContext as PersonViewModel).selectedPerson as TeachingAssistant);
                    await diag.ShowAsync();
                }
                else if ((DataContext as PersonViewModel).selectedPerson is Student)
                {
                    var diag = new StudentDialog((DataContext as PersonViewModel).People, (DataContext as PersonViewModel).selectedPerson as Student);
                    await diag.ShowAsync();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void Course_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CoursePage));
        }
    }
}