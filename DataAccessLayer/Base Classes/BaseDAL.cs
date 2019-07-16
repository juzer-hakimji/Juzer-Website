using BusinessEntities.Entities.Entity_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base_Classes
{
    public abstract class BaseDAL
    {
        protected U ExecuteDALMethod<T,U>(JuzerWebsiteEntities db, Func<JuzerWebsiteEntities, T, U> Func, T Obj)
        {
            try
            {
                return Func(db, Obj);
            }
            catch (DbUpdateException ex)
            {
                return (U)Convert.ChangeType(false, typeof(U));
            }
            catch (DbEntityValidationException ex)
            {
                return (U)Convert.ChangeType(false, typeof(U));
            }
            catch (SqlException ex)
            {
                return (U)Convert.ChangeType(false, typeof(U));
            }
            catch (Exception ex)
            {
                return (U)Convert.ChangeType(false, typeof(U));
            }
        }
    }
}
