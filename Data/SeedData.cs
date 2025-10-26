using System;
using WorkoutPlannerMVC.Models;

namespace WorkoutPlannerMVC.Data
{
    public class SeedData
    {
        public static void Initialize(WorkoutPlannerMVCContext context)
        {
            if (!context.Workouts.Any())
            {
                var workout = new Workout
                {
                    Name = "Push Day",
                    Exercises = new List<Exercise>
                {
                    new Exercise { Name = "Bench Press", Sets = 4, Reps = 10, Weight = 100 },
                    new Exercise { Name = "Overhead Press", Sets = 3, Reps = 8, Weight = 1000 }
                }
                };

                context.Workouts.Add(workout);
                context.SaveChanges();
            }
        }
    }
}
