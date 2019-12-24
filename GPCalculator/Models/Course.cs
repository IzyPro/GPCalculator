using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GPCalculator.Models
{
    public class Course
    {
        [Key]
        public int Sn { get; set; }
        public string Name { get; set; }
        [RegularExpression("^[0-9]{1,2}$")]
        public int Unit { get; set; }
        [MaxLength(1)]
        public string Grade { get; set; }


        public int ResultId { get; set; }
        public Result Result { get; set; }
      
    }
}
