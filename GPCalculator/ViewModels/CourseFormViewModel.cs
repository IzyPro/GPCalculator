using GPCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPCalculator.ViewModels
{
    public class CourseFormViewModel
    {
        public int ResultId { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
