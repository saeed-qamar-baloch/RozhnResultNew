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
                        Serial = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                        TermNo = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                        Date = DateTime.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? DateTime.Now.ToString()),
                        StudentName = worksheet.Cells[row, 4].Value?.ToString() ?? "",
                        Father = worksheet.Cells[row, 5].Value?.ToString() ?? "",
                        Teacher = worksheet.Cells[row, 6].Value?.ToString() ?? "",
                        Class = worksheet.Cells[row, 7].Value?.ToString() ?? "",
                        Month1 = double.TryParse(worksheet.Cells[row, 8].Value?.ToString(), out double m1) ? m1 : 0,
                        Month2 = double.TryParse(worksheet.Cells[row, 9].Value?.ToString(), out double m2) ? m2 : 0,
                        Written = double.TryParse(worksheet.Cells[row, 10].Value?.ToString(), out double written) ? written : 0,
                        Wordlist = double.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out double wordlist) ? wordlist : 0,
                        Viva = double.TryParse(worksheet.Cells[row, 12].Value?.ToString(), out double viva) ? viva : 0,
                        PresentConver = double.TryParse(worksheet.Cells[row, 13].Value?.ToString(), out double presentConver) ? presentConver : 0,
                        AttendBookReview = double.TryParse(worksheet.Cells[row, 14].Value?.ToString(), out double attendBookReview) ? attendBookReview : 0,
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
