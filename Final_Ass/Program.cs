using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Final_Ass;

namespace Final_Assignment
{
    class Program
    {
        static StudentManager<RegularStudent> _studentManager = new StudentManager<RegularStudent>();
        
        
        static Dictionary<int, string> menu = new Dictionary<int, string>()
        {
            { 1, "Nhập danh sách học viên" },
            { 2, "Xuất danh sách học viên" },
            { 3, "Tìm kiếm học viên theo khoảng điểm" },
            { 4, "Tìm kiếm học viên theo học lực" },
            { 5, "Tìm học viên theo mã số và cập nhật thông tin" },
            { 6, "Sắp xếp học viên theo điểm" },
            { 7, "Xuất 5 học viên có điểm cao nhất" },
            { 8, "Tính điểm trung bình của lớp" },
            { 9, "Xuất danh sách học viên có điểm trên điểm trung bình của lớp" },
            {10, "Tổng hợp số học viên theo học lực" },
            {11, "ReadFromFile"},
            {12,"Clear Screen"},
            {13, "Save"},
            { 0, "Thoát" },
        };

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine(Environment.CurrentDirectory);
            
            while (true)
            {
                PrintMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (menu.ContainsKey(choice))
                    {
                        ProcessOption(choice);

                        if (choice == 0)
                        {
                            Console.WriteLine("Kết thúc chương trình!");
                            break; // Thoát khỏi vòng lặp
                        }
                    }
                    else
                    {
                        Console.WriteLine("Chọn không hợp lệ. Vui lòng chọn lại!");
                    }
                }
                else
                {
                    Console.WriteLine("Chọn không hợp lệ. Vui lòng chọn lại!");
                }

                Console.WriteLine();
            }
        }

        #region Menu
        static void PrintMenu()
        {
            Console.WriteLine("===== MENU =====");
            foreach (var entry in menu)
            {
                Console.WriteLine($"{entry.Key}. {entry.Value}");
            }

            Console.Write("Chọn chức năng (0-10): ");
        }

        static void ProcessOption(int choice)
        {
            Console.WriteLine($"Chức năng {menu[choice]}");
            switch (choice)
            {
                case 1:
                    InputStudents();
                    break;
                case 2:
                    PrintStudents();
                    break;
                case 3:
                    SearchByScoreRange();
                    break;
                case 4:
                    SearchByAcademicPerformance();
                    break;
                case 5:
                    SearchAndUpdateStudentInfo();
                    break;
                case 6:
                    SortByPoint_();
                    break;
                case 7:
                    PrintTop5Students();
                    break;
                case 8:
                    CalculateClassAverage();
                    break;
                case 9:
                    PrintAboveAverageStudents();
                    break;
                case 10:
                    CountStudentsByAcademicPerformance();
                    break;
                case 11:
                    ReadStudentsFromFile();
                    break;
                case 12:
                    Console.Clear();
                    break;
                case 13:
                    WriteStudentToFile();
                    break;
                default:
                    Console.WriteLine("Chọn không hợp lệ. Vui lòng chọn lại!");
                    break;
            }
        }
    
        #endregion
        #region ProcessOption
        private static void CountStudentsByAcademicPerformance()
        {
            _studentManager.CountStudentByClassification();
        }

        private static void PrintAboveAverageStudents()
        {
            _studentManager.displayStudentWithPointHigherAvg();
        }

        private static void CalculateClassAverage()
        {
            _studentManager.CalculateAveragePoint();
        }

        private static void PrintTop5Students()
        {
            _studentManager.Display5StudentsWithHighestPoint();
        }

        private static void SortByPoint_()
        {
            _studentManager.SortByPoint();
            _studentManager.DisplayStudents();
        }

        private static void SearchAndUpdateStudentInfo()
        {
            _studentManager.SearchAndUpdateStudent();
        }

        private static void SearchByAcademicPerformance()
        {
            _studentManager.SearchByClassification();
        }

        private static void SearchByScoreRange()
        {
            _studentManager.SearchByScoreRange();
        }

        private static void PrintStudents()
        {
            _studentManager.DisplayStudents();
        }

        private static void InputStudents()
        {   
            // normal input
            
            
            while (true)
            {   
                Console.Write("Nhập số lượng học viên: ");
                if (int.TryParse(Console.ReadLine(), out int n))
                {
                    _studentManager.InputStudents(n);
                    
                    break;
                }
                else
                {
                    Console.WriteLine("Nhap lai!!");
                }
                
                    
                
            }
            //int n = int.Parse(Console.ReadLine());
            
            
            //input fromfile
            //_studentManager.ReadStudentsFromFile();
        }
        #endregion   
        
        //read from file
        static void ReadStudentsFromFile()
        {
            Console.Write("Nhập tên file: ");
            string fileName = "students.txt"; //Console.ReadLine();
            
            //string path = "students.txt";
            // /Users/thuthao/Documents/CNTT/C_Shapre/Final_Ass/Final_Ass/students.txt
            // path
            string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "CNTT", "C_Shapre", "Final_Ass", "Final_Ass");
            
            //_studentManager.ReadStudentsFromFile( _path+ fileName);
            _studentManager.ReadStudentsFromFile(Path.Combine(_path, fileName));

        }
        // write student 

        static void WriteStudentToFile()
        {
            string fileName = "students_outPut.txt";
            string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "CNTT", "C_Shapre", "Final_Ass", "Final_Ass");
            _studentManager.WriteStudentsToFile(Path.Combine(_path, fileName));
        }
        
        
        
        
    }
}