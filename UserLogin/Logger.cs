using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    public static class Logger
    {
        static public List<string> currentSessionActivities = new List<string>();
        static public string log_path = "C:\\Users\\danim\\source\\repos\\PS-project\\UserLogin\\log.txt";

        static public void LogActivity(string activity)
        {
            string activityLine = DateTime.Now + ";"
            + LoginValidation.currentUserUsername + ";"
            + LoginValidation.currentUserRole + ";"
            + activity + "\n";

            currentSessionActivities.Add(activityLine);

            if (File.Exists(log_path) == true)
            {
                File.AppendAllText(log_path, activityLine);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Throws exception if file couldnt be opened
        /// </summary>
        /// <param name="date">Before what date (day) should the log be erased</param>
        static public void RemoveBeforeDate(DateTime date)
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

        /// <summary>
        /// Get activities from the current program run (stored in memory) based on a filter
        /// </summary>
        /// <param name="filter">What string(char) should the current activity contain, to be returned</param>
        /// <returns></returns>
        public static IEnumerable<string> GetCurrentSessionActivities(string filter)
        {
            return (from activity in currentSessionActivities where activity.Contains(filter) select activity);
        }

        public static IEnumerable<string> GetLogFileActivities()
        {
            List<string> log = new List<string>();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(log_path))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        log.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                log.Add("The file could not be read:");
                log.Add(e.Message);
            }
            return log;
        }

        public static void CreateFileIfNotExists()
        {
            //TODO implement
        }
    }
}
