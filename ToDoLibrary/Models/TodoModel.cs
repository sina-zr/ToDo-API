using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary.Models
{
    public class TodoModel
    {
        public int ID { get; set; }
        public string? Task { get; set; }
        public string AssignedTo { get; set; }
        public bool IsComplete { get; set; }
    }
}
