using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    public class RegistrationService:IregistrationService
    {
        private Dictionary<int, Student> students; //key:studentid value:Student object
        private Dictionary<int, Course> courses;//same as above for ccourse id and object
        private Dictionary<int, List<int>> enrollments;//key:studentid ,value:list of course ids
        private IStorageIntrerface storage; 
        public RegistrationService()
        {
            students=new Dictionary<int, Student>();
            courses=new Dictionary<int, Course>();
            enrollments=new Dictionary<int, List<int>>();
            storage = new FileStorage();

        }

        public void AddStudent(int studentId,string name,string email,Department major,
            int enrollmentYear,int expectedGraduationYear)
        {
            if (students.ContainsKey(studentId))
            {
                throw new ArgumentException("Student with ID" + studentId + "already exists");
            }
            Student student=new Student(studentId, name, email, major,
                                         StudentStatus.ACTIVE, enrollmentYear, expectedGraduationYear);
            //this adds to dictionary
            students.Add(studentId, student);

            //Intialize empty enrollment list for this student
            enrollments.Add(studentId, new List<int>());
        }

        public void RemoveStudent(int studentId)
        {
            if (!students.ContainsKey((int)studentId))
            {
                throw new ArgumentException("Student with ID" + studentId + "doesnt exists");
            }

            //we git list of courses this student is enrolled in and then drop him from those
            //courses as well

            List<int> enrolledCourses=new List<int>(enrollments[studentId]);
            foreach(int courseId in enrolledCourses)
            {
                DropCourse(studentId, courseId);
            }
            //removing student from students dictionary
            students.Remove(studentId);

            //remove student's enrollment record
            enrollments.Remove(studentId);
        }
        public Student GetStudent(int studentId)
        {
            if (!students.ContainsKey(studentId))
            {
                return null;
            }
            return students[studentId];
        }
        public List<Student> GetAllStudents()
        {
            return new List<Student>(students.Values);
        }

        public void UpdateStudent(int studentId, string newName, String newEmail, Department newMajor)
        {
            if (!students.ContainsKey(studentId))
            {
                throw new ArgumentException($"student witj Id {studentId} does not exists");
            }

            //first we get the current student
            Student oldStudent=students[studentId];

            //remove this student form dicitonary
            students.Remove(studentId);

            //create new one with updated info
            Student UpdatedStudent = new Student(oldStudent.StudentId, newName, newEmail,
                newMajor, oldStudent.Status, oldStudent.EnrollmentYear, oldStudent.ExpectedGraduationYear);

            //adding updated student back
            students.Add(studentId, UpdatedStudent);
        }

        public void AddCourse(int courseId, string name, Department department, int credits,
            int capacity, string scehdule, Semester semester, int year)
        {
            if (courses.ContainsKey(courseId))
            {
                throw new ArgumentException($"COURSE with ID {courseId} already exists");

            }

            Course course = new Course(courseId, name, department, credits, capacity, scehdule,
                semester, year);
            courses.Add(courseId, course);
        }
        public void RemoveCourse(int courseId)
        {
            if (!courses.ContainsKey(courseId))
            {
                throw new ArgumentException($"this {courseId} does not exists");
            }

            //check if students are enrolled
            foreach(KeyValuePair<int,List<int>> enrollment in enrollments)
            {
                if (enrollment.Value.Contains(courseId))
                {
                    throw new InvalidOperationException("Cannot delete course with enrolled Students.Drop all students first");

                }
            }
            courses.Remove(courseId);
        }
        public Course GetCourse(int courseId)
        {
            if (!courses.ContainsKey(courseId))
            {
                return null;
            }
            return courses[courseId];       
        }
        public List<Course> GetAllCourse()
        {
            return new List<Course>(courses.Values);
        }

        public List<Course> GetAvailableCourses()
        {
            List<Course> availableCourses = new List<Course>();
            
            foreach (Course course in courses.Values)
            {
                if (!course.IsFull())
                {
                    availableCourses.Add(course);
                }
            }
            return availableCourses;
        }
        public List<Course> GetCourseBySemester(Semester semester)
        {
            List<Course> SemesterCourses=new List<Course> ();
            foreach(Course course in courses.Values)
            {
                if (course.Semester == semester)
                {
                    SemesterCourses.Add(course);
                }
            }

            return SemesterCourses;
        }

        public void EnrollStudentInCourse(int studentId,int courseId)
        {
            if (!students.ContainsKey(studentId))
            {
                throw new ArgumentException("Student With ID" + studentId + "not found");
            }

            if (!courses.ContainsKey(courseId))
            {
                throw new ArgumentException($"course with ID{courseId} not found");
            }
            if (enrollments[studentId].Contains(courseId))
            {
                throw new InvalidOperationException("student is already enrolled in this course");
            }

            if (courses[courseId].IsFull())
            {
                throw new InvalidOperationException("course is full");
            }

            //checking credit limit
            if (GetStudentTotalCredits(studentId) + courses[courseId].Credits > 18)
            {
                throw new InvalidOperationException("enrolling in this course would exceed the 18 credit limit");
            }
            enrollments[studentId].Add(courseId);
            courses[courseId].IncrementEnrollment();

        }
        public void DropCourse(int studentId,int courseId)
        {
            if (!students.ContainsKey(studentId))
            {
                throw new ArgumentException("Student With ID" + studentId + "not found");
            }

            if (!courses.ContainsKey(courseId))
            {
                throw new ArgumentException($"course with ID{courseId} not found");
            }
            if (!enrollments[studentId].Contains(courseId))
            {
                throw new InvalidOperationException("student is not enrolled in this course");
            }

            enrollments[studentId].Remove(courseId);

            courses[courseId].DecrementEnrollment();

        }

        public List<Course> GetStudentCourses(int studentId)
        {
            if (!students.ContainsKey(studentId))
            {
                throw new ArgumentException("Student With ID" + studentId + "not found");
            }

            List<Course> studentCourses = new List<Course>();
            foreach(int courseId in enrollments[studentId])
            {
                studentCourses.Add(courses[courseId]);
            }
            return studentCourses;
        }

        public List<Student> GetCourseStudents(int courseId)
        {
            if (!courses.ContainsKey(courseId))
            {
                throw new ArgumentException("Course with ID " + courseId + " not found");
            }
            List<Student> courseStudents = new List<Student>();
            foreach(KeyValuePair<int,List<int>> enrollment in enrollments)
            {
                if (enrollment.Value.Contains(courseId))
                {
                    courseStudents.Add(students[enrollment.Key]);
                }
            }
            return courseStudents;
        }

        public int GetStudentTotalCredits(int studentId)
        {
            if (!students.ContainsKey(studentId))
            {
                throw new ArgumentException("Student with ID " + studentId + " not found");
            }

            int totalCredits = 0;
            foreach(int courseId in enrollments[studentId])
            {
                totalCredits += courses[courseId].Credits;
            }

            return totalCredits;
        }

        public void Save()
        {
            storage.SaveStudents(students);
            storage.SaveCourses(courses);
            storage.SaveEnrollments(enrollments);
        }

        public void Load()
        {
            students=storage.LoadStudents();
            courses=storage.LoadCourses();

            //initializing enrollment lists for each student first
            foreach(int studentId in students.Keys)
            {
                enrollments.Add(studentId, new List<int>());
            }

            //load enrollments and mergeing then
            Dictionary<int,List<int>> loadedEnrollemnts=storage.LoadEnrollments();
            foreach(KeyValuePair<int,List<int>> enrollment in loadedEnrollemnts)
            {
                if (enrollments.ContainsKey(enrollment.Key))
                {
                    enrollments[enrollment.Key] = enrollment.Value;
                }
            }

        }
        public void ClearAllData()
        {
            students.Clear();
            courses.Clear();
            enrollments.Clear();
            storage.ClearAllData();
        }
        
    }

}
