using System.Collections.Generic;

namespace InDevTaskManagmentTask.Serviceses
{
    public interface ITaskService
    {
        List<Task> GetTasks();
        Task GetTask(int id);
        void AddTask(Task item);
        void UpdateTask(Task item);
        void DeleteTask(int id);
        bool TaskExists(int id);
    }
}
