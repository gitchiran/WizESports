using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wiz_eSports_Management.Common
{
    public static class SessionUser
    {
        public static LoggedUser GetUser(ISession session)
        {
            LoggedUser user = null;
            if (session != null && session.Keys.Contains("LoggedUser"))
            {
                user = session.GetSession<LoggedUser>("LoggedUser");
            }
            return user;
        }

        public static int GetUserRoleId(ISession session)
        {
            int role = 0;
            if (session != null && session.Keys.Contains("LoggedUser"))
            {
                var user = session.GetSession<LoggedUser>("LoggedUser");
                if(user != null)
                {
                    role = user.RoleId;
                }
            }
            return role;
        }

        public static int GetUserId(ISession session)
        {
            int userId = 0;
            if (session != null && session.Keys.Contains("LoggedUser"))
            {
                var user = session.GetSession<LoggedUser>("LoggedUser");
                if (user != null)
                {
                    userId = user.UserId;
                }
            }
            return userId;
        }
    }
}
