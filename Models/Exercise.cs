namespace WorkoutPlannerMVC.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int Weight { get; set; }

    
        //Foriegn key
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
