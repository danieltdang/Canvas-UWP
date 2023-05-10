using NetStandard.LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP.LMS.ViewModels;
using UWP.LMS.Pages;
using UWP.LMS.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using NetStandard.LMS.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            Footer.Text = $"Made by Daniel Dang" +
                          $"\nCourses: {CourseService.Current.courseList.Count}" +
                          $"\nPeople: {PersonService.Current.personList.Count}";
        }

        private void Instructor_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.personType = 0;
            Frame.Navigate(typeof(LoginPage));
        }

        private void TeachingAssistant_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.personType = 1;
            Frame.Navigate(typeof(LoginPage));
        }

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.personType = 2;
            Frame.Navigate(typeof(LoginPage));
        }
    }
}