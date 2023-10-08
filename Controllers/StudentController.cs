using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // var students = StudentModel.GetStudents();
            var students = _context.Students.ToList();
            // Send students to the view
            // ViewBag.ListStudent = students;  //C1: ViewBag
            // ViewData["ListStudent"] = students; //C2: ViewData
            return View(students);// C1, C2: return View(); C3: return View(students);-> Model
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            return View(student);
        }
        public IActionResult Save(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}