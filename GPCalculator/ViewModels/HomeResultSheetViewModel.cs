using GPCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPCalculator.ViewModels
{
    public class HomeResultSheetViewModel
    {
        public Course Course { get; set; }
        public Result Result { get; set; }
        public ICollection<Course> Courses { get; set; }
        public int ResultId { get; set; }
        public double GP { get; set; }
        public int SumOfUnits { get; set; }
        public int NoOfCourses { get; set; }
    }

}
