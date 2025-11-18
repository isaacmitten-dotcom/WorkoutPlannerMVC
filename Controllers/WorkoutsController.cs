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
      //  private readonly WorkoutPlannerMVCContext _context;
        private readonly ILogger<WorkoutsController> _logger;
        private readonly IWorkoutService _workoutService;
        private readonly IWorkoutRepCounterService _counterService;

        public WorkoutsController(WorkoutPlannerMVCContext context,  IWorkoutRepCounterService counterService, IWorkoutService workouts, ILogger<WorkoutsController> logger)
        {
            //  _context = context;
            _counterService = counterService;
            _workoutService = workouts;
            _logger = logger;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Workouts.Include(w => w.Exercises).ToListAsync());
            return View(await _workoutService.GetAllAsync());
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

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var workout = await _workoutService.GetByIdAsync(id);
              

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate")] Workout workout)
        {

            if (!ModelState.IsValid)
            {
                // Loop through errors
                foreach (var state in ModelState)
                {
                    string key = state.Key;
                    var errors = state.Value.Errors;

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _workoutService.AddAsync(workout);
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var workout = await _workoutService.GetByIdAsync(id);
           
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate")] Workout workout)
        {

            if (id != workout.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Loop through errors
                foreach (var state in ModelState)
                {
                    string key = state.Key;
                    var errors = state.Value.Errors;

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _workoutService.UpdateAsync(workout);
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogError("ConcurrencyExpetion");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var workout = await _workoutService.GetByIdAsync(id);

            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _workoutService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool WorkoutExists(int id)
        //{
        //    return _context.Workouts.Any(e => e.Id == id);
        //}
    }
}
