using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using DataAccessLayer.Data_Model;
using DataAccessLayer.Database_Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Base_Classes
{
    public abstract class BaseDAL
    {
        protected DBContextResult<U> ExecuteDALMethod<T, U>(JuzerWebsiteEntities db, Func<JuzerWebsiteEntities, T, U> Func, T Obj)
        {
            try
            {
                return new DBContextResult<U>
                {
                    Data = Func(db, Obj),
                    TransactionResult = true
                };
            }
            catch (DbUpdateException ex)
            {
                new LogError(ex);
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (DbEntityValidationException ex)
            {
                new LogError(ex);
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (SqlException ex)
            {
                new LogError(ex);
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (Exception ex)
            {
                new LogError(ex);
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
        }
    }
}

