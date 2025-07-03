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
            // Load data once on application start
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo fileInfo = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                    if (worksheet == null)
                    {
                        Console.WriteLine($"Worksheet '{sheetName}' not found.");
                        return students;
                    }

                    int rowCount = worksheet.Dimension?.Rows ?? 0;

                    for (int row = 2; row <= rowCount; row++) // Start from row 2 (assuming row 1 is headers)
                    {
                        try
                        {
                            Student student = new Student
                            {
                                Serial = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                                TermNo = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                                // Kept as string
                                StudentName = worksheet.Cells[row, 4].Value?.ToString() ?? "",
                                Father = worksheet.Cells[row, 5].Value?.ToString() ?? "",
                                Teacher = worksheet.Cells[row, 6].Value?.ToString() ?? "",
                                Class = worksheet.Cells[row, 8].Value?.ToString() ?? "",
                                Month1 = worksheet.Cells[row, 9].Value?.ToString() ?? "",
                                Month2 = worksheet.Cells[row, 10].Value?.ToString() ?? "",
                                Written = worksheet.Cells[row, 11].Value?.ToString() ?? "",
                                Wordlist = worksheet.Cells[row, 12].Value?.ToString() ?? "",
                                Viva = worksheet.Cells[row, 13].Value?.ToString() ?? "",
                                Present = worksheet.Cells[row, 14].Value?.ToString() ?? "",
                                conversation = worksheet.Cells[row, 15].Value?.ToString() ?? "",
                                SpontenousCom = worksheet.Cells[row, 16].Value?.ToString() ?? "",
                                GroupTaskSurpriseTest = worksheet.Cells[row, 17].Value?.ToString() ?? "",
                                Debate = worksheet.Cells[row, 18].Value?.ToString() ?? "",
                                Performance = worksheet.Cells[row, 19].Value?.ToString() ?? "",
                                BookReview = worksheet.Cells[row, 20].Value?.ToString() ?? "",
                                Attendance = worksheet.Cells[row, 21].Value?.ToString() ?? "",
                                Assignment = worksheet.Cells[row, 22].Value?.ToString() ?? "",
                                Total = double.TryParse(worksheet.Cells[row, 24].Value?.ToString(), out double total) ? total : 0.0,
                                Obtained = double.TryParse(worksheet.Cells[row, 25].Value?.ToString(), out double obtained) ? obtained : 0.0,
                                Percentage = double.TryParse(worksheet.Cells[row, 27].Value?.ToString(), out double percentage)
                                    ? Math.Round(percentage, 0).ToString("0") + "%"  // Round off to no decimal
                                    : "0%", // Ensure default format
                                Result = worksheet.Cells[row, 28].Value?.ToString() ?? "",
                            
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

            var studentResults = _students
                .Where(s => s.StudentName.Contains(studentName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (studentResults == null || studentResults.Count == 0)
            {
                ViewBag.ErrorMessage = "No student found with that name.";
                return View("Index");
            }

            return View("SearchResults", studentResults);
        }
    }
}
