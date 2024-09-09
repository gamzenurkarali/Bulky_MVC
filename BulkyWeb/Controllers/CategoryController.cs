using Bulky.DataAccess;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name==obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name.");
            //}
            //if (obj.Name.ToLower()=="test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}
            if (ModelState.IsValid) 
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb=_context.Categories.Find(id);
            //Category? categoryFromDb1=_context.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2=_context.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _context.Categories.Find(id);
            //Category? categoryFromDb1=_context.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2=_context.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
            
        }
    }
}
