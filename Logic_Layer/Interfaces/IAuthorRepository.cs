using Data_Access.Data.Entities;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> Get_Author_With_Books(int id);
    }
}
