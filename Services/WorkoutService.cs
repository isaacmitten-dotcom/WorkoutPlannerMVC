using WorkoutPlannerMVC.Models;
using WorkoutPlannerMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlannerMVC.Services
{

    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetAllAsync();

        Task<Workout> GetByIdAsync(int? id);

        Task AddAsync(Workout workout);

        Task DeleteAsync(int? id);

        Task UpdateAsync(Workout workout);


    }


    public class WorkoutService : IWorkoutService
    {
        private readonly WorkoutPlannerMVCContext _context;

        public WorkoutService(WorkoutPlannerMVCContext context) { _context = context; }

        public async Task AddAsync(Workout workout)
        {
            _context.Workouts.Add(workout);
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


        public async Task<Workout> GetByIdAsync(int? id) => await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        

        public async Task UpdateAsync(Workout workout)
        {
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();
        }
    }
}
