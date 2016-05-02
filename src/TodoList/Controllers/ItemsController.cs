using System.Linq;
using Microsoft.AspNet.Mvc;
using TodoList.Models;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ToDoList.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository itemRepo;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.itemRepo = new EFItemRepository();
            }
            else
            {
                this.itemRepo = thisRepo;
            }
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(itemRepo.Items.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = 1;//new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            ViewBag.CategoryId = 1;//new SelectList(db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }
    }
}
