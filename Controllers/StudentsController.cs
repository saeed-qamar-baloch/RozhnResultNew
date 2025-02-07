using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Result.Models;

namespace Result.Controllers
{
    public class StudentsController : Controller
    {
        private static List<Student> _students; // Store students

        public StudentsController()
        {
            // Load data (ideally, do this once on application start)
            if (_students == null)
            {
                string excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", "StudentsResult.xlsx");

                _students = LoadStudentData(excelFilePath, "Sheet1");
            }
        }

        private List<Student> LoadStudentData(string filePath, string sheetName)
        {
            List<Student> students = new List<Student>();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                    if (worksheet == null)
                    {
                        Console.WriteLine($"Worksheet '{sheetName}' not found.");
                        return students; // Return empty list if worksheet not found
                    }

                    int rowCount = worksheet.Dimension?.Rows ?? 0;

                    for (int row = 2; row <= rowCount; row++) // Start from row 2
                    {
                        try
                        {
                            Student student = new Student
                            {
                                Serial = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                                TermNo = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                                Date = DateTime.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out DateTime dateValue) ? dateValue : DateTime.Now,
                                StudentName = worksheet.Cells[row, 4].Value?.ToString() ?? "",
                                Father = worksheet.Cells[row, 5].Value?.ToString() ?? "",
                                Teacher = worksheet.Cells[row, 6].Value?.ToString() ?? "",
                                Class = worksheet.Cells[row, 8].Value?.ToString() ?? "",
                                Month1 = double.TryParse(worksheet.Cells[row, 9].Value?.ToString(), out double m1) ? m1 : 0,
                                Month2 = double.TryParse(worksheet.Cells[row, 10].Value?.ToString(), out double m2) ? m2 : 0,
                                Written = double.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out double written) ? written : 0,
                                Wordlist = double.TryParse(worksheet.Cells[row, 12].Value?.ToString(), out double wordlist) ? wordlist : 0,
                                Viva = double.TryParse(worksheet.Cells[row, 13].Value?.ToString(), out double viva) ? viva : 0,
                                PresentConver = double.TryParse(worksheet.Cells[row, 14].Value?.ToString(), out double presentConver) ? presentConver : 0,
                                AttendBookReview = double.TryParse(worksheet.Cells[row, 15].Value?.ToString(), out double attendBookReview) ? attendBookReview : 0,
                                AssignmentFacilitators = worksheet.Cells[row, 16].Value?.ToString() ?? "",
                                Total = double.TryParse(worksheet.Cells[row, 21].Value?.ToString(), out double total) ? total : 0,
                                Obtained = double.TryParse(worksheet.Cells[row, 22].Value?.ToString(), out double obtained) ? obtained : 0,
                                Percentage = worksheet.Cells[row, 23].Value?.ToString() ?? "",
                                Result = worksheet.Cells[row, 24].Value?.ToString() ?? "",
                                Grade = worksheet.Cells[row, 25].Value?.ToString() ?? "",
                          
                            };

                            students.Add(student);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing row {row}: {ex.Message}");
                        }
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }

            return students;
        }

        public IActionResult Index()
        {
            return View(); // Return the search form
        }

        [HttpPost]
        public IActionResult Search(string studentName)
        {
            if (string.IsNullOrEmpty(studentName))
            {
                return View("Index"); // Or display an error message
            }

            // Search by first name or last name
            var studentResults = _students.Where(s =>
                s.StudentName.Contains(studentName, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (studentResults == null || studentResults.Count == 0)
            {
                ViewBag.ErrorMessage = "No student found with that name.";
                return View("Index");
            }

            return View("SearchResults", studentResults); // Pass the list to a new view
        }
    }
}
