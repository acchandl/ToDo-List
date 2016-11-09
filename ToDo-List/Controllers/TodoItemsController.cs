using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo_List.Models;

namespace ToDo_List.Controllers
{
    public class TodoItemsController : Controller
    {
        private ToDo_ListContext db = new ToDo_ListContext();

        // GET: TodoItems
        public ActionResult Index()
        {
            return View(db.TodoItems.ToList());
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // GET: TodoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        
        //
        //
        //get rid of something that you dont want to create..so date created and date completed
        public ActionResult Create([Bind(Include = "TodoItemID,ItenName,ItemNote,Priority,DueDate")] TodoItem todoItem)
        {

            //
            //
            //will put a timestamp for the date created
            todoItem.DateCreate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.TodoItems.Add(todoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TodoItemID,ItenName,ItemNote,Priority,DueDate")] TodoItem todoItem)
        {


            //tries to combine and create an entire new variable from the bind items


            //look up the item thats currently in the database
            var existingTodoItem = db.TodoItems.Find(todoItem.TodoItemID);


            //added this (below)
            if (existingTodoItem == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                //we dont want to save the thing they passed in
                //we dont want to update database, we want to replace it, so the below is commented out
                //db.Entry(todoItem).State = EntityState.Modified;
              //look and see what the person did and stick it in the spot in the database
              //the listed means that we dont want the user to change the stuff listed below
                existingTodoItem.ItenName = todoItem.ItenName;
                existingTodoItem.ItemNote = todoItem.ItemNote;
                existingTodoItem.Priority = todoItem.Priority;
                existingTodoItem.DueDate = todoItem.DueDate;
                //LEAVE existing.TodoItem.DateCreate and DateComplete UNTOUCHED!!!

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }



        //

        //This is copied and pasted from above. for the check box action




        // POST: TodoItems/Finish/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finish(int? id)
        {

            var existingTodoItem = db.TodoItems.Find(id);


            //added this (below-not found conditional)
            if (existingTodoItem == null)
            {
                return HttpNotFound();
            }

            
            existingTodoItem.DateCompleted = DateTime.Now;

            //if (ModelState.IsValid)
            //{
            //   existingTodoItem.DateCompleted = db.TodoItems.
               
                db.SaveChanges();
            
                return RedirectToAction("Index");
            }

        //














        // POST: TodoItems/Unfinish/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unfinish(int? id)
        {

            var existingTodoItem = db.TodoItems.Find(id);


            //added this (below-not found conditional)
            if (existingTodoItem == null)
            {
                return HttpNotFound();
            }

            //when this to do item is not done
            existingTodoItem.DateCompleted = null;


            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: TodoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            db.TodoItems.Remove(todoItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
