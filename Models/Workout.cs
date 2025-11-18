namespace WorkoutPlannerMVC.Models
{

    //Model for the Workout entity

    public class Workout
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();


    }
}
