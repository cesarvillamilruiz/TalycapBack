using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Logic_Layer.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly DataContext context;

        public AuthorRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Author> Get_Author_With_Books(int id)
        {
            Author authorWithBooks = await  this.context.Author
                .Where(a => a.Id == id)
                .Include(b => b.Books)
                .FirstOrDefaultAsync();

            return authorWithBooks;
        }
    }
}
