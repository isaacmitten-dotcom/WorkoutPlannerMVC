using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerMVC.Models
{
    public class Exercise
    {
        //Model for the Exercise entity

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? Description { get; set; }

        [Required]
        public int Sets { get; set; }

        [Required]
        public int Reps { get; set; }

        [Required]
        public int Weight { get; set; }


        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
