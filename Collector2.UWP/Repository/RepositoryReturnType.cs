using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Repository
{
    public class RepositoryReturnType<T>
    {
        private List<T> _returnList;
        public List<T> ReturnList
        {
            get
            {
                return _returnList;
            }
        }

        public void WriteReturnList(List<T> t)
        {
            _returnList = t;
        }

        public T ReturnData { get; set; }
        public string Message { get; set; }
        public int RecordsAffected { get; set; }
        public bool IsTransactionSuccessfull { get; set; }

    }
}
