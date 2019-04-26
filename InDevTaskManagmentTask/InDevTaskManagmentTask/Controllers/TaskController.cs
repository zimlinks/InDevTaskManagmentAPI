using InDevTaskManagmentTask.Libraries;
using InDevTaskManagmentTask.Models;
using InDevTaskManagmentTask.Serviceses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InDevTaskManagmentTask.Controllers
{
   
    public class TaskController : BaseController
    {
        private readonly ITaskService service;

        public TaskController(ITaskService service)
        {
            this.service = service;
        }


        // This method is used to send values to the set Destination. It starts by checking if the entries are empty.
        // If not empty then checks if the entry is valid. If All condition pass it add to the List node   
        [HttpPost]
        [Route("AddNewTask")]
        public IActionResult Create([FromBody]TaskInputModel taskGetReturn)
        {
            if (taskGetReturn == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return Unprocessable(ModelState);

            var taskset = ToDomainModel(taskGetReturn);
            service.AddTask(taskset);

            var taskSetReturn = ToOutputModel(taskset);
            return CreatedAtRoute("GetTask", new { id = taskSetReturn.Id }, taskSetReturn);
        }

       
        // This method is used to edit values to the set Destination.
        // It starts by checking if the entries are empty or if it has a invalid Id.If All condition pass it add the update information to the List node 
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]TaskInputModel taskGetReturn)
        {
            if (taskGetReturn == null || id != taskGetReturn.Id)
                return BadRequest();

            if (!service.TaskExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            var taskset = ToDomainModel(taskGetReturn);
            service.UpdateTask(taskset);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(
            int id, [FromBody]JsonPatchDocument<TaskInputModel> patch)
        {
            if (patch == null)
                return BadRequest();

            var taskset = service.GetTask(id);
            if (taskset == null)
                return NotFound();

            var taskGetReturn = ToInputModel(taskset);
            patch.ApplyTo(taskGetReturn);

            TryValidateModel(taskGetReturn);
            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            taskset = ToDomainModel(taskGetReturn);
            service.UpdateTask(taskset);

            return NoContent();
        }

        // This method is used to Deleting values from the destination.

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.TaskExists(id))
                return NotFound();

            service.DeleteTask(id);

            return NoContent();
        }


        // This method is used to List all values from the set Destination.
        [HttpGet]
        [Route("ShowAllTasks")]
        public IActionResult Get()
        {
            var taskset = service.GetTasks();

            var taskSetReturn = ToOutputModel(taskset);
            return Ok(taskSetReturn);
        }

        // This method is used to List only one task values from the set Destination.
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult Get(int id)
        {
            var taskset = service.GetTask(id);
            if (taskset == null)
                return NotFound();

            var taskSetReturn = ToOutputModel(taskset);
            return Ok(taskSetReturn);
        }


        #region " Mappings "

        private TaskOutputModel ToOutputModel(Task taskset)
        {
            return new TaskOutputModel
            {
                Id = taskset.Id,
                TaskName = taskset.TaskName,
                ProjectMembers = taskset.ProjectMembers,
                AssignedTo = taskset.AssignedTo,
                Assignedby = taskset.Assignedby,
                TaskDetails = taskset.TaskDetails,
                TaskAssigningDate = DateTime.Now,
                TaskCompletingDate = DateTime.Now.ToString("dddd, dd MMMM yyyy")



            };
        }

        private List<TaskOutputModel> ToOutputModel(List<Task> taskset)
        {
            return taskset.Select(item => ToOutputModel(item))
                        .ToList();
        }

        private Task ToDomainModel(TaskInputModel taskGetReturn)
        {
            return new Task
            {
                Id = taskGetReturn.Id,
                TaskName = taskGetReturn.TaskName,
                AssignedTo = taskGetReturn.AssignedTo,
                Assignedby = taskGetReturn.Assignedby,
                ProjectMembers = taskGetReturn.ProjectMembers,
                TaskDetails = taskGetReturn.TaskDetails

              

            };
        }

        private TaskInputModel ToInputModel(Task taskset)
        {
            return new TaskInputModel
            {
                Id = taskset.Id,
                TaskName = taskset.TaskName,
                AssignedTo = taskset.AssignedTo,
                Assignedby = taskset.Assignedby,
                ProjectMembers = taskset.ProjectMembers,
                TaskDetails = taskset.TaskDetails
            };
        }
        
        #endregion
    }
}
