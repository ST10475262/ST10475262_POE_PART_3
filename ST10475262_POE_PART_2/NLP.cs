using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10475262_POE_PART_2
{
    public static class NLP
    {
        public static bool IsAddTask(string input)
        {
            input = input.ToLower();

            return input.Contains("add task") ||
                   input.Contains("create task") ||
                   input.Contains("new task") ||
                   input.Contains("remember");
        }

        public static bool IsShowTasks(string input)
        {
            input = input.ToLower();

            return (input.Contains("show") ||
                    input.Contains("display") ||
                    input.Contains("view")) &&
                    input.Contains("task");
        }

        public static bool IsCompleted(string input)
        {
            input = input.ToLower();

            return (input.Contains("complete") ||
                    input.Contains("finish") &&
                    input.Contains("task"));
        }

        public static bool DeleteTasks(string input)
        {
            input = input.ToLower();

            return (input.Contains("delete") ||
                    input.Contains("remove") ||
                    input.Contains("wipe")) &&
                    input.Contains("task");
        }

        public static bool IsStartQuiz(string input)
        {
            input = input.ToLower();

            return input.Contains("start quiz") ||
                   input.Contains("quiz") ||
                   input.Contains("test me");
        }

        public static bool IsActivityLog(string input)
        {
            input = input.ToLower();

            return input.Contains("activity") ||
                   input.Contains("log") ||
                   input.Contains("what have you done");
        }
    }
}
