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
        public string Month1 { get; set; }
        public string Month2 { get; set; }
        public string Written { get; set; }
        public string Wordlist { get; set; }
        public string Viva { get; set; }
        public string PresentConver { get; set; }
        public string AttendBookReview { get; set; }

        public string SpontenComGroupTask { get; set; }
        public string Debate { get; set; }
        public string AssignmentFacilitators { get; set; }
        public double Total { get; set; }
        public double Obtained { get; set; }
        public string Percentage { get; set; }
        public string Performance { get; set; }
        public string Result { get; set; }
        public string PassingPercentage { get; set; }
        public string Grade { get; set; }

        public static List<Student> ReadStudentsFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
            List<Student> students = new List<Student>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip headers
                {
                    Student student = new Student
                    {
                       
                       
                       
                        AssignmentFacilitators = worksheet.Cells[row, 15].Value?.ToString() ?? "",
                        Total = double.TryParse(worksheet.Cells[row, 16].Value?.ToString(), out double total) ? total : 0,
                        Obtained = double.TryParse(worksheet.Cells[row, 17].Value?.ToString(), out double obtained) ? obtained : 0,
                        Percentage = worksheet.Cells[row, 18].Value?.ToString() ?? "",
                        Result = worksheet.Cells[row, 19].Value?.ToString() ?? "",
                        PassingPercentage = worksheet.Cells[row, 20].Value?.ToString() ?? "",
                        Grade = worksheet.Cells[row, 21].Value?.ToString() ?? ""
                    };

                    students.Add(student);
                }
            }

            return students;
        }
    }
}
