using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marksheet.Models
{
    public class School
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string Slogan { get; set; }
        public string URL { get; set; }
        public string State { get; set; }
        public string Municipality { get; set; }
        public string WardNo { get; set; }
        public ICollection<Student> Students { get; set; }
        public string Logo { get; set; }
    }
}