using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBasicOperationsUser
    {
        DBContextResult<MST_UserInfo> Insert(MST_UserInfo p_Obj);
        DBContextResult<object> Update(MST_UserInfo p_Obj);
        DBContextResult<object> Delete(int p_UserId);
    }
}
