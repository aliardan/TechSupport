using System;

namespace TechSupport.Models
{
    public class CreateTaskData
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int ExecutorId { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ClosedDateTime { get; set; }
    }
}