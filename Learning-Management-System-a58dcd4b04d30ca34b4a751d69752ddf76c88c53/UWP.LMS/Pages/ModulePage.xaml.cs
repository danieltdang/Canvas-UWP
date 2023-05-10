using NetStandard.LMS.Models;
using System;
using UWP.LMS.Dialogs;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModulePage : Page
    {
        public ModulePage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
            CourseDetailViewModel.selectedOption = 1;
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
                var diag = new ModuleTypeDialog(DataContext as CourseDetailViewModel);
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
                (DataContext as CourseDetailViewModel).RefreshModules();
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else if (!((DataContext as CourseDetailViewModel).selectedCourseItem is null))
            {
                var selectedItem = (DataContext as CourseDetailViewModel).selectedCourseItem;
                if ((DataContext as CourseDetailViewModel).selectedCourseItem is Module)
                {
                    var diag = new ModuleDialog((DataContext as CourseDetailViewModel).ModulesOnly, (DataContext as CourseDetailViewModel).selectedCourseItem as Module);
                    await diag.ShowAsync();
                }
                else if ((DataContext as CourseDetailViewModel).selectedCourseItem is PageItem)
                {
                    var diag = new PageItemDialog((DataContext as CourseDetailViewModel).CurrentModule, (DataContext as CourseDetailViewModel).selectedCourseItem as PageItem);
                    await diag.ShowAsync();
                }
                else if ((DataContext as CourseDetailViewModel).selectedCourseItem is FileItem)
                {
                    var diag = new FileItemDialog((DataContext as CourseDetailViewModel).CurrentModule, (DataContext as CourseDetailViewModel).selectedCourseItem as FileItem);
                    await diag.ShowAsync();
                }
                else if ((DataContext as CourseDetailViewModel).selectedCourseItem is AssignmentItem)
                {
                    var diag = new AssignmentItemDialog((DataContext as CourseDetailViewModel).CurrentModule, (DataContext as CourseDetailViewModel).selectedCourseItem as AssignmentItem);
                    await diag.ShowAsync();
                }

                (DataContext as CourseDetailViewModel).RefreshModules();
                ItemBox.SelectedItem = selectedItem;
            }
        }
    }
}