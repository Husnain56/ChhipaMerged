using Chhipa_Motors.DL;
using Chhipa_Motors.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.BL
{
    public class UserBL
    {
        private UserDL _userDL;

        public UserBL()
        {
            _userDL = new UserDL();
        }

        public UserDTO GetUserInfo(string userId)
        {
            return _userDL.GetUserInfo(userId);
        }

        public bool VerifyPassword(UserDTO userDTO)
        {
            return _userDL.VerifyPassword(userDTO);
        }

        public bool UsernameExists(string username)
        {
            return _userDL.UsernameExists(username);
        }

        public int UpdateUsername(string userId, string newUsername)
        {
            return _userDL.UpdateUsername(userId, newUsername);
        }

        public int DeleteUser(string userId)
        {
            return _userDL.DeleteUser(userId);
        }
    }
}
