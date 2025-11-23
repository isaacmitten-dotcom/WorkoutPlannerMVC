using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerMVC.Data;
using WorkoutPlannerMVC.Models;
using WorkoutPlannerMVC.Models.ViewModels;
using WorkoutPlannerMVC.Services;

namespace WorkoutPlannerMVC.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly ILogger<WorkoutsController> _logger;
        private readonly IWorkoutService _workoutService;
        private readonly IWorkoutRepCounterService _counterService;

        public WorkoutsController(WorkoutPlannerMVCContext context, IWorkoutRepCounterService counterService, IWorkoutService workouts, ILogger<WorkoutsController> logger)
        {
            _counterService = counterService;
            _workoutService = workouts;
            _logger = logger;
        }


        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            return View(await _workoutService.GetAllAsync());
        }

        public IActionResult IncrementReps()
        {
            _counterService.IncrementCount();
            return RedirectToAction(nameof(Reps));
        }


        public IActionResult Reps()
        {

            var reps = _counterService.GetCount();

            return View(reps);
        }



        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var workout = await _workoutService.GetByIdAsync(id);


            return View(workout);
        }

        // GET: Workouts/Create
        public async Task<IActionResult> Create()
        {
            var vm = await _workoutService.GetWorkoutVmAsync(null);

            return View(vm);
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutVm vm)
        {

            if (!ModelState.IsValid)
            {
                vm.AvailableExercises = await _workoutService.GetAvailableExercisesAsync();
                return View(vm);
            }


            await _workoutService.CreateWorkoutAsync(vm);
            return RedirectToAction(nameof(Index));

        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = await _workoutService.GetWorkoutVmAsync(id);

            return View(vm);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkoutVm vm)
        {

            if (id != vm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState Invalid");
                return View(vm);
            }


            try
            {
                await _workoutService.UpdateWorkoutAsync(vm);
            }

            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("ConcurrencyExpetion");
            }
            return RedirectToAction(nameof(Index));

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
    }
}
