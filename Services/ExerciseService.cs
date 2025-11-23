using WorkoutPlannerMVC.Data;
using WorkoutPlannerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlannerMVC.Services
{
    public interface IExerciseService
    {
        Task<IEnumerable<Exercise>> GetAllAsync();

        Task<Exercise> GetByIdAsync(int? id);

        Task AddAsync(Exercise exercise);

        Task DeleteAsync(int? id);

        Task UpdateAsync(Exercise exercise);

    }


    public class ExerciseService : IExerciseService
    {
        private readonly WorkoutPlannerMVCContext _context;

        public ExerciseService(WorkoutPlannerMVCContext context) { _context = context; }

        public async Task AddAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            var entity = await _context.Exercises.FindAsync(id);
            if (entity is null) return;
            _context.Exercises.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync() => await _context.Exercises.ToListAsync();


        public async Task<Exercise> GetByIdAsync(int? id)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }
    }
}
