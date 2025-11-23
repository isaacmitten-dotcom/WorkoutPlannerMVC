using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerMVC.Data;
using WorkoutPlannerMVC.Models;
using WorkoutPlannerMVC.Models.ViewModels;

namespace WorkoutPlannerMVC.Services
{

    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetAllAsync();

        Task<List<SelectListItem>> GetAvailableExercisesAsync();

        Task<Workout> CreateWorkoutAsync(WorkoutVm vm);
        Task UpdateWorkoutAsync(WorkoutVm vm);
        Task<WorkoutVm> GetWorkoutVmAsync(int? id);

        Task<Workout> GetByIdAsync(int? id);

        Task DeleteAsync(int? id);
    }


    public class WorkoutService : IWorkoutService


    {
        private readonly WorkoutPlannerMVCContext _context;

        public WorkoutService(WorkoutPlannerMVCContext context) { _context = context; }


        public async Task<WorkoutVm> GetWorkoutVmAsync(int? id)
        {
            Workout? workout = null;

            if (id.HasValue)
            {
                workout = await _context.Workouts
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync(w => w.Id == id.Value);
            }
            else
            {
                //Make a new workout instance when being used without an Id
                workout = new Workout();
            }

                var availableExercises = await _context.Exercises
                    .OrderBy(e => e.Name)
                    .ToListAsync();

            var vm = new WorkoutVm
            {
                Name = workout.Name,
                Description = workout.Description,
                StartDate = workout.StartDate,
                Exercises = workout.Exercises,

                //A list of all the exercises already selected
                SelectedExerciseIds = workout.Exercises.Select(e => e.Id).ToList(),

                //Converts availableExercises to a SelectListItem
                AvailableExercises = availableExercises
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Name,
                        Selected = workout?.Exercises.Any(ex => ex.Id == e.Id) ?? false
                    }).ToList()
            };

            return vm;
        }

        public async Task<Workout> CreateWorkoutAsync(WorkoutVm vm)
        {
            var workout = new Workout
            {
                Name = vm.Name,
                Description = vm.Description,   
                StartDate = vm.StartDate,
            };

            var exerciseIds = vm.SelectedExerciseIds;

            if (exerciseIds != null && exerciseIds.Count != 0)
            {
                var exercises = await _context.Exercises
                    .Where(e => exerciseIds.Contains(e.Id))
                    .ToListAsync();

                foreach (var exercise in exercises)
                {
                    workout.Exercises.Add(exercise);
                }
            }

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }
        public async Task UpdateWorkoutAsync(WorkoutVm vm)
        {
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.Id == vm.Id);

            if (workout == null)
                throw new Exception("Workout not found");

            workout.Name = vm.Name;
            workout.Description = vm.Description;
            workout.StartDate = vm.StartDate;

           
            workout.Exercises.Clear();

            if (vm.SelectedExerciseIds != null)
            {
                var selectedExercises = await _context.Exercises
                    .Where(e => vm.SelectedExerciseIds.Contains(e.Id))
                    .ToListAsync();

                workout.Exercises = selectedExercises;
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            var entity = await _context.Workouts.FindAsync(id);
            if (entity is null) return;
            _context.Workouts.Remove(entity);   
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Workout>> GetAllAsync() => await _context.Workouts.Include(w => w.Exercises).ToListAsync();

        public async Task<List<SelectListItem>> GetAvailableExercisesAsync()
        {
         var exercises = await _context.Exercises
            .OrderBy(e => e.Name)
            .ToListAsync();

            //Return a SelectListTtem to be used in the view
            return exercises.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
        }

        public async Task<Workout> GetByIdAsync(int? id) => 
            await _context.Workouts
            .Include(w => w.Exercises)
            .FirstOrDefaultAsync(w => w.Id == id);
        
    }
}
