using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
using DataAccessLayer.Data_Model;
using DataAccessLayer.Database_Utilities.Enums;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Spending : BaseDAL, IDisposable
    {
        JuzerWebsiteEntities db;

        protected Abstract_Spending()
        {
            db = new JuzerWebsiteEntities();
        }

        protected DBContextResult<List<usp_GetExpenseList_Result>> SelectExpense(int p_UserId)
        {
            return ExecuteDALMethod(db, (DataContext, P_UserId) =>
            {
                return DataContext.usp_GetExpenseList(P_UserId).ToList();
            }, p_UserId);
        }

        protected DBContextResult<List<usp_GetIncomesList_Result>> SelectIncome(int p_UserId)
        {
            return ExecuteDALMethod(db, (DataContext, P_UserId) =>
            {
                return DataContext.usp_GetIncomesList(P_UserId).ToList();
            }, p_UserId);
        }

        protected DBContextResult<object> InsertExpense(TRN_Expense p_TRN_Expense)
        {
            return ExecuteDALMethod<TRN_Expense, object>(db, (DataContext, P_TRN_Expense) =>
            {
                DataContext.TRN_Expense.Add(P_TRN_Expense);
                DataContext.SaveChanges();
                return null;
            }, p_TRN_Expense);
        }

        protected DBContextResult<object> InsertIncome(TRN_Income p_TRN_Income)
        {
            return ExecuteDALMethod<TRN_Income, object>(db, (DataContext, P_TRN_Income) =>
            {
                DataContext.TRN_Income.Add(P_TRN_Income);
                DataContext.SaveChanges();
                return null;
            }, p_TRN_Income);
        }

        protected DBContextResult<object> UpdateExpense(TRN_Expense p_TRN_Expense)
        {
            return ExecuteDALMethod<TRN_Expense, object>(db, (DataContext, P_TRN_Expense) =>
            {
                TRN_Expense Obj = DataContext.TRN_Expense.Find(P_TRN_Expense.ExpenseId);
                Obj.CategoryId = P_TRN_Expense.CategoryId;
                Obj.Amount = P_TRN_Expense.Amount;
                Obj.CreatedDate = P_TRN_Expense.CreatedDate;
                Obj.Note = P_TRN_Expense.Note;
                Obj.ModifiedDate = P_TRN_Expense.ModifiedDate;
                DataContext.SaveChanges();
                return null;
            }, p_TRN_Expense);
        }

        protected DBContextResult<object> UpdateIncome(TRN_Income p_TRN_Income)
        {
            return ExecuteDALMethod<TRN_Income, object>(db, (DataContext, P_TRN_Income) =>
            {
                TRN_Income Obj = DataContext.TRN_Income.Find(P_TRN_Income.IncomeId);
                Obj.CategoryId = P_TRN_Income.CategoryId;
                Obj.Amount = P_TRN_Income.Amount;
                Obj.CreatedDate = P_TRN_Income.CreatedDate;
                Obj.Note = P_TRN_Income.Note;
                Obj.ModifiedDate = P_TRN_Income.ModifiedDate;
                DataContext.SaveChanges();
                return null;
            }, p_TRN_Income);
        }

        protected DBContextResult<object> DeleteExpense(int p_ExpenseId)
        {
            return ExecuteDALMethod<int, object>(db, (DataContext, P_ExpenseId) =>
            {
                TRN_Expense Obj = DataContext.TRN_Expense.Find(P_ExpenseId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return null;
            }, p_ExpenseId);
        }

        protected DBContextResult<object> DeleteIncome(int p_IncomeId)
        {
            return ExecuteDALMethod<int, object>(db, (DataContext, P_IncomeId) =>
            {
                TRN_Income Obj = DataContext.TRN_Income.Find(P_IncomeId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, p_IncomeId);
        }

        protected DBContextResult<List<DEV_Category>> GetCategoryList(bool p_IsExpense)
        {
            return ExecuteDALMethod(db, (DataContext, P_IsExpense) =>
            {
                return P_IsExpense ? DataContext.DEV_Category.Where(x => x.TypeId == (int)Dev_Category.Expense).ToList() : DataContext.DEV_Category.Where(x => x.TypeId == (int)Dev_Category.Income).ToList();
            }, p_IsExpense);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~Abstract_Spending()
        {
            Dispose();
        }
    }
}
