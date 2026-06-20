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

        public List<TaskItem> GetTasks()//uses list to avoid tasks overwriting one another
        {
            List<TaskItem> tasks = new List<TaskItem>();//uses list instead of arrays to avoid a fixed size

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Task";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TaskItem task = new TaskItem();

                    task.Id = Convert.ToInt32(reader["Id"]);

                    task.Title = reader["Title"].ToString();

                    task.Description = reader["Description"].ToString();

                    if (reader["ReminderDate"] != DBNull.Value)
                    {
                        task.ReminderDate = Convert.ToDateTime(reader["ReminderDate"]);
                    }

                    task.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);

                    tasks.Add(task);
                }
            }

            return tasks;
        }
    }
}
