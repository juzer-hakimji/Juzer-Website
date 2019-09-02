using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.TransactionResultModel
{
    public class TransactionResult<T>
    {
        public bool Success { get; set; }
        public string RedirectURL { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
