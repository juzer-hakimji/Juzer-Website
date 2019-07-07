using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBasicOperationsUser
    {
        bool Insert(MST_UserInfo p_Obj);
        bool Update(MST_UserInfo p_Obj);
        bool Delete(int p_UserId);
    }
}
