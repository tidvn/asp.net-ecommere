using TDStore.Areas.Store.Models;
using TDStore.Models;
namespace TDStore.Models.Repository
{
    public interface IRepositoryProduct: IRepositoryGeneric<Product, string>
    {
        List<Product> Paging(int page, int pageSize, out long totalrows);
        List<Product> SearchPaging(int page, int pageSize, out long totalrows);
        
    }
}
