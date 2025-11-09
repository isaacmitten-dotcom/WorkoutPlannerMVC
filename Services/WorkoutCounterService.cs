namespace WorkoutPlannerMVC.Services
{

    public interface IWorkoutRepCounterService
    {
        void IncrementCount();
        int GetCount();
    }

    public class WorkoutRepCounterService : IWorkoutRepCounterService
    {
        private int _count = 0;

        public void IncrementCount()
        {
            _count++;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
