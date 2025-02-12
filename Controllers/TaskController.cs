using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using System.Linq;
using System.Collections.Generic;

// Ensures only logged-in users can access this controller
public class TaskController : Controller
{
    private readonly ApplicationDbContext _db;

    public TaskController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Show only tasks belonging to the logged-in user
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        List<TaskItem> objTaskList = _db.Tasks.Where(t => t.UserId == userId).ToList();
        return View(objTaskList);
    }

    // GET: Create Task
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create Task
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaskItem task)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user's ID

        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "User is not logged in.");
            return View(task);
        }

        if (task.Title == task.Description)
        {
            ModelState.AddModelError("Description", "The Description cannot be similar to the Title.");
        }

        if (ModelState.IsValid)
        {
            task.UserId = userId; 
            _db.Tasks.Add(task);
            _db.SaveChanges();
            TempData["success"] = "Task Created Successfully";
            return RedirectToAction("Index");
        }
        return View(task);
    }

    // GET: Task Details (Ensure user only accesses their own tasks)
    public IActionResult Details(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var task = _db.Tasks.FirstOrDefault(t => t.TaskID == id && t.UserId == userId);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // GET: Edit Task (Ensure user only edits their own tasks)
    public IActionResult Edit(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var task = _db.Tasks.FirstOrDefault(t => t.TaskID == id && t.UserId == userId);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Edit Task
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(TaskItem task)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var existingTask = _db.Tasks.FirstOrDefault(t => t.TaskID == task.TaskID && t.UserId == userId);

        if (existingTask == null)
        {
            return NotFound();
        }

        if (task.Title == task.Description)
        {
            ModelState.AddModelError("Description", "The Description cannot be similar to the Title.");
        }

        if (ModelState.IsValid)
        {
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            _db.Tasks.Update(existingTask);
            _db.SaveChanges();
            TempData["success"] = "Task Updated Successfully";
            return RedirectToAction("Index");
        }

        return View(task);
    }

    // GET: Delete Task
    public IActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var task = _db.Tasks.FirstOrDefault(t => t.TaskID == id && t.UserId == userId);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Delete Task
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var task = _db.Tasks.FirstOrDefault(t => t.TaskID == id && t.UserId == userId);
        if (task == null)
        {
            return NotFound();
        }

        _db.Tasks.Remove(task);
        _db.SaveChanges();
        TempData["success"] = "Task Deleted Successfully";
        return RedirectToAction("Index");
    }
}
