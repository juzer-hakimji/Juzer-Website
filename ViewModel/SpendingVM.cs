using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Please enter Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter Amount")]
        public int? Amount { get; set; }
        [Required(ErrorMessage = "Please enter Date")]
        public string CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter Note")]
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
