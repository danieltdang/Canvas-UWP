using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Canvas.UWP.ViewModels
{
    public class PersonViewModel
    {
        public Person selectedPerson { get; set; }
        private CourseService courseService;
        private PersonService personService;

        public PersonViewModel() 
        { 
            courseService = CourseService.Current;
            personService = PersonService.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Person> People
        {
            get
            {
                return personService.personList;
            }
        }

        public void Remove()
        {
            personService.personList.Remove(selectedPerson);
        }

        public ObservableCollection<Person> Search(string query)
        {
            var searchResults = People.Where(people => people.Name.ToLower().Contains(query.ToLower())).ToList();
            var person = new ObservableCollection<Person>(searchResults);
            return person;
        }
    }
}