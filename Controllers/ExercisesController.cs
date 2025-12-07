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
        private readonly ILogger<ExercisesController> _logger;



        public ExercisesController(IExerciseService exService, ILogger<ExercisesController> logger)
        {
            _exService = exService;
            _logger = logger;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var exercises = await _exService.GetAllAsync();

            return View(exercises);
        }

        public async Task<IActionResult> TopExercise()
        {
            var topExercise = _exService.GetTopExercise();

            return View(topExercise);
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


            //Force an invalid model state to test the logging
            //ModelState.AddModelError("Test", "For testing");

            if (ModelState.IsValid)
            {
                await _exService.AddAsync(exercise);

                _logger.LogInformation("Exercise created {@logObject}", new { 
                    Action = "ExerciseCreate",
                    Success = true,
                    ExerciseId = exercise.Id,
                    ExerciseName = exercise.Name,
                    ResponseId  = HttpContext.TraceIdentifier
                });

                return RedirectToAction("Index");
            }

            _logger.LogError("Exercise could not be created {@logObject}", new
            {
                Action = "ExerciseCreate",
                Success = false,
                ExerciseName = exercise.Name,
                ResponseId = HttpContext.TraceIdentifier
            });
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

                    _logger.LogInformation("Exercise was updated {@logObject}", new
                    {
                        Action = "ExerciseEdit",
                        Success = true,
                        ExerciseId = exercise.Id,
                        ExerciseName = exercise.Name,
                        ResponseId = HttpContext.TraceIdentifier
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Exercise could not be updated {@logObject}", new
            {
                Action = "ExerciseEdit",
                Success = false,
                ExerciseId = exercise.Id,
                ExerciseName = exercise.Name,
                ResponseId = HttpContext.TraceIdentifier
            });

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

            try
            {
                await _exService.DeleteAsync(id);

                _logger.LogInformation("Exercise was deleted {@logObject}", new
                {
                    Action = "ExerciseDelete",
                    Success = true,
                    ExerciseId = id,
                    ResponseId = HttpContext.TraceIdentifier
                });
            }

            catch
            {
                _logger.LogError("Exercise could not be deleted {@logObject}", new
                {
                    Action = "ExerciseDelete",
                    Success = false,
                    ExerciseId = id,
                    ResponseId = HttpContext.TraceIdentifier
                });
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
