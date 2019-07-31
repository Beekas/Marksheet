using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marksheet.Models;
using Marksheet.DAL;
using Marksheet.ViewModels;
using System.Data.Entity;

namespace Marksheet.Controllers
{
    public class MarkSheetController : Controller
    {
        private string[] Subjects = new string[] { "English", "Nepali", "Mathematics", "Science", "Social Studies", "Health", "Optional1", "Optional2" };
        private readonly MarkSheetEntities db = new MarkSheetEntities();

        // GET: MarkSheet
        public ActionResult Index()
        {
            IEnumerable<Models.Marksheet> marksheets = db.Marksheets.Include(m => m.Student).Include(m => m.AcademicYear).ToList();
            return View(marksheets);
        }

        public ActionResult ViewMarkSheet(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Error!";
                return RedirectToAction("Index");
            }

            Models.Marksheet marksheet = db.Marksheets.Find(id);
            MarksVM marksvm = new MarksVM();
            List<SubjectMarkVM> lstSubject = new List<SubjectMarkVM>();


            if (marksheet != null)
            {
                if (!db.ActiveDays.Any(m => m.SchoolId == marksheet.Student.SchoolId &&
                                         m.AcademicYearId == marksheet.AcedamicYearId &&
                                         m.TerminalName == marksheet.Term))
                {
                    TempData["ErrorMessage"] = "Error! No active days set.";
                    return RedirectToAction("Index");
                }
                marksvm.StudentName = marksheet.Student.StudentName;
                marksvm.TerminalExam = marksheet.Term;
                marksvm.DOB = marksheet.Student.DOB.ToString("yyyy-mm-dd");
                marksvm.RollNo = "";// marksheet.Student.RollNo;
                marksvm.SchoolName = marksheet.Student.School.SchoolName;
                marksvm.SchoolAddress = marksheet.Student.School.Municipality;
                marksvm.FatherName = marksheet.Student.FatherName;
                marksvm.MotherName = marksheet.Student.MotherName;
                marksvm.StudentAddress = marksheet.Student.Address;
                marksvm.PresentDay = marksheet.Attendance.ToString();
                marksvm.AcademicYear = marksheet.AcademicYear.Year;

                marksvm.AcademicDay = db.ActiveDays.FirstOrDefault(m => m.SchoolId == marksheet.Student.SchoolId &&
                                        m.AcademicYearId == marksheet.AcedamicYearId &&
                                        m.TerminalName == marksheet.Term).Activeday.ToString();

                foreach (string item in Subjects)
                {
                    SubjectMarkVM objSubj = new SubjectMarkVM();
                    string caseSwitch = item;

                    switch (caseSwitch)
                    {
                        case "English":
                            objSubj.SerialNo = "01";
                            objSubj.SubjectName = item;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.EnglishTM.ToString();
                            objSubj.Practical = marksheet.EnglishPM.ToString();
                            objSubj.Total = (marksheet.EnglishPM + marksheet.EnglishTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Nepali":
                            objSubj.SerialNo = "02";
                            objSubj.SubjectName = item;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.NepaliTM.ToString();
                            objSubj.Practical = "";
                            objSubj.Total = (marksheet.NepaliTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Mathematics":
                            objSubj.SerialNo = "03";
                            objSubj.SubjectName = item;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.MathTM.ToString();
                            objSubj.Total = (marksheet.MathTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Science":
                            objSubj.SerialNo = "04";
                            objSubj.SubjectName = item;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.ScienceTM.ToString();
                            objSubj.Practical = marksheet.SciencePM.ToString();
                            objSubj.Total = (marksheet.SciencePM + marksheet.ScienceTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Social Studies":
                            objSubj.SerialNo = "05";
                            objSubj.SubjectName = item;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.SocialTM.ToString();
                            objSubj.Practical = "";
                            objSubj.Total = (marksheet.SocialTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Health":
                            objSubj.SerialNo = "06";
                            objSubj.SubjectName = "Health, Pop & Env Edu";
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.HealthTM.ToString();
                            objSubj.Practical = marksheet.HealthPM.ToString();
                            objSubj.Total = (marksheet.HealthPM + marksheet.HealthTM).ToString();
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Optional1":
                            bool haspractical = db.OptionalSubjects.Include(m => m.Subject).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.HasPractical;
                            objSubj.SerialNo = "07";
                            objSubj.SubjectName =  db.OptionalSubjects.Include(m => m.Subject).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.SubName;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.Optional1TM.ToString();
                            objSubj.Practical = "";
                            if (haspractical)
                            {
                                objSubj.Practical = marksheet.Optional1PM.ToString();
                            }
                            if (haspractical)
                            {
                                objSubj.Total = (marksheet.Optional1PM + marksheet.Optional1TM).ToString();

                            }
                            else
                            {
                                objSubj.Total = (marksheet.Optional1TM).ToString();

                            }
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Optional2":
                            bool haspractical2 = db.OptionalSubjects.Include(m => m.Subject).OrderByDescending(s => s.Id).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.HasPractical;
                            objSubj.SerialNo = "08";
                            objSubj.SubjectName =  db.OptionalSubjects.Include(m => m.Subject).OrderByDescending(s => s.Id).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.SubName;
                            objSubj.FullMarks = "100";
                            objSubj.PassMarks = "40";
                            objSubj.Theory = marksheet.Optional2TM.ToString();
                            objSubj.Practical = "";
                            if (haspractical2)
                            {
                                objSubj.Practical = marksheet.Optional2PM.ToString();
                            }
                            if (haspractical2)
                            {
                                objSubj.Total = (marksheet.Optional2PM + marksheet.Optional2TM).ToString();

                            }
                            else
                            {
                                objSubj.Total = (marksheet.Optional2TM).ToString();

                            }
                            if (Convert.ToInt32(objSubj.Total) < 32)
                            {
                                objSubj.Remarks = "F";
                            }
                            lstSubject.Add(objSubj);

                            break;
                        default:

                            break;
                    }
                }
                marksvm.Subjects = lstSubject;
                if (lstSubject.Any(m => m.Remarks == "F"))
                {
                    marksvm.Division = "Fail";
                }
                if (marksvm.Division == "Fail")
                {
                    marksvm.GrandTotal = "";
                    marksvm.Percentage = "";
                }
                else
                {
                    marksvm.GrandTotal = lstSubject.Sum(m => int.Parse(m.Total)).ToString();
                    marksvm.Percentage = (Convert.ToDecimal(marksvm.GrandTotal) / lstSubject.Sum(m => int.Parse(m.FullMarks))).ToString("0.00");
                    if (Convert.ToInt32(marksvm.GrandTotal) >= 640)
                    {
                        marksvm.Division = "First Division with Distinction";
                    }
                    else if (Convert.ToDecimal(marksvm.GrandTotal) < 640 && Convert.ToDecimal(marksvm.GrandTotal) >= 480)
                    {
                        marksvm.Division = "First Division";
                    }
                    else if (Convert.ToDecimal(marksvm.GrandTotal) < 480 && Convert.ToDecimal(marksvm.GrandTotal) >= 360)
                    {
                        marksvm.Division = "Second Division";
                    }
                    else if (Convert.ToDecimal(marksvm.GrandTotal) < 360 && Convert.ToDecimal(marksvm.GrandTotal) >= 256)
                    {
                        marksvm.Division = "Third Division";
                    }
                }
                return View(marksvm);

            }
            TempData["ErrorMessage"] = "Error!";
            return RedirectToAction("Index");
        }

        public ActionResult ViewGradeSheet(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Error!";
                return RedirectToAction("Index");
            }

            Models.Marksheet marksheet = db.Marksheets.Find(id);
            GradeVM marksvm = new GradeVM();
            List<SubjectVM> lstSubject = new List<SubjectVM>();


            if (marksheet != null)
            {
                if (!db.ActiveDays.Any(m => m.SchoolId == marksheet.Student.SchoolId &&
                                         m.AcademicYearId == marksheet.AcedamicYearId &&
                                         m.TerminalName == marksheet.Term))
                {
                    TempData["ErrorMessage"] = "Error! No active days set.";
                    return RedirectToAction("Index");
                }

                marksvm.StudentName = marksheet.Student.StudentName;
                marksvm.TerminalExam = marksheet.Term;
                marksvm.DOB = marksheet.Student.DOB.ToString("yyyy-mm-dd");
                marksvm.RollNo = "";
                marksvm.SchoolName = marksheet.Student.School.SchoolName;
                marksvm.SchoolAddress = marksheet.Student.School.Municipality;

                marksvm.FatherName = marksheet.Student.FatherName;
                marksvm.MotherName = marksheet.Student.MotherName;
                marksvm.StudentAddress = marksheet.Student.Address;
                marksvm.PresentDay = marksheet.Attendance.ToString();
                marksvm.AcademicYear = marksheet.AcademicYear.Year;
                marksvm.AcademicDay = db.ActiveDays.FirstOrDefault(m => m.SchoolId == marksheet.Student.SchoolId &&
                                        m.AcademicYearId == marksheet.AcedamicYearId &&
                                        m.TerminalName == marksheet.Term).Activeday.ToString();

                foreach (string item in Subjects)
                {
                    SubjectVM objSubj = new SubjectVM();
                    objSubj.CreditHour = "4";
                    string caseSwitch = item;

                    switch (caseSwitch)
                    {
                        case "English":
                            objSubj.SerialNo = "01";
                            objSubj.SubjectName = item;
                            objSubj.Theory =findGrade(marksheet.EnglishTM, 75) ;
                            objSubj.Practical =findGrade(marksheet.EnglishPM,25);
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.EnglishPM + marksheet.EnglishTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.EnglishPM + marksheet.EnglishTM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Nepali":
                            objSubj.SerialNo = "02";
                            objSubj.SubjectName = item;
                            objSubj.Theory = findGrade(marksheet.NepaliTM, 100);
                            objSubj.Practical = "";
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.NepaliTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.NepaliTM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Mathematics":
                            objSubj.SerialNo = "03";
                            objSubj.SubjectName = item;
                            objSubj.Theory = findGrade(marksheet.MathTM, 100);
                            objSubj.Practical = "";
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.MathTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.MathTM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Science":
                            objSubj.SerialNo = "04";
                            objSubj.SubjectName = item;
                            objSubj.Theory = findGrade(marksheet.ScienceTM, 75);
                            objSubj.Practical = findGrade(marksheet.SciencePM, 25);
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.SciencePM + marksheet.ScienceTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.ScienceTM + marksheet.SciencePM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Social Studies":
                            objSubj.SerialNo = "05";
                            objSubj.SubjectName = item;
                            objSubj.Theory = findGrade(marksheet.SocialTM, 100);
                            objSubj.Practical = "";
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.SocialTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.SocialTM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Health":
                            objSubj.SerialNo = "06";
                            objSubj.SubjectName = "Health, Pop & Env Edu";
                            objSubj.Theory = findGrade(marksheet.HealthTM, 75);
                            objSubj.Practical = findGrade(marksheet.HealthPM, 25);
                            objSubj.Remarks = "";
                            objSubj.FinalGrade = findGrade((marksheet.HealthPM + marksheet.HealthTM), 100);
                            objSubj.GradePoint = findGradePoint(marksheet.HealthPM + marksheet.HealthTM);
                            lstSubject.Add(objSubj);

                            break;
                        case "Optional1":
                            bool haspractical = db.OptionalSubjects.Include(m => m.Subject).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.HasPractical;
                            objSubj.SerialNo = "07";
                            objSubj.SubjectName =  db.OptionalSubjects.Include(m => m.Subject).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.SubName;
                            objSubj.CreditHour = "4";
                            objSubj.Remarks = "";
                            if (haspractical)
                            {
                                int practicalMark = db.OptionalSubjects.Include(m => m.Subject).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.PFullMarks.Value;
                                objSubj.Practical =findGrade(marksheet.Optional1PM,practicalMark);
                                objSubj.Theory = findGrade(marksheet.Optional1TM, (100- practicalMark));
                                objSubj.FinalGrade = findGrade(marksheet.Optional1PM + marksheet.Optional1TM, 100);
                                objSubj.GradePoint = findGradePoint(marksheet.Optional1PM + marksheet.Optional1TM);

                            }
                            else
                            {
                                objSubj.Practical = "";
                                objSubj.Theory = findGrade(marksheet.Optional1TM, (100));
                                objSubj.FinalGrade = findGrade( marksheet.Optional1TM, 100);
                                objSubj.GradePoint = findGradePoint( marksheet.Optional1TM);
                            }
                            lstSubject.Add(objSubj);

                            break;
                        case "Optional2":
                            bool haspractical2 = db.OptionalSubjects.Include(m => m.Subject).OrderByDescending(s => s.Id).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.HasPractical;
                            objSubj.SerialNo = "08";
                            objSubj.SubjectName =  db.OptionalSubjects.Include(m => m.Subject).OrderByDescending(s => s.Id).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.SubName;
                            objSubj.CreditHour = "4";
                            objSubj.Remarks = "";
                            if (haspractical2)
                            {
                                int practicalMark = db.OptionalSubjects.Include(m => m.Subject).OrderByDescending(s => s.Id).FirstOrDefault(m => m.SchoolId == marksheet.SchoolId).Subject.PFullMarks.Value;
                                objSubj.Practical = findGrade(marksheet.Optional2PM, practicalMark);
                                objSubj.Theory = findGrade(marksheet.Optional2TM, (100 - practicalMark));
                                objSubj.FinalGrade = findGrade(marksheet.Optional2PM + marksheet.Optional2TM, 100);
                                objSubj.GradePoint = findGradePoint(marksheet.Optional2PM + marksheet.Optional2TM);

                            }
                            else
                            {
                                objSubj.Practical = "";
                                objSubj.Theory = findGrade(marksheet.Optional2TM, (100));
                                objSubj.FinalGrade = findGrade(marksheet.Optional2TM, 100);
                                objSubj.GradePoint = findGradePoint(marksheet.Optional2TM);
                            }
                            lstSubject.Add(objSubj);

                            break;
                        default:

                            break;
                    }
                }

               marksvm.GPA = lstSubject.Sum(m => Decimal.Parse(m.GradePoint)/8).ToString("0.00");
                marksvm.Subjects = lstSubject;
                return View(marksvm);

            }
            TempData["ErrorMessage"] = "Error!";
            return RedirectToAction("Index");
        }

        public string findGrade(int marks, int fullmarks)
        {
            decimal percentage =Convert.ToDecimal(marks)/fullmarks * 100;
            
                if (percentage >= 90)
                {
                    return "A+";
                }
                else if (percentage < 90 && percentage >= 80)
                {
                    return "A";
                }
                else if (percentage < 80 && percentage >= 70)
                {
                    return "B+";
                }

                else if (percentage < 70 && percentage >= 60)
                {
                    return "B";
                }

                else if (percentage < 60 && percentage >= 50)
                {
                    return "C+";
                }

                else if (percentage < 50 && percentage >= 40)
                {
                    return "C";
                }

                else if (percentage < 40 && percentage >= 30)
                {
                    return "D+";
                }

                else if (percentage < 30 && percentage >= 20)
                {
                    return "D";
                }

                else if (percentage < 20 && percentage > 0)
                {
                    return "E";
                }
                else if (percentage == 0)
                {
                    return "N";
                }
            return null;

        }

        public string findGradePoint(int marks)
        {
            decimal percentage = marks;

            if (percentage >= 90)
            {
                return "4.0";
            }
            else if (percentage < 90 && percentage >= 80)
            {
                return "3.6";
            }
            else if (percentage < 80 && percentage >= 70)
            {
                return "3.2";
            }

            else if (percentage < 70 && percentage >= 60)
            {
                return "2.8";
            }

            else if (percentage < 60 && percentage >= 50)
            {
                return "2.4";
            }

            else if (percentage < 50 && percentage >= 40)
            {
                return "2";
            }

            else if (percentage < 40 && percentage >= 30)
            {
                return "1.6";
            }

            else if (percentage < 30 && percentage >= 20)
            {
                return "1.2";
            }

            else if (percentage < 20 && percentage > 0)
            {
                return "0.8";
            }
            else if (percentage == 0)
            {
                return "0.0";
            }
            return "0.0";

        }



    public ActionResult SelectSchool()
    {
        if (db.Schools.Any())
        {
            ViewBag.SchoolId = new SelectList(db.Schools.ToList(), "Id", "SchoolName");
            return View();
        }
        else
        {
            TempData["ErrorMessage"] = "No record found for school.";
            return RedirectToAction("Index", "School");
        }

    }
    public ActionResult AddEdit(string SchoolId)
    {
        if (string.IsNullOrEmpty(SchoolId))
        {
            Response.Redirect("SelectSchool");
        }
        int schoolId = Convert.ToInt32(SchoolId);

        List<SelectListItem> terminals = new List<SelectListItem> {
                new SelectListItem{Text="First",Value="First" },new SelectListItem{Text="Second",Value="Second" },new SelectListItem{Text="Final",Value="Final"}
            };

        IEnumerable<Student> students = db.Students.Where(m => m.SchoolId == schoolId);

        if (!students.Any())
        {
            TempData["ErrorMessage"] = "No record found for students.";
            Response.Redirect("SelectSchool");
        }
        AcademicYear year = db.AcademicYears.FirstOrDefault(m => m.ActiveYear);
        if (!db.AcademicYears.Any(m => m.ActiveYear))
        {
            TempData["ErrorMessage"] = "No Active academic year found.Please make one of them active.";
            Response.Redirect("SelectSchool");
        }

        IEnumerable<Subject> subjects = db.OptionalSubjects.Where(m => m.SchoolId == schoolId).Select(m => m.Subject);
        string optional1 = "Optional1";
        string optional2 = "Optional2";
        bool IsPractical1 = false;
        bool IsPractical2 = false;
        if (subjects.ToList().Count == 2)
        {

            optional1 = subjects.First().SubName;
            IsPractical1 = subjects.First().HasPractical;
            optional2 = subjects.Reverse().First().SubName;
            IsPractical2 = subjects.Reverse().First().HasPractical;
        }
        else
        {
            TempData["ErrorMessage"] = "No record found for optional subject. Select at least two optional subject for selected school.";
            Response.Redirect("SelectSchool");
        }


        List<MarksheetVM> marks = new List<MarksheetVM>();

        foreach (var item in students)
        {
            MarksheetVM objMarkSheet = new MarksheetVM();
            objMarkSheet.StudentId = item.Id;
            objMarkSheet.StudentName = item.StudentName;
            objMarkSheet.SchoolId = schoolId;
            objMarkSheet.AcedamicYearId = year.Id;
            objMarkSheet.Opt1HasPractical = IsPractical1;
            objMarkSheet.Opt2HasPractical = IsPractical2;
            marks.Add(objMarkSheet);
        }
        marks[0].Terminal = new SelectList(terminals, "Value", "Text");
        marks[0].AcademicYear = year.Year;
        marks[0].School = db.Schools.Find(schoolId).SchoolName;
        marks[0].Optional1Name = optional1;
        marks[0].Optional2Name = optional2;
        return View(marks);
    }


    [HttpPost]
    public ActionResult AddEdit(List<MarksheetVM> marks, FormCollection col)
    {
        List<SelectListItem> terminals = new List<SelectListItem> {
                                                    new SelectListItem{Text="First",Value="First" },
                                                     new SelectListItem{Text="Second",Value="Second" },
                                                    new SelectListItem{Text="Final",Value="Final"}
                                                    };
        marks[0].Terminal = new SelectList(terminals, "Value", "Text");
        if (ModelState.IsValid)
        {
            School school = db.Schools.Find(marks[0].SchoolId.Value);
            AcademicYear year = db.AcademicYears.Find(marks[0].AcedamicYearId);
            string term = col["Term"].ToString();
            if (string.IsNullOrEmpty(term))
            {

                ViewBag.ErrorMessage = "Please Select Term.";
                return View(marks);
            }
            bool HasPractical1 = marks[0].Opt1HasPractical;
            bool HasPractical2 = marks[0].Opt2HasPractical;

            if (!db.Marksheets.Any(m => m.Term == term && m.AcedamicYearId == year.Id && m.SchoolId == school.Id))
            {
                foreach (var item in marks.AsEnumerable())
                {
                    Models.Marksheet objmark = new Models.Marksheet();
                    objmark.StudentId = item.StudentId;
                    objmark.Student = db.Students.Find(item.StudentId);
                    objmark.SchoolId = school.Id;
                    objmark.AcademicYear = year;
                    objmark.AcedamicYearId = year.Id;
                    objmark.Term = term;
                    objmark.Attendance = item.Attendance;
                    objmark.EnglishPM = item.EnglishPM;
                    objmark.EnglishTM = item.EnglishTM;
                    objmark.NepaliTM = item.NepaliTM;
                    objmark.MathTM = item.MathTM;
                    objmark.SocialTM = item.SocialTM;
                    objmark.HealthPM = item.HealthPM;
                    objmark.HealthTM = item.HealthTM;
                    objmark.SciencePM = item.SciencePM;
                    objmark.ScienceTM = item.ScienceTM;
                    objmark.Optional1TM = item.Optional1TM;
                    objmark.Optional2TM = item.Optional2TM;
                    objmark.HasPractical1 = item.Opt1HasPractical;
                    objmark.HasPractical2 = item.Opt2HasPractical;
                    if (item.Opt1HasPractical)
                    {
                        objmark.Optional1PM = item.Optional1PM;
                    }
                    if (item.Opt2HasPractical)
                    {
                        objmark.Optional2PM = item.Optional2PM;
                    }
                    db.Marksheets.Add(objmark);
                    db.SaveChanges();
                }
            }
            else
            {

                ViewBag.ErrorMessage = "Marks already inserted for selected school and selected terminal exam.";
                return View(marks);
            }
            return RedirectToAction("Index");
        }
        ViewBag.ErrorMessage = "Error! Something Went Wrong.";
        return View(marks);
    }




}
}