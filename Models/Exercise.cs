namespace WorkoutPlannerMVC.Models
{
    public class Exercise
    {
        //Model for the Exercise entity

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int Weight { get; set; }


        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
