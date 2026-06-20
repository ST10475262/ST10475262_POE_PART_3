using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10475262_POE_PART_2
{
    public class TasksDatabase
    {
        string connectionString =@"Server=(localdb)\MSSQLLocalDB; Database=PART_3_DATABASE; Trusted_Connection=True;";

        public void AddTask(TaskItem task)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Task(Title, Description, ReminderDate, IsCompleted)
                                 VALUES(@Title, @Description,@ReminderDate, @IsCompleted)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title",task.Title);
                cmd.Parameters.AddWithValue("@Description",task.Description);
                cmd.Parameters.AddWithValue("@ReminderDate",task.ReminderDate == null? DBNull.Value: task.ReminderDate);
                cmd.Parameters.AddWithValue("@IsCompleted",task.IsCompleted);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
