using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechSupport.Models
{
    [Table("tasks", Schema = "public")]
    public class Task
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("creatorid")]
        public int CreatorId { get; set; }

        [Column("executorid")]
        public int ExecutorId { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("createddatetime")]
        public DateTime CreatedDateTime { get; set; }

        [Column("closeddatetime")]
        public DateTime ClosedDateTime { get; set; }
    }
}
