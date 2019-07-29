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

            List<Student> students = db.Schools.Find(schoolId).Students.ToList();

            AcademicYear year = db.AcademicYears.FirstOrDefault(m=>m.ActiveYear);

            List<Subject> subjects = new List<Subject>();

            subjects.Concat(db.Subjects.ToList());
            

            List<MarksheetVM> marks = new List<MarksheetVM>();
            for (int i = 0; i < students.Count; i++)
            {
                MarksheetVM objMarkSheet = new MarksheetVM();
                objMarkSheet.StudentId = students[i].Id;
                objMarkSheet.StudentName = students[i].StudentName;
                

                
            }

            return View();
        }
    }
}