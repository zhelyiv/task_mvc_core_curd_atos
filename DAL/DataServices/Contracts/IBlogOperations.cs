using System.Collections.Generic; 
using System.Threading.Tasks;
using ViewModels;

namespace DAL.DataServices.Contracts
{
    public interface IBlogOperations
    {
        IEnumerable<BlogViewModel> GetByUserId(int? userId);
    }
}
