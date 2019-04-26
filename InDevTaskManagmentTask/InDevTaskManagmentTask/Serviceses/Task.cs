namespace InDevTaskManagmentTask.Serviceses
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public string Assignedby { get; set; }
        public int ProjectMembers { get; set; }                          
        public string TaskDetails { get; set; }

    }
}
