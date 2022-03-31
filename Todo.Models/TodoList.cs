using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoList
    {
        [Key]
        public int Todo_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDatetime { get; set; } = DateTime.Now;
        [DisplayName("Updated Date")]
        public DateTime UpdatedDatetime { get; set; } = DateTime.Now;
    }
}
