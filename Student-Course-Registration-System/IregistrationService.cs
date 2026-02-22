using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Student_Course_Registration_System
{
    public interface IregistrationService
    {
        void AddStudent(int studentId, string name, string email, Department major,
            int enrollmentYear, int expectedGraduationYear);
        void RemoveStudent(int studentId);
        Student GetStudent(int studentId);
        List<Student> GetAllStudents();
        void UpdateStudent(int studentId, string newName,String newEmail,Department newMajor);

        //Course Management
        void AddCourse(int courseId,string name,Department department,int credits,
            int capacity,string scehdule,Semester semester,int year);
        void RemoveCourse(int courseID);
        Course GetCourse(int courseId);

        List<Course> GetAllCourse();
        List<Course> GetAvailableCourses();
        List<Course> GetCourseBySemester(Semester semester);

        //enrollment management
        void EnrollStudentInCourse(int studentId,int courseId);
        void DropCourse(int studentId, int CourseId);

        List<Course> GetStudentCourses(int studentId);
        List<Student> GetCourseStudents(int courseId);
        int GetStudentTotalCredits(int studentId);


    }
}
