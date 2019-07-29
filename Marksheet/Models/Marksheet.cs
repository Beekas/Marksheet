using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marksheet.Models
{
    public class Marksheet
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int? SchoolId { get; set; }
        public int? PMarks { get; set; }
        public int TMarks { get; set; }
        public string PGrade { get; set; }
        public string TGrade { get; set; }
        public int AggMarks { get; set; }
        public string AggGrade { get; set; }
        public bool ResultStatus { get; set; }
        public string AcedamicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public string Term { get; set; }
        public int Attendance { get; set; }

    }
}