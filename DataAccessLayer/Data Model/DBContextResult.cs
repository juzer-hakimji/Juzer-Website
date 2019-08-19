using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data_Model
{
    public class DBContextResult<U>
    {
        public U Data { get; set; }
        public bool TransactionResult { get; set; }
    }
}
