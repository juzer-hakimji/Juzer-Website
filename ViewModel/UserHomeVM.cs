using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserHomeVM : BaseViewModel
    {
        public MonthVM MonthVM { get; set; }
        public YearVM YearVM { get; set; }
    }

    public class MonthVM 
    {
        public int? MonthIncome { get; set; }
        public int? MonthExpense { get; set; }
    }

    public class YearVM
    {
        public int? YearIncome { get; set; }
        public int? YearExpense { get; set; }
    }

}
