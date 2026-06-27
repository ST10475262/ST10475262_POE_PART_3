using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10475262_POE_PART_2
{
    public static class ActivityLogger
    {
        private static List<string> logs = new List<string>();

        public static void Add(string activity)
        {
            logs.Add($"[{DateTime.Now:HH:mm:ss}] {activity}");
        }

        public static List<string> GetRecent()
        {
            return logs.TakeLast(10).ToList();
        }
    }
}
