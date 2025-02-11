using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;

public class TaskController : Controller
{
    private readonly ApplicationDbContext _db;

    public TaskController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<TaskItem> objTaskList = _db.Tasks.ToList();
        return View(objTaskList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaskItem task)
    {
        if (task.Title == task.Description)
        {
            ModelState.AddModelError("Description", "The Description cannot be similar to the title");
        }

        if (ModelState.IsValid)
        {
            _db.Tasks.Add(task);
            _db.SaveChanges();
            TempData["success"]="Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    public IActionResult Details(int id)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.TaskID == id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    // GET Edit Action
    public IActionResult Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var task = _db.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST Edit Action
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(TaskItem task)
    {
        if (task.Title == task.Description)
        {
            ModelState.AddModelError("Description", "The Description cannot be similar to the title");
        }

        if (ModelState.IsValid)
        {
            _db.Tasks.Update(task);  
            _db.SaveChanges();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    // GET Delete Action
    public IActionResult Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var task = _db.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST Delete Action
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id)
    {
        var task = _db.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        _db.Tasks.Remove(task);  // This will delete the task from the database
        _db.SaveChanges();       // Save the changes after deletion
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");  // Redirect to the task list
    }

}
