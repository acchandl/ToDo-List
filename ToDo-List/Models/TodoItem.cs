using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDo_List.Models
{
    public class TodoItem
    {

        [Key]
        public int TodoItemID { get; set; }

        [Required]
        public string ItenName { get; set; }
        public string ItemNote { get; set; }
        public int Priority { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateCompleted { get; set; }


       //unneccessary (below) if we're using Navigation Properties with only one foreign key
       //if we were doing a dropdown list then we would need the foreign key
       //strings can inheritanly be null
        //[ForeignKey()]
       //public int TodoListID { get; set; }

        //Navigation properties
        public virtual TodoList TodoList { get; set; }


    }
}