using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo_List.Models
{
    public class TodoList
    {

        [Key]
        public int TodoListID { get; set; }
        public string Title { get; set; }

        //Navigation Properties
        public virtual ICollection<TodoItem> TodoItems { get; set; }

    }
}