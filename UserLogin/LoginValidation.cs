using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal class LoginValidation
    {
        public static UserRoles currentUserRole { get; private set; }
        public static string currentUserUsername { get; private set; }
        public delegate void ActionOnError(string error);

        private string username;
        private string password;
        private string error; //TODO remove error field and use only delegate
        private ActionOnError error_func;

        public LoginValidation(string username, string password, ActionOnError error_func)
        {
            this.username = username;
            this.password = password;
            this.error_func = error_func;
        }

        public bool ValidateUserInput(out User user)
        {
            user = null;
            currentUserRole = UserRoles.ANONYMOUS;

            // PRIMITIVE CHECKS

            if (this.username.Equals(String.Empty))
            {
                this.error = "No username given!";
                error_func(this.error);
                return false;
            }

            if (this.username.Length < 4)
            {
                this.error = "Username too short!";
                error_func(this.error);
                return false;
            }

            if (this.password.Equals(String.Empty))
            {
                this.error = "No password given!";
                error_func(this.error);
                return false;
            }

            if (this.password.Length < 4)
            {
                this.error = "Password too short!";
                error_func(this.error);
                return false;
            }

            // TRUE CHECK

            user = UserData.IsUserPassCorrect(this.username, this.password);

            if (user == null)
            {
                this.error = "Invalid Username or Password!";
                error_func(this.error);
                return false;
            }

            currentUserRole = (UserRoles)user.user_role;
            currentUserUsername = (String)user.username;
            Logger.LogActivity("Succesful login");
            return true;
        }
    }
}
