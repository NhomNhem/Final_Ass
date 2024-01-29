using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Final_Ass
{
    public delegate void AddStudentHandler(object sender, EventArgs eventArgs);

    public class StudentManager<T> where T : RegularStudent, new()
    {
        public static event AddStudentHandler OnStudentAdded;

        //private const string fileName = "students.txt";
        

        private List<T> _students = new List<T>();

        public void AddStudent(T student)
        {
            _students.Add(student);
            OnStudentAdded?.Invoke(this, EventArgs.Empty);
        }

        public void InputStudents(int numberOfStudents)
        {
            for (int i = 0; i < numberOfStudents; i++)
            {
                T student = new T();
                student.Input();
                AddStudent(student);
            }
        }
    
        
        // Read students from file
        public void ReadStudentsFromFile(string _path)
        {
            try
            {
                string[] lines = File.ReadAllLines(_path);
                Console.WriteLine($"Number of lines read from file: {lines.Length}");

                foreach (var line in lines)
                {
                    Console.WriteLine($"Processing line: {line}");
                    string[] parts = line.Split(',');

                    if (parts.Length == 4)
                    {
                        T student = new T();
                        student.MSSV = parts[0];
                        student.HoTen = parts[1];
                        student.Diem = double.Parse(parts[2]);
                        student.Email = parts[3];
                        student.SetHocLuc();
                       
                        AddStudent(student);
                    }
                    else
                    {
                        Console.WriteLine($"Dòng {line} không hợp lệ. Bỏ qua.");
                    }
                }

                Console.WriteLine("Đọc file thành công.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Đọc file thất bại.");
                Console.WriteLine(e.Message);
            }
        }
        
        
        // find student by point int range entered by user
        public void SearchByScoreRange()
        {
            Console.Write("Nhập điểm thấp nhất: ");
            double min = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm cao nhất: ");
            double max = double.Parse(Console.ReadLine());

            bool found = false;
            foreach (var student in _students)
            {
                if (student.Diem >= min && student.Diem <= max)
                {
                    Console.WriteLine(student.ToString());
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy học viên nào.");
            }
        }
        
        
        // search student by classification valid input: Yếu, Trung Bình, Khá, Giỏi, Xuất Sắc
        protected bool IsValidClassification(string classification)
        {
            string[] validClassifications = { "Yeu","Kha","Gioi","Trung Binh","Xuat Sac", "yeu", "trung binh", "kha", "gioi", "xuat sac", "y", "tb", "k", "g", "xs" };
            foreach (var validClassification in validClassifications)
            {
                if (classification == validClassification)
                    return true;
            }

            return false;
        }
        public void SearchByClassification()
        {
            Console.Write("Nhập học lực (Yếu, Trung Bình, Khá, Giỏi, Xuất Sắc): ");
            string classification = Console.ReadLine();
            Console.WriteLine(classification);
            if (IsValidClassification(classification))
            {
                bool found = false;
                foreach (var student in _students)
                {
                    if (student.HocLuc.Trim().ToLower().Equals(classification.Trim().ToLower()))
                    {
                        Console.WriteLine(student.ToString());
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Không tìm thấy học viên nào.");
                }
            }
            else
            {
                Console.WriteLine("Học lực không hợp lệ.");
            }
        }
        
        // Search student by MSSV and update information
        public void SearchAndUpdateStudent()
        {
            Console.Write("Nhập mã số sinh viên: ");
            string mssv = Console.ReadLine();

            var studentToUpdate = _students.FirstOrDefault(student => student.MSSV == mssv);

            if (studentToUpdate != null)
            {
                Console.WriteLine(studentToUpdate.ToString());
                Console.WriteLine("Nhập thông tin mới cho học viên này.");
                studentToUpdate.Input();
            }
            else
            {
                Console.WriteLine("Không tìm thấy học viên nào.");
            }
        }

        
        // sort student by point
        public void SortByPoint()
        {
            _students.Sort((student1, student2) => student1.Diem.CompareTo(student2.Diem));
        }
        // sort by point by bubble sort
        public void SortByPointBubbleSort()
        {
            for (int i = 0; i < _students.Count - 1; i++)
            {
                for (int j = 0; j < _students.Count - i - 1; j++)
                {
                    if (_students[j].Diem > _students[j + 1].Diem)
                    {   
                        /*
                        var temp = _students[j];
                        _students[j] = _students[j + 1];
                        _students[j + 1] = temp;
                        */
                        (_students[j], _students[j + 1]) = (_students[j + 1], _students[j]);
                    }
                }
            }
        }
        
        // Display 5 students with highest point
        public void Display5StudentsWithHighestPoint()
        {
            SortByPoint();
            for (int i = _students.Count - 1; i >= _students.Count - 5; i--)
            {
                Console.WriteLine(_students[i].ToString());
            }
        }
        
        // Calculate average point of class
        public void CalculateAveragePoint()
        {
            double sum = 0;
            foreach (var student in _students)
            {
                sum += student.Diem;
            }

            Console.WriteLine($"Điểm trung bình của lớp là: {sum / _students.Count}");
        }
        
        // Display students with point higher than average point of class
        public void displayStudentWithPointHigherAvg()
        {
            double sum = 0;
            foreach (var student in _students)
            {
                sum += student.Diem;
            }

            double averagePoint = sum / _students.Count;

            foreach (var student in _students)
            {
                if (student.Diem > averagePoint)
                {
                    Console.WriteLine(student.ToString());
                }
            }
        }
        
        // Count number of students by classification
        public void CountStudentByClassification()
        {
            var count = _students.GroupBy(student => student.HocLuc)
                .Select(group => $"{group.Key}: {group.Count()}")
                .ToList();

            foreach (var result in count)
            {
                Console.WriteLine(result);
            }
        }

        

        /*
        public void DisplayStudents()
        {
            foreach (var student in _students)
            {
                Console.WriteLine(student.ToString());
            }
        }*/
        
        
        
        public void DisplayStudents()
        {
            if (_students.Count == 0)
            {
                Console.WriteLine("Danh sách học viên trống.");
            }
            else
            {
                foreach (var student in _students)
                {
                    Console.WriteLine(student.ToString());
                }
            }
        }

    }
}
