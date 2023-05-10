using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using static NetStandard.LMS.Models.Student;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentDialog : ContentDialog
    {
        private bool isEditMode;
        private PersonService personService;

        public StudentDialog(ObservableCollection<Person> people)
        {
            InitializeComponent();
            DataContext = new Student();
            personService = PersonService.Current;
            personService.personList = people;
            isEditMode = false;
        }

        public StudentDialog(ObservableCollection<Person> people, Student student)
        {
            InitializeComponent();
            DataContext = student;
            personService = PersonService.Current;
            personService.personList = people;
            isEditMode = true;
            Title = "Edit Student";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as Student).Classification = (StudentClassification)StudentCombo.SelectedIndex;
            if (isEditMode)
            {
                personService.Update(DataContext as Student);
            }
            else
            {
                personService.Add(DataContext as Student);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
