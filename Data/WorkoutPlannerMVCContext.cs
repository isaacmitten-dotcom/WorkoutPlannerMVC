using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerMVC.Models;

namespace WorkoutPlannerMVC.Data
{
    //This is the context

    public class WorkoutPlannerMVCContext : DbContext
    {
        public WorkoutPlannerMVCContext(DbContextOptions<WorkoutPlannerMVCContext> options)
            : base(options)
        {
        }

        public DbSet<WorkoutPlannerMVC.Models.Workout> Workouts { get; set; } = default!;

        public DbSet<WorkoutPlannerMVC.Models.Exercise> Exercises { get; set; } = default!;

        public DbSet<TopExercise> TopExercises { get; set; }

        public TopExercise GetTopExercise() { 
            return Set<TopExercise>().FromSqlRaw("EXEC GetTopExercise").AsEnumerable().FirstOrDefault();  
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>()
                .HasMany(w => w.Exercises)
                .WithMany(e => e.Workouts);

            modelBuilder.Entity<TopExercise>()
                .HasNoKey();
        }
    } 
}
