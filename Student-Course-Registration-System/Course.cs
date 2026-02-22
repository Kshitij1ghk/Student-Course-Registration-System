using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    public class Course
    {
        public int CourseId { get;private set; }
        public string Name { get;private set; }
        public Department Department { get;private set; }
        public int Credits {  get; private set; }
        public int Capacity {  get; private set; }
        public int EnrolledCount {  get; private set; }
        public string Schedule { get; private set; }
        
        public Semester Semester { get; private set; }
        public int Year {  get; private set; }

        public Course(int courseId , string name, Department department, int credits, int capacity, string schedule, Semester semester, int year)
        {
            if (courseId <= 0)
            {
                throw new ArgumentException("Corses ID must be positive");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Course name is required");
            }
            if (credits <= 0 || credits > 6)
            {
                throw new ArgumentException("credits must be between 1 and 6");
            }
            if(capacity <= 0)
            {
                throw new ArgumentException("cpacity must be postive");
            }
            if(string.IsNullOrWhiteSpace(schedule))
            {
                throw new ArgumentException("Schdule is required");
            }
            if (year < 2020 || year > 2030)
            {
                throw new ArgumentException("Year must be betweem 2020 and 2030");
            }

            CourseId = courseId;
            Name = name.ToUpper();
            Department = department;
            Credits = credits;
            Capacity = capacity;
            EnrolledCount = 0; //new course starts with 0 students since
            Schedule = schedule;
            Semester = semester;
            Year = year;

           
        }

        public bool IsFull() //to check if course is full
        {
            return EnrolledCount >= Capacity;
        }

        public void IncrementEnrollment()
        {
            if (IsFull())
            {
                throw new InvalidOperationException("course is already full");
            }
            EnrolledCount++;
        }
        public void DecrementEnrollment()
        {
            if (EnrolledCount <= 0)
            {
                throw new InvalidOperationException("No students enrolled to remove");
            }
            EnrolledCount--;
        }
    }
}
