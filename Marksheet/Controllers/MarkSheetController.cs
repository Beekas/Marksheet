using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marksheet.Models;
using Marksheet.DAL;
using Marksheet.ViewModels;

namespace Marksheet.Controllers
{
    public class MarkSheetController : Controller
    {
        private readonly MarkSheetEntities db;

        public MarkSheetController(MarkSheetEntities _db)
        {
            db = _db;
        }
        // GET: MarkSheet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectSchool()
        {
            ViewBag.SchoolId = new SelectList(db.Schools.ToList(), "Id", "SchoolName");
            return View();  
        }
        public ActionResult AddEdit(string SchoolId)
        {
            if (string.IsNullOrEmpty(SchoolId))
            {
                Response.Redirect("SelectSchool");
            }
            int schoolId = Convert.ToInt32(SchoolId);

            IEnumerable<Student> students = db.Schools.Find(schoolId).Students.ToList();

            AcademicYear year = db.AcademicYears.FirstOrDefault(m=>m.ActiveYear);

            IEnumerable<Subject> subjects = db.OptionalSubjects.Where(m => m.SchoolId == schoolId).Select(m => m.Subject);
            string optional1 = "Optional1";
            string optional2 = "Optional2";
            bool IsPractical1 = false;
            bool IsPractical2=false;
            if (subjects.Any())
            {
                optional1 = subjects.First().SubName;
                IsPractical1 = subjects.First().HasPractical;
                optional2 = subjects.Last().SubName;
                IsPractical2 = subjects.Last().HasPractical;
            }
            List<MarksheetVM> marks = new List<MarksheetVM>();

            foreach (var item in students)
            {
                MarksheetVM objMarkSheet = new MarksheetVM();
                objMarkSheet.StudentId = item.Id;
                objMarkSheet.StudentName = item.StudentName;
                objMarkSheet.SchoolId = schoolId;
                objMarkSheet.AcedamicYearId = year.Id.ToString();
                objMarkSheet.Opt1HasPractical = IsPractical1;
                objMarkSheet.Opt2HasPractical = IsPractical2;
                marks.Add(objMarkSheet);
            }

            return View(marks);
        }

        [HttpPost]
        public ActionResult AddEdit(List<MarksheetVM> marks, FormCollection col)
        {
            if (ModelState.IsValid)
            {
                School school = db.Schools.Find(marks[0].SchoolId.Value);
                AcademicYear year = db.AcademicYears.Find(marks[0].AcedamicYearId);
                string term = col["Term"].ToString();
                bool HasPractical1 = marks[0].Opt1HasPractical;
                bool HasPractical2 = marks[0].Opt2HasPractical;


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
                }


                return RedirectToAction("Index");
            }
            return View(marks);
        }
    }
}