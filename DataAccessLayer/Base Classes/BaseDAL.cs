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
        public bool ExecuteDALMethod<T>(JuzerWebsiteEntities db, Func<JuzerWebsiteEntities, T, bool> Func, T Obj)
        {
            try
            {
                return Func(db, Obj);
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
