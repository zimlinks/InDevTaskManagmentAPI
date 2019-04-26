using System.Collections.Generic;
using System.Linq;

namespace InDevTaskManagmentTask.Serviceses
{
    public class TaskService : ITaskService
    {
        private readonly List<Task> Tasks;

        public TaskService()
        {
            this.Tasks = new List<Task>
            {
                new Task { Id = 1, TaskName = "Default Task 1", AssignedTo = "Ali Mwanza", Assignedby = "InDev", ProjectMembers  = 1, TaskDetails = "Hardcorded Task 1. Create API ." },
                new Task { Id = 2, TaskName = "Default Task 2", AssignedTo = "Ali Mwanza", Assignedby = "InDev", ProjectMembers  = 2, TaskDetails = "Hardcorded Task 1. Intergrate ERP System." },

            };
        }

        public List<Task> GetTasks()
        {
            return this.Tasks.ToList();
        }

        public Task GetTask(int id)
        {
            return this.Tasks.Where(m => m.Id == id).FirstOrDefault();
        }

        public void AddTask(Task item)
        {
            this.Tasks.Add(item);
        }

        public void UpdateTask(Task item)
        {
            var taskset = this.Tasks.Where(m => m.Id == item.Id).FirstOrDefault();

            taskset.TaskName = item.TaskName;
            taskset.ProjectMembers = item.ProjectMembers;
            taskset.TaskDetails = item.TaskDetails;
        }

        public void DeleteTask(int id)
        {
            var taskset = this.Tasks.Where(m => m.Id == id).FirstOrDefault();

            this.Tasks.Remove(taskset);
        }

        public bool TaskExists(int id)
        {
            return this.Tasks.Any(m => m.Id == id);
        }
    }
}
