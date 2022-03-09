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
        static public string log_path = "C:\\Users\\danim\\source\\repos\\PS-project\\UserLogin\\log.txt";

        static public void LogActivity(string activity)
        {
            string activityLine = DateTime.Now + ";"
            + LoginValidation.currentUserUsername + ";"
            + LoginValidation.currentUserRole + ";"
            + activity + "\n";

            if (File.Exists(log_path) == true)
            {
                File.AppendAllText(log_path, activityLine);
            }
            else
            {
                Console.WriteLine("Please create a log.txt file");
            }
            currentSessionActivities.Add(activityLine);
        }

        static public void RemoveBeforeDate(DateTime date)
        {
            try
            {
                List<string> line_list = new List<string>();
                using (StreamReader sr = new StreamReader(log_path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (DateTime.Compare(Convert.ToDateTime(line.Substring(0, line.IndexOf(";"))),date) > 0){
                            line_list.Add(line);
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(log_path, false))
                {
                    foreach(string line in line_list)
                    {
                        sw.WriteLine(line);
                    }
                }

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void CreateFileIfNotExists()
        {
            //TODO implement
        }
    }
}
