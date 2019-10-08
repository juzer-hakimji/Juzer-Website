using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SpendingVM : BaseViewModel
    {
        public SpendingVM()
        {
            CategoryList = new List<CategoryVM>();
        }

        public int? Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Amount { get; set; }
        public string CreatedDate { get; set; }
        public string Note { get; set; }
        public string IsExpense { get; set; }
        public List<CategoryVM> CategoryList { get; set; }
    }

    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
