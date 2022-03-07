using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal static class UserData
    {
        public static List<User> TestUsers { get { ResetTestUserData(); return _testUsers; } set { } }
        private static List<User> _testUsers;

        static private void ResetTestUserData()
        {
            if (_testUsers == null)
            {
                _testUsers = new List<User>();
                _testUsers.Add(new User("Student1", "123456", "121219107", UserRoles.STUDENT));
                _testUsers.Add(new User("Student2", "123456", "121219108", UserRoles.STUDENT));
                _testUsers.Add(new User("Admin", "longpass", "null", UserRoles.ADMIN));

                foreach (User user in _testUsers)
                {
                    user.creation_time = DateTime.Now;
                    user.expiration_date = DateTime.MaxValue;
                }
            }
        }

        static public User IsUserPassCorrect(string username, string password)
        {
            foreach (User user in TestUsers)
            {
                if (user.username == username && user.password == password) { return user; }
            }
            return null;
        }

        static public void SetUserActiveTo(string username, DateTime expiration_date)
        {
            User user = GetUser(username);
            if (user != null)
            {
                user.expiration_date = expiration_date;
                Logger.LogActivity("Change expiration date of " + username);
            }
            else
            {
                Logger.LogActivity("Couldn't find user with username " + username);
            }
        }

        static public void AssignUserRole(string username, UserRoles role)
        {
            User user = GetUser(username);
            if (user != null)
            {
                user.user_role = role;
                Logger.LogActivity("Change role of " + username);
            }
            else
            {
                Logger.LogActivity("Couldn't find user with username " + username);
            }
        }

        static private User GetUser(string username) 
        {
            foreach (User user in TestUsers)
            {
                if (user.username == username)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
