using Canvas.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Canvas.Library.Services
{
    public class CourseService
    {
        public ObservableCollection<Course> courseList;
        private static CourseService _instance;

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }
                return _instance;
            }
        }

        public CourseService() 
        { 
            courseList = new ObservableCollection<Course>();
        }

        public void Add(Course course)
        {
            course.CalculateSemester();
            courseList.Add(course);
        }

        public void Update(Course course)
        {
            var courseIndex = Current.courseList.IndexOf(course);
            course.CalculateSemester();
            Current.courseList.RemoveAt(courseIndex);
            Current.courseList.Insert(courseIndex, course);
        }

        public void Remove(Course course)
        {
            courseList.Remove(course);
        }

        public List<Course> SearchAll(string query)
        {
            return courseList.Where(course => course.Name.ToLower().Contains(query.ToLower())
                                           || course.Code.ToLower().Contains(query.ToLower())
                                           || course.Location.ToLower().Contains(query.ToLower())
                                           || course.Description.ToLower().Contains(query.ToLower())).ToList();
        }

        public ObservableCollection<Course> CoursesWithPerson(Person person, bool currentSemester = true)
        {
            ObservableCollection<Course> list = new ObservableCollection<Course>();
            if (person is TeachingAssistant)
            {
                foreach (var course in Current.courseList)
                {
                    foreach (var people in course.Roster)
                    {
                        if (people.Id == person.Id)
                        {
                            list.Add(course);
                        }
                    }
                }
            }
            else if (person is Student)
            {
                if (currentSemester)
                {
                    var tempCourse = new Course()
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                    };
                    tempCourse.CalculateSemester();
                    foreach (var course in Current.courseList)
                    {
                        foreach (var people in course.Roster)
                        {
                            if (people.Id == person.Id && course.Semester == tempCourse.Semester)
                            {
                                list.Add(course);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var course in Current.courseList)
                    {
                        foreach (var people in course.Roster)
                        {
                            if (people.Id == person.Id)
                            {
                                list.Add(course);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public decimal CalculateCourseAverage(Course course, Student student)
        {
            decimal average = 0;
            decimal totalWeight = 0;
            foreach (var ag in course.AssignmentGroups)
            {
                totalWeight += ag.Weight;
            }

            foreach (var ag in course.AssignmentGroups)
            {
                foreach (var assignment in ag.Assignments)
                {
                    average += (student.Grades[assignment.Id] / ag.TotalPoints) * ((ag.Weight / totalWeight) * 4); //(ag.Weight / 25);
                }
            }

            return average;
        }

        public void CalculateGPA(Student student)
        {
            int creditHours = 0;

            foreach (var course in courseList)
            {
                if (course.AssignmentGroups.Count != 0)
                {
                    foreach (var person in course.Roster)
                    {
                        if (student.Id == person.Id)
                        {
                            creditHours += course.CreditHours;
                        }
                    }
                }
            }

            decimal gradePoints = 0;

            foreach (var course in courseList)
            {
                if (course.AssignmentGroups.Count != 0)
                {
                    foreach (var person in course.Roster)
                    {
                        if (student.Id == person.Id)
                        {
                            gradePoints += CalculateCourseAverage(course, student) * course.CreditHours;
                        }
                    }
                }
            }

            student.GPA = (gradePoints / creditHours);
        }
    }
}