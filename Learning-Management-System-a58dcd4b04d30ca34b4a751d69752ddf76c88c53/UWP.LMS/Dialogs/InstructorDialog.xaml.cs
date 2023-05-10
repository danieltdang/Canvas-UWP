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
    public sealed partial class InstructorDialog : ContentDialog
    {
        private bool isEditMode;
        private PersonService personService;

        public InstructorDialog(ObservableCollection<Person> people)
        {
            InitializeComponent();
            DataContext = new Instructor();
            personService = PersonService.Current;
            personService.personList = people;
            isEditMode = false;
        }
        public InstructorDialog(ObservableCollection<Person> people, Instructor instructor)
        {
            InitializeComponent();
            DataContext = instructor;
            personService = PersonService.Current;
            personService.personList = people;
            isEditMode = true;
            Title = "Edit Instructor";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                personService.Update(DataContext as Instructor);
            }
            else
            {
                personService.Add(DataContext as Instructor);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
