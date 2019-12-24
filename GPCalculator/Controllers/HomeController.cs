using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPCalculator.Models;
using GPCalculator.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GPCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext context;
        private readonly ILogger<HomeController> _logger;
        private readonly IResultRepository _resultRepository;
        

        public HomeController(AppDBContext appDB, ILogger<HomeController> logger, IResultRepository resultRepository)
        {
            _logger = logger;
            _resultRepository = resultRepository;
            this.context = appDB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult ResultSheet(int Id)
        {
            var NoOfCourses = context.Course.Where(c => c.ResultId == Id).Count();
            int courseValue = 0;
            int sumOfUnits = 0;
            int totalValue = 0;

            
            foreach (var course in context.Course.Where(c => c.ResultId == Id))
            {
                if (course.Grade == "A")
                {
                    courseValue = course.Unit * 5;
                }
                else if (course.Grade == "B")
                {
                    courseValue = course.Unit * 4;
                }
                else if (course.Grade == "C")
                {
                    courseValue = course.Unit * 3;
                }
                else if (course.Grade == "D")
                {
                    courseValue = course.Unit * 2;
                }
                else if (course.Grade == "E")
                {
                    courseValue = course.Unit * 1;
                }
                else if (course.Grade == "F")
                {
                    courseValue = course.Unit * 0;
                }
                totalValue += courseValue;
            }

            foreach(var course in context.Course.Where(c => c.ResultId == Id))
            {
                sumOfUnits += course.Unit;
            }
            
            double GP = (double)totalValue / sumOfUnits;

            HomeResultSheetViewModel homeResultSheetViewModel = new HomeResultSheetViewModel()
            {
                Result = _resultRepository.GetResult(Id),
                Courses = context.Course.Where(c => c.ResultId == Id).ToList(),
                ResultId = Id,
                GP = GP,
                NoOfCourses = context.Course.Where(c => c.ResultId == Id).Count(),
                SumOfUnits = sumOfUnits,
        };
            context.SaveChanges();
            return View(homeResultSheetViewModel);
            
        }
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(Result result)
        {
            if (ModelState.IsValid)
            {
                Result newResult = _resultRepository.Add(result);
                return RedirectToAction("CourseForm", new { ResultId = newResult.Id });
            }
            return View();
        }
        public IActionResult CourseForm(int ResultId)
        {
            var model = new CourseFormViewModel()
            {
                Courses = context.Course.Where(c => c.ResultId == ResultId).ToList(),
                ResultId = ResultId,
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddFromModal(int ResultId)
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddFromModal(Course course, int ResultId)
        {
            if (ModelState.IsValid)
            {
                Result result = context.Results.SingleOrDefault(r => r.Id == course.ResultId);
                

                Course newCourse = new Course()
                {
                    Name = course.Name,
                    Unit = course.Unit,
                    Grade = course.Grade,
                    ResultId = ResultId,
                };
                context.Course.Add(newCourse);
                context.SaveChanges();
                return RedirectToAction("CourseForm", new { ResultId = newCourse.ResultId });
            }
            return View();
        }

        
        public IActionResult ResultList()
        {
            var model = _resultRepository.GetAllResult();
            return View(model);
        }

        //public IActionResult DeleteCourse(int Sn)
        //{
        //    Course course = context.Course.Find(Sn);
        //    if (course != null)
        //    {
        //        context.Course.Remove(course);
        //        context.SaveChanges();
        //    }
        //    return View(course);
        //}
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}
