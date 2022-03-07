using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string faculty_num { get; set; }
        public UserRoles user_role { get; set; }
        public DateTime creation_time { get; set; }
        public DateTime expiration_date { get; set; }

        public User(string username, string password, string faculty_num, UserRoles user_role)
        {
            this.username = username;
            this.password = password;
            this.faculty_num = faculty_num;
            this.user_role = user_role;
        }
    }
}
