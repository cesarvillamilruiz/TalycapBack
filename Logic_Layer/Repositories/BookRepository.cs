using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.DTO;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DataContext context;

        public BookRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
