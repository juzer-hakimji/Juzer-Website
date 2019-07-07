using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Spending : Abstract_Spending
    {
        public List<usp_GetExpenseList_Result> DAL_GetExpenseList(int p_UserId)
        {
            return this.SelectExpense(p_UserId);
        }

        public List<usp_GetIncomesList_Result> DAL_GetIncomesList(int p_UserId)
        {
            return this.SelectIncome(p_UserId);
        }

        public bool DAL_SaveExpense(TRN_Expense p_TRN_Expense)
        {
            return this.InsertExpense(p_TRN_Expense);
        }

        public bool DAL_SaveIncome(TRN_Income p_TRN_Income)
        {
            return this.InsertIncome(p_TRN_Income);
        }

        public bool DAL_UpdateExpense(TRN_Expense p_TRN_Expense)
        {
            return this.UpdateExpense(p_TRN_Expense);
        }

        public bool DAL_UpdateIncome(TRN_Income p_TRN_Income)
        {
            return this.UpdateIncome(p_TRN_Income);
        }

        public bool DAL_DeleteExpense(int p_ExpenseId)
        {
            return this.DeleteExpense(p_ExpenseId);
        }

        public bool DAL_DeleteIncome(int p_IncomeId)
        {
            return this.DeleteIncome(p_IncomeId);
        }
    }
}
