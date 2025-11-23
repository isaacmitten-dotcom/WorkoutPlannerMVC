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
    public class ExercisesController : Controller
    {
        private readonly IExerciseService _exService;


        public ExercisesController(IExerciseService exService)
        {
            _exService = exService;

        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var exercises = await _exService.GetAllAsync();

            return View(exercises);
        }



        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exService.GetByIdAsync(id);
            return View(exercise);
        }

        // GET: Exercises/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Sets,Reps,Weight")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                await _exService.AddAsync(exercise);
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exService.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Sets,Reps,Weight")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _exService.UpdateAsync(exercise);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exService.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _exService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
