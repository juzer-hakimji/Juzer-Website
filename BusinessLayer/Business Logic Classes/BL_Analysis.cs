﻿using BusinessEntities.Entities.Entity_Model;
using BusinessLayer.TransactionResultModel;
using DataAccessLayer.Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Business_Logic_Classes
{
    public class BL_Analysis
    {
        private DAL_Analysis DALObj { get; set; }

        public BL_Analysis()
        {
            DALObj = new DAL_Analysis();
        }

        public void BL_GetUserData()
        {

        }

        public TransactionResult<object> BL_AddOrRemoveAdmin(string p_UserIds, bool p_IsAdmin)
        {
            if (DALObj.AddOrRemoveAdmin(p_UserIds.Split(',').ToList(), p_IsAdmin))
            {
                if (p_IsAdmin)
                {
                    return new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = "/Analysis/Index",
                        Message = "Admin Addition successful"
                    };
                }
                else
                {
                    return new TransactionResult<object>
                    {
                        Success = true,
                        RedirectURL = "/Analysis/Index",
                        Message = "Admin Removal successful"
                    };
                }
            }
            else
            {
                if (p_IsAdmin)
                {
                    return new TransactionResult<object>
                    {
                        Success = false,
                        Message = "Admin Addition unsuccessful"
                    };
                }
                else
                {
                    return new TransactionResult<object>
                    {
                        Success = false,
                        Message = "Admin Removal unsuccessful"
                    };
                }
            }
        }

        public List<UserDetailsVM> BL_GetAddOrRemoveAdminList(bool IsAddAdminList)
        {
            List<UserDetailsVM> UserList = new List<UserDetailsVM>();
            foreach (var item in DALObj.DAL_GetAddOrRemoveAdminList(IsAddAdminList).Data)
            {
                UserList.Add(new UserDetailsVM { UserId = item.UserId, SignUpEmail = item.Email });
            }
            return UserList;
        }
    }
}
