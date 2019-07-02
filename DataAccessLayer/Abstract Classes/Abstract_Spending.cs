using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract_Classes
{
    public abstract class Abstract_Spending : IDisposable
    { 

        JuzerWebsiteEntities db;

        public Abstract_Spending()
        {
            db = new JuzerWebsiteEntities();
        }

        public List<usp_GetExpenseList_Result> SelectExpense(int p_UserId)
        {
            return db.usp_GetExpenseList(p_UserId).ToList();
        }

        public List<usp_GetIncomesList_Result> SelectIncome(int p_UserId)
        {
            return db.usp_GetIncomesList(p_UserId).ToList();
        }

        public bool InsertExpense(TRN_Expense p_TRN_Expense)
        {
            db.TRN_Expense.Add(p_TRN_Expense);
            db.SaveChanges();
            return true;
        }

        public bool InsertIncome(TRN_Income p_TRN_Income)
        {
            db.TRN_Income.Add(p_TRN_Income);
            db.SaveChanges();
            return true;
        }

        public bool UpdateExpense(TRN_Expense p_TRN_Expense)
        {
            TRN_Expense Obj = db.TRN_Expense.Find(p_TRN_Expense.ExpenseId);
            Obj.CategoryId = p_TRN_Expense.CategoryId;
            Obj.Amount = p_TRN_Expense.Amount;
            Obj.CreatedDate = p_TRN_Expense.CreatedDate;
            Obj.Note = p_TRN_Expense.Note;
            Obj.ModifiedDate = p_TRN_Expense.ModifiedDate;
            db.SaveChanges();    
            return true;
        }

        public bool UpdateIncome(TRN_Income p_TRN_Income)
        {
            TRN_Income Obj = db.TRN_Income.Find(p_TRN_Income.IncomeId);
            Obj.CategoryId = p_TRN_Income.CategoryId;
            Obj.Amount = p_TRN_Income.Amount;
            Obj.CreatedDate = p_TRN_Income.CreatedDate;
            Obj.Note = p_TRN_Income.Note;
            Obj.ModifiedDate = p_TRN_Income.ModifiedDate;
            db.SaveChanges();
            return true;
        }

        public bool DeleteExpense(int p_ExpenseId)
        {
            TRN_Expense Obj = db.TRN_Expense.Find(p_ExpenseId);
            Obj.IsActive = false;
            db.SaveChanges();
            return true;
        }

        public bool DeleteIncome(int p_IncomeId)
        {
            TRN_Income Obj = db.TRN_Income.Find(p_IncomeId);
            Obj.IsActive = false;
            db.SaveChanges();
            return true;
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
