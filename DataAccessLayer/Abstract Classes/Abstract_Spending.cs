using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Base_Classes;
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

        protected List<usp_GetExpenseList_Result> SelectExpense(int p_UserId)
        {
            return db.usp_GetExpenseList(p_UserId).ToList();
        }

        protected List<usp_GetIncomesList_Result> SelectIncome(int p_UserId)
        {
            return db.usp_GetIncomesList(p_UserId).ToList();
        }

        protected bool InsertExpense(TRN_Expense p_TRN_Expense)
        {
            return this.ExecuteDALMethod<TRN_Expense, bool>(db, (DataContext, P_TRN_Expense) =>
            {
                DataContext.TRN_Expense.Add(P_TRN_Expense);
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Expense);
        }

        protected bool InsertIncome(TRN_Income p_TRN_Income)
        {
            return this.ExecuteDALMethod<TRN_Income, bool>(db, (DataContext, P_TRN_Income) =>
            {
                DataContext.TRN_Income.Add(P_TRN_Income);
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Income);
        }

        protected bool UpdateExpense(TRN_Expense p_TRN_Expense)
        {
            return this.ExecuteDALMethod<TRN_Expense, bool>(db, (DataContext, P_TRN_Expense) =>
            {
                TRN_Expense Obj = DataContext.TRN_Expense.Find(P_TRN_Expense.ExpenseId);
                Obj.CategoryId = P_TRN_Expense.CategoryId;
                Obj.Amount = P_TRN_Expense.Amount;
                Obj.CreatedDate = P_TRN_Expense.CreatedDate;
                Obj.Note = P_TRN_Expense.Note;
                Obj.ModifiedDate = P_TRN_Expense.ModifiedDate;
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Expense);
        }

        protected bool UpdateIncome(TRN_Income p_TRN_Income)
        {
            return this.ExecuteDALMethod<TRN_Income, bool>(db, (DataContext, P_TRN_Income) =>
            {
                TRN_Income Obj = DataContext.TRN_Income.Find(P_TRN_Income.IncomeId);
                Obj.CategoryId = P_TRN_Income.CategoryId;
                Obj.Amount = P_TRN_Income.Amount;
                Obj.CreatedDate = P_TRN_Income.CreatedDate;
                Obj.Note = P_TRN_Income.Note;
                Obj.ModifiedDate = P_TRN_Income.ModifiedDate;
                DataContext.SaveChanges();
                return true;
            }, p_TRN_Income);
        }

        protected bool DeleteExpense(int p_ExpenseId)
        {
            TRN_Expense TRN_Expense = new TRN_Expense
            {
                ExpenseId = p_ExpenseId
            };
            return this.ExecuteDALMethod<TRN_Expense, bool>(db, (DataContext, P_TRN_Expense) =>
            {
                TRN_Expense Obj = DataContext.TRN_Expense.Find(P_TRN_Expense.ExpenseId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, TRN_Expense);
        }

        protected bool DeleteIncome(int p_IncomeId)
        {
            TRN_Income TRN_Income = new TRN_Income
            {
                IncomeId = p_IncomeId
            };
            return this.ExecuteDALMethod<TRN_Income, bool>(db, (DataContext, P_TRN_Income) =>
            {
                TRN_Income Obj = DataContext.TRN_Income.Find(P_TRN_Income.IncomeId);
                Obj.IsActive = false;
                DataContext.SaveChanges();
                return true;
            }, TRN_Income);
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
