using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerMVC.Data;
using WorkoutPlannerMVC.Models;
using WorkoutPlannerMVC.Services;

namespace WorkoutPlannerMVC.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly WorkoutPlannerMVCContext _context;
        private readonly IWorkoutRepCounterService _counterService;

        public WorkoutsController(WorkoutPlannerMVCContext context, IWorkoutRepCounterService counterService)
        {
            _context = context;
            _counterService = counterService;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Workouts.Include(w => w.Exercises).ToListAsync());
        }

        public IActionResult IncrementReps()
        {
          _counterService.IncrementCount();
            return RedirectToAction(nameof(Reps));
        }


        public async Task<IActionResult> Reps() {
            
            var reps = _counterService.GetCount();
            
            return View(reps);
        
        }

        //These are commented out to remain within the scope of this week.
        //Crud will be created next week and moved into the service folder.

        //    // GET: Workouts/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var workout = await _context.Workouts
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (workout == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(workout);
        //    }

        //    // GET: Workouts/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Workouts/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate")] Workout workout)
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            // Loop through errors
        //            foreach (var state in ModelState)
        //            {
        //                string key = state.Key;
        //                var errors = state.Value.Errors;

        //                foreach (var error in errors)
        //                {
        //                    Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
        //                }
        //            }
        //        }

        //            if (ModelState.IsValid)
        //        {
        //            _context.Add(workout);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(workout);
        //    }

        //    // GET: Workouts/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var workout = await _context.Workouts.FindAsync(id);
        //        if (workout == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(workout);
        //    }

        //    // POST: Workouts/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate")] Workout workout)
        //    {
        //        if (id != workout.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            // Loop through errors
        //            foreach (var state in ModelState)
        //            {
        //                string key = state.Key;
        //                var errors = state.Value.Errors;

        //                foreach (var error in errors)
        //                {
        //                    Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
        //                }
        //            }
        //        }

        //            if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(workout);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!WorkoutExists(workout.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(workout);
        //    }

        //    // GET: Workouts/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var workout = await _context.Workouts
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (workout == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(workout);
        //    }

        //    // POST: Workouts/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var workout = await _context.Workouts.FindAsync(id);
        //        if (workout != null)
        //        {
        //            _context.Workouts.Remove(workout);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool WorkoutExists(int id)
        //    {
        //        return _context.Workouts.Any(e => e.Id == id);
        //    }
    }
}
