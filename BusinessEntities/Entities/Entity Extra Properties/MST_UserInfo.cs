using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Entities.Entity_Model
{
    public partial class MST_UserInfo
    {
        public enum EnumUserStatus
        {
            AuthenticatedAdmin,
            AuthenticatedUser,
            NonAuthenticatedUser
        }
        public EnumUserStatus UserStatus { get; set; }
    }
}
