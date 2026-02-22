using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    public interface IStorageIntrerface
    {
        void SaveStudents(Dictionary<int, Student> students);
        void SaveCourses(Dictionary<int, Course> courses);
        void SaveEnrollments(Dictionary<int, List<int>> enrollments);

        Dictionary<int, Student> LoadStudents();
        Dictionary<int, Course> LoadCourses();

        Dictionary<int, List<int>> LoadEnrollments();

        void ClearAllData();
    }
}
