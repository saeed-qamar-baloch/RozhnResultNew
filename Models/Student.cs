using OfficeOpenXml;

namespace Result.Models
{
    public class Student
    {
        public string Serial { get; set; }
        public string TermNo { get; set; }
        public DateTime Date { get; set; }
        public string StudentName { get; set; }
        public string Father { get; set; }
        public string Teacher { get; set; }
        public string Class { get; set; }
        public double Month1 { get; set; }
        public double Month2 { get; set; }
        public double Written { get; set; }
        public double Wordlist { get; set; }
        public double Viva { get; set; }
        public double PresentConver { get; set; }
        public double AttendBookReview { get; set; }
        public string AssignmentFacilitators { get; set; }
        public double Total { get; set; }
        public double Obtained { get; set; }
        public string Percentage { get; set; }
        public string Result { get; set; }
        public string PassingPercentage { get; set; }
        public string Grade { get; set; }

        }
    
}
