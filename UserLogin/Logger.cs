using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    static class Logger
    {
        static public List<string> currentSessionActivities = new List<string>();

        static public void LogActivity(string activity)
        {
            string activityLine = DateTime.Now + ";"
            + LoginValidation.currentUserUsername + ";"
            + LoginValidation.currentUserRole + ";"
            + activity;

            if (File.Exists("log.txt") == true)
            {
                File.AppendAllText("test.txt", activityLine);
            }
            else
            {
                Console.WriteLine("Please create a log.txt file (I'm limited by the technology of my time)");
            }
            currentSessionActivities.Add(activityLine);
        }
    }
}
