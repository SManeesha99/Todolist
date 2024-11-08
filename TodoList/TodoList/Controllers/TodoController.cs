﻿using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private static List<ToDoItem> todolist = new List<ToDoItem>();
        private static int nextId = 1;

        public IActionResult Index()
        {
            return View(todolist);
        }

        public IActionResult Create()
        {
            return View();
        }

        //handle form submission create new todo list
        [HttpPost]
        public IActionResult Create(ToDoItem todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = nextId++;
                todolist.Add(todo);

                TempData["SuccessMessage"] = "Todo item added successfully!";
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        public IActionResult Edit(int id)
        {
            var todo = todolist.FirstOrDefault(t => t.Id == id);
            if(todo == null) return NotFound();
            return View(todo);
        }

        //handle form submission edit todo list
        [HttpPost]
        public IActionResult Edit(ToDoItem updatedTodo)
        {
            var existingTodo = todolist.FirstOrDefault(t => t.Id == updatedTodo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = updatedTodo.Title;
                existingTodo.IsCompleted = updatedTodo.IsCompleted;
            }
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var todoToDelete = todolist.FirstOrDefault(t => t.Id == id);
            if (todoToDelete != null)
            {
                todolist.Remove(todoToDelete);
            }
            return RedirectToAction("Index");
        }



    }
}
