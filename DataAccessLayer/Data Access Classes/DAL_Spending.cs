using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Abstract_Classes;
using DataAccessLayer.Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Access_Classes
{
    public class DAL_Spending : Abstract_Spending
    {
        public DBContextResult<List<usp_GetExpenseList_Result>> DAL_GetExpenseList(int p_UserId)
        {
            return SelectExpense(p_UserId);
        }

        public DBContextResult<List<usp_GetIncomesList_Result>> DAL_GetIncomesList(int p_UserId)
        {
            return SelectIncome(p_UserId);
        }

        public DBContextResult<object> DAL_SaveExpense(TRN_Expense p_TRN_Expense)
        {
            return this.InsertExpense(p_TRN_Expense);
        }

        public DBContextResult<object> DAL_SaveIncome(TRN_Income p_TRN_Income)
        {
            return this.InsertIncome(p_TRN_Income);
        }

        public DBContextResult<object> DAL_UpdateExpense(TRN_Expense p_TRN_Expense)
        {
            return this.UpdateExpense(p_TRN_Expense);
        }

        public DBContextResult<object> DAL_UpdateIncome(TRN_Income p_TRN_Income)
        {
            return this.UpdateIncome(p_TRN_Income);
        }

        public DBContextResult<object> DAL_DeleteExpense(int p_ExpenseId)
        {
            return this.DeleteExpense(p_ExpenseId);
        }

        public DBContextResult<object> DAL_DeleteIncome(int p_IncomeId)
        {
            return this.DeleteIncome(p_IncomeId);
        }
    }
}
