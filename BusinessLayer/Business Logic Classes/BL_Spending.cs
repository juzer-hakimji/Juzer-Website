using BusinessEntities.Entities.Entity_Model;
using DataAccessLayer.Data_Access_Classes;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Spending
    {
        private DAL_Spending DALObj { get; set; }
        private List<SpendingVM> SpendingVMList { get; set; }
        private TRN_Expense ExpenseObj { get; set; }
        private TRN_Income IncomeObj { get; set; }

        BL_Spending()
        {
            DALObj = new DAL_Spending();
        }

        public List<SpendingVM> BL_GetExpenseList(int p_UserId)
        {
            SpendingVMList = new List<SpendingVM>();
            List<usp_GetExpenseList_Result> ExpenseListObj = DALObj.DAL_GetExpenseList(p_UserId).Data;
            foreach (usp_GetExpenseList_Result Expense in ExpenseListObj)
            {
                SpendingVM SpendingVMObj = new SpendingVM
                {
                    Id = Expense.ExpenseId,
                    CategoryId = Expense.CategoryId,
                    Amount = Expense.Amount,
                    CreatedDate = Expense.CreatedDate.ToString(),
                    Note = Expense.Note
                };
                SpendingVMList.Add(SpendingVMObj);
            }
            return SpendingVMList;
        }

        public List<SpendingVM> BL_GetIncomesList(int p_UserId)
        {
            SpendingVMList = new List<SpendingVM>();
            List<usp_GetIncomesList_Result> IncomesListObj = DALObj.DAL_GetIncomesList(p_UserId).Data;
            foreach (usp_GetIncomesList_Result Income in IncomesListObj)
            {
                SpendingVM SpendingVMObj = new SpendingVM
                {
                    Id = Income.IncomeId,
                    CategoryId = Income.CategoryId,
                    Amount = Income.Amount,
                    CreatedDate = Income.CreatedDate.ToString(),
                    Note = Income.Note
                };
                SpendingVMList.Add(SpendingVMObj);
            }
            return SpendingVMList;
        }

        public bool BL_SaveExpense(SpendingVM p_SpendingVMObj)
        {
            ExpenseObj = new TRN_Expense
            {
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note,
                IsActive = true
            };
            return DALObj.DAL_SaveExpense(ExpenseObj).TransactionResult;
        }

        public bool BL_SaveIncome(SpendingVM p_SpendingVMObj)
        {
            IncomeObj = new TRN_Income
            {
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note,
                IsActive = true
            };
            return DALObj.DAL_SaveIncome(IncomeObj).TransactionResult;
        }

        public bool BL_UpdateExpense(SpendingVM p_SpendingVMObj)
        {
            ExpenseObj = new TRN_Expense
            {
                ExpenseId = p_SpendingVMObj.Id,
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note
            };
            return DALObj.DAL_UpdateExpense(ExpenseObj).TransactionResult;
        }

        public bool BL_UpdateIncome(SpendingVM p_SpendingVMObj)
        {
            IncomeObj = new TRN_Income
            {
                IncomeId = p_SpendingVMObj.Id,
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note
            };
            return DALObj.DAL_UpdateIncome(IncomeObj).TransactionResult;
        }

        public bool BL_DeleteExpense(int p_ExpenseId)
        {
            return DALObj.DAL_DeleteExpense(p_ExpenseId).TransactionResult;
        }

        public bool BL_DeleteIncome(int p_IncomeId)
        {
            return DALObj.DAL_DeleteIncome(p_IncomeId).TransactionResult;
        }
    }
}
