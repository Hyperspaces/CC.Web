using CC.Web.Dao.Core;
using CC.Web.Model.System;
using System.Linq;

namespace CC.Web.Dao.System
{
    public class UserDao : BaseDao<User>, IUserDao
    {
        public User FindUserByNameAndPwd(string userName, string passWord)
        {
            return Repository.TableNoTracking
                .Where(t => t.UserName == userName &&
                t.PassWord == passWord)
                .FirstOrDefault();
        }

        public User FindUserByName(string userName)
        {
            return Repository.TableNoTracking
                .Where(t => t.UserName == userName)
                .FirstOrDefault();
        }
    }
}
