using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Interface
{
    public interface IRepository<T> where T: class
    {
        Task<ObservableCollection<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        T FindByIdAsync(int id);
    }
}
