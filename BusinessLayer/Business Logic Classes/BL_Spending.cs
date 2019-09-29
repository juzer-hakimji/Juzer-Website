using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.TransactionResultModel;
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

        public BL_Spending()
        {
            DALObj = new DAL_Spending();
        }

        public List<SpendingVM> BL_GetExpenseList(int p_UserId)
        {
            SpendingVMList = new List<SpendingVM>();
            //List<usp_GetExpenseList_Result> ExpenseListObj = DALObj.DAL_GetExpenseList(p_UserId).Data;
            foreach (usp_GetExpenseList_Result Expense in DALObj.DAL_GetExpenseList(p_UserId).Data)
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
            //List<usp_GetIncomesList_Result> IncomesListObj = DALObj.DAL_GetIncomesList(p_UserId).Data;
            foreach (usp_GetIncomesList_Result Income in DALObj.DAL_GetIncomesList(p_UserId).Data)
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

        public TransactionResult<object> BL_SaveExpense(SpendingVM p_SpendingVMObj, int p_UserId)
        {
            if (DALObj.DAL_SaveExpense(new TRN_Expense
            {
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note,
                UserId = p_UserId,
                IsActive = true
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Expense Saved Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_SaveIncome(SpendingVM p_SpendingVMObj,int p_UserId)
        {
            if (DALObj.DAL_SaveIncome(new TRN_Income
            {
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note,
                UserId = p_UserId,
                IsActive = true
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Income Saved Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_UpdateExpense(SpendingVM p_SpendingVMObj)
        {
            if (DALObj.DAL_UpdateExpense(new TRN_Expense
            {
                ExpenseId = p_SpendingVMObj.Id ?? 0,
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Expense Updated Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_UpdateIncome(SpendingVM p_SpendingVMObj)
        {
            if (DALObj.DAL_UpdateIncome(new TRN_Income
            {
                IncomeId = p_SpendingVMObj.Id ?? 0,
                CategoryId = p_SpendingVMObj.CategoryId,
                Amount = p_SpendingVMObj.Amount,
                CreatedDate = Convert.ToDateTime(p_SpendingVMObj.CreatedDate, CultureInfo.InvariantCulture),
                Note = p_SpendingVMObj.Note
            }).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Income Updated Successfully"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_DeleteExpense(int p_ExpenseId)
        {
            if (DALObj.DAL_DeleteExpense(p_ExpenseId).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Expense Deletion Successful"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }

        public TransactionResult<object> BL_DeleteIncome(int p_IncomeId)
        {
            if (DALObj.DAL_DeleteIncome(p_IncomeId).TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "Income Deletion Successful"
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something Went Wrong,Please try again"
                };
            }
        }
    }
}
