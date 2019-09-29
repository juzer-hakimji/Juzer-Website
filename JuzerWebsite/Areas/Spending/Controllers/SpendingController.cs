using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.Business_Logic_Classes;
using BusinessLayer.TransactionResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace JuzerWebsite.Areas.Spending.Controllers
{
    public class SpendingController : Controller
    {
        private BL_Spending BLObj { get; set; }

        public SpendingController()
        {
            BLObj = new BL_Spending();
        }

        public ActionResult List()
        {
            return View("Spending", new SpendingVM
            {
                Title = "Notes",
                FirstName = (Session["MST_UserInfo"] as MST_UserInfo).FirstName,
                DeveloperName = "Juzer Hakimji",
                Year = DateTime.Now.Year.ToString()
            });
        }

        public JsonResult GetListData()
        {
            List<SpendingVM> ExpenseList = BLObj.BL_GetExpenseList((Session["MST_UserInfo"] as MST_UserInfo).UserId);
            List<SpendingVM> IncomeList = BLObj.BL_GetIncomesList((Session["MST_UserInfo"] as MST_UserInfo).UserId);
            return Json(new { ExpenseList, IncomeList }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult SaveExpenseOrIncome(SpendingVM P_SpendingVM, bool IsExpense)
        {
            TransactionResult<object> result;
            if (P_SpendingVM.Id != null)
            {
                if (IsExpense)
                {
                    result = BLObj.BL_UpdateExpense(P_SpendingVM);
                }
                else
                {
                    result = BLObj.BL_UpdateIncome(P_SpendingVM);
                }
            }
            else
            {
                if (IsExpense)
                {
                    result = BLObj.BL_SaveExpense(P_SpendingVM, (Session["MST_UserInfo"] as MST_UserInfo).UserId);
                }
                else
                {
                    result = BLObj.BL_SaveIncome(P_SpendingVM, (Session["MST_UserInfo"] as MST_UserInfo).UserId);
                }
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteExpenseOrIncome(int Id,bool IsExpense)
        {
            TransactionResult<object> result;
            if (IsExpense)
            {
                result = BLObj.BL_DeleteExpense(Id);
            }
            else
            {
                result = BLObj.BL_DeleteIncome(Id);
            }
            return Json(result);
        }
    }
}
