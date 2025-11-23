using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerMVC.Models.ViewModels
{
    public class WorkoutVm
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]

        public string? Description { get; set; }

        [Required]

        public DateTime? StartDate { get; set; }

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

        public List<SelectListItem>? AvailableExercises { get; set; }

        public List<int> SelectedExerciseIds { get; set; } = new List<int>();
    }
}
