using NetStandard.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace NetStandard.LMS.Services
{
    public class PersonService
    {
        public ObservableCollection<Person> personList;
        private static int currentId;
        private List<int> deletedId;
        private static PersonService _instance;

        public PersonService()
        {
            personList = new ObservableCollection<Person>();
            currentId = 0;
            deletedId = new List<int>();
        }

        public static PersonService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonService();
                }

                return _instance;
            }
        }

        public void Add(Person person)
        {
            if (deletedId.Any())
            {
                person.Id = deletedId.First();
                deletedId.RemoveAt(0);
            }
            else
            {
                person.Id = currentId++;
            }
            personList.Add(person);
        }

        public void Remove(Person person)
        {
            deletedId.Add(person.Id);
            personList.Remove(person);
        }

        public void Update(Person person)
        {
            var personIndex = personList.IndexOf(person);
            personList.RemoveAt(personIndex);
            personList.Insert(personIndex, person);
        }

        public List<Person> SearchPerson(string query) 
        {
            return personList.Where(person => person.Name.Contains(query)).ToList();
        }

        public List<Student> SearchStudent(string query)
        {
            return personList.OfType<Student>().Where(student => student.Name.Contains(query)).ToList();
        }
    }
}