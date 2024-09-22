using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;
using TaskManagementApp.Repository;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository taskRepo;

        public TaskController(ITaskRepository taskRepo)
        {
            this.taskRepo = taskRepo;
        }
        public IActionResult Index()
        {
            return View(taskRepo.GetAll());
        }
        public IActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTask(UserTask newTask)
        {
            if (ModelState.IsValid)
            {
                taskRepo.Insert(newTask);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("UserId", "User Id Not Valid");
                
                return View(newTask);
            }
        }

        public IActionResult Edit(int Id)
        {
            UserTask task = taskRepo.GetById(Id);
            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(int Id,UserTask newtask)
        {
            if (ModelState.IsValid)
            {
                taskRepo.Update(Id, newtask);
                return RedirectToAction("Details", new {Id=newtask.TaskId});
            }
            return View(newtask);
        }

        public IActionResult Details(int Id)
        {
            return View(taskRepo.GetById(Id));
        }
        public IActionResult Delete(int Id)
        {
            taskRepo.Delete(Id);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmDelete(int Id)
        {

            return View(Id);
        }
    }
}
