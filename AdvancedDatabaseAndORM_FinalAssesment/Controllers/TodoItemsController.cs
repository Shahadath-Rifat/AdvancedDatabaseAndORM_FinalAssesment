using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvancedDatabaseAndORM_FinalAssesment.Data;
using AdvancedDatabaseAndORM_FinalAssesment.Models;

namespace AdvancedDatabaseAndORM_FinalAssesment.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly AdvancedDatabaseAndORM_FinalAssesmentContext _context;

        public TodoItemsController(AdvancedDatabaseAndORM_FinalAssesmentContext context)
        {
            _context = context;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            var items = await _context.TodoItem
                .Include(t => t.TodoList)
                .OrderByDescending(t => t.Priority)  // Order by Priority (High to Low)
                .ThenByDescending(t => t.DateOfCreation)  // Then order by DateOfCreation (Newest to Oldest)
                .ToListAsync();

            return View(items);
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.TodoList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            ViewData["TodoListId"] = new SelectList(_context.TodoList, "Id", "Title");
            return View();
        }

        // POST: TodoItems/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DateOfCreation,Priority,Description,IsCompleted,TodoListId")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TodoListId"] = new SelectList(_context.TodoList, "Id", "Title", todoItem.TodoListId);
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            ViewData["TodoListId"] = new SelectList(_context.TodoList, "Id", "Title", todoItem.TodoListId);
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateOfCreation,Priority,Description,IsCompleted,TodoListId")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TodoListId"] = new SelectList(_context.TodoList, "Id", "Title", todoItem.TodoListId);
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.TodoList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TodoItem == null)
            {
                return Problem("Entity set 'AdvancedDatabaseAndORM_FinalAssesmentContext.TodoItem'  is null.");
            }
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItem.Remove(todoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
          return (_context.TodoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
