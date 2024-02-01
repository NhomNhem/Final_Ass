using System;

namespace Final_Ass
{
    public abstract class Student
    {

        #region Get Set
        public string HoTen { get; protected internal set; }
        public double Diem { get; protected internal set; }
        public string Email { get; protected internal set; }
        public string HocLuc { get; protected set; }
        public string MSSV { get; protected internal set; }
        #endregion

        #region Constructor
        protected Student(){}
        protected Student(string hoTen, double diem, string email)
        {
            HoTen = hoTen;
            Diem = diem;
            Email = email;
        }
        #endregion

        #region  Abstract
        public abstract string GetPerformace();
        public abstract void InputFromFile(Student student);
        abstract public void Input();

        abstract public double ReadDouble(double min, double max);
        abstract public string ReadEmail();
        public abstract void SetHocLuc();

        #endregion


        
        
        
        protected bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public override string ToString()
        {
            return $" MSSV: {MSSV}, Họ tên: {HoTen}, Điểm: {Diem}, Học Lực: {HocLuc}, Email: {Email}";
        }
    }
}