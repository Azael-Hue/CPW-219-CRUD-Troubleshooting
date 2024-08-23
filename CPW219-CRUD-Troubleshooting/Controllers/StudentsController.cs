using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> products = StudentDb.GetStudents(_context);
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(s, _context);
                ViewData["Message"] = $"{s.Name} was added to the roster!";
                return View();
            }

            //Show web page with errors
            return View(s);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student s = StudentDb.GetStudent(_context, id);

            if (s == null)
            {
                return NotFound();
            }

            //show it on web page
            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(_context, s);
                TempData["Message"] = $"{s.Name}'s info was Updated!";
                return RedirectToAction("Index");
            }

            //return view with errors
            return View(s);
        }

        public IActionResult Delete(int id)
        {
            Student s = StudentDb.GetStudent(_context, id);
            if (s == null)
            {
                return NotFound();
            }

            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Student from database
            Student s = StudentDb.GetStudent(_context, id);
            
            if (s != null)
            {
                //Delete the student
                StudentDb.Delete(_context, s);
                TempData["Message"] = s.Name + " was deleted";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This student was already deleted";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Student s = StudentDb.GetStudent(_context, id);
            if (s == null)
            {
                return NotFound();
            }

            return View(s);
        }
    }
}
