using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerMVC.Models;

namespace WorkoutPlannerMVC.Data
{
    public class WorkoutPlannerMVCContext : DbContext
    {
        public WorkoutPlannerMVCContext(DbContextOptions<WorkoutPlannerMVCContext> options)
            : base(options)
        {
        }

        public DbSet<WorkoutPlannerMVC.Models.Workout> Workouts { get; set; } = default!;

        public DbSet<WorkoutPlannerMVC.Models.Workout> Exercises { get; set; } = default!;

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>()
                .HasMany(w => w.Exercises)
                .WithOne(e => e.Workout)
                .HasForeignKey(e => e.WorkoutId);
        }
        public DbSet<WorkoutPlannerMVC.Models.Exercise> Exercise { get; set; } = default!;
    } 
}
