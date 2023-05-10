using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System;
using System.Collections.ObjectModel;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonTypeDialog : ContentDialog
    {
        private PersonService personService;

        public PersonTypeDialog(ObservableCollection<Person> people)
        {
            InitializeComponent();
            personService = PersonService.Current;
            personService.personList = people;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            switch (PersonCombo.SelectedIndex)
            {
                case 0:
                    var instructorDiag = new InstructorDialog(personService.personList);
                    await instructorDiag.ShowAsync();
                    break;
                case 1:
                    var teachingAssistantDiag = new TeachingAssistantDialog(personService.personList);
                    await teachingAssistantDiag.ShowAsync();
                    break;
                case 2:
                    var studentDiag = new StudentDialog(personService.personList);
                    await studentDiag.ShowAsync();
                    break;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
