using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SpendingVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Amount { get; set; }
        public string CreatedDate { get; set; }
        public string Note { get; set; }
    }
}
