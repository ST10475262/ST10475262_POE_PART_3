using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10475262_POE_PART_2
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReminderDate { get; set; }// ? means it's a nullable value (even though DateTime requires a value by default, making it a nullable value allows users to leave it blank/null)

        public bool IsCompleted { get; set; }
    }
}