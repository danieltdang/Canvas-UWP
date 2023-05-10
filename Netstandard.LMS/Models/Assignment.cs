using System;

namespace Canvas.Library.Models
{
    public class Assignment : CourseDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TotalAvailablePoints { get; set; }
        public DateTimeOffset DueDate { get; set; }

        public Assignment()
        {
            Name = string.Empty;
            Description = string.Empty;
            TotalAvailablePoints = 100;
            DueDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} ({TotalAvailablePoints})" +
                   $"\nDue Date: {DueDate.ToString("MMMM dd, yyyy")}" +
                   $"\nDescription: {Description}";
        }
    }
}