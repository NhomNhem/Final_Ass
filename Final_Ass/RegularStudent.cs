using System;
namespace Final_Ass
{
    public class RegularStudent : Student, IClassification
    {
        public RegularStudent() : base()
        {}
        public RegularStudent(string hoTen, double diem, string email) : base(hoTen, diem, email)
        {
        }

        public override string GetPerformace()
        {
            return GetClassification(Diem);
        }

        public override void SetHocLuc()
        {
            HocLuc = GetClassification(Diem);
        }

        public string GetClassification(double _ponit)
        {
            if (_ponit < 3) return "Yeu";
            else if (_ponit < 5) return "Yeu";
            else if (_ponit < 6.5) return "Trung Binh";
            else if (_ponit < 7.5) return "Kha";
            else if (_ponit < 9) return "Gioi";
            else return "Xuat Sac";
        }
        public override void Input()
        {
            Console.Write("Nhập họ và tên (từ 16 đến 40 ký tự): ");
            HoTen = Console.ReadLine();

            Console.Write("Nhập điểm (từ 0 đến 10): ");
            Diem = ReadDouble(0,10);

            Console.Write("Nhập email (từ 8 đến 50 ký tự, phải có '@' và '.'): ");
            Email = ReadEmail();
        }
        
        public override void InputFromFile(Student student)
        {
        }
        
        public override double ReadDouble(double min, double max)
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double result))
                {
                    if (result >= min && result <= max)
                        return result;

                    Console.WriteLine($"Điểm phải từ {min} đến {max}. Vui lòng nhập lại.");
                }
                else
                {
                    Console.WriteLine("Điểm không hợp lệ. Vui lòng nhập lại.");
                }
            }
        }
        
        public override string ReadEmail()
        {
            while (true)
            {
                string email = Console.ReadLine();

                try
                {
                    if (IsValidEmail(email))
                        return email;

                    Console.WriteLine("Email không hợp lệ. Vui lòng nhập lại.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }
        }
    }
}