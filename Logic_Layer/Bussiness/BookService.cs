using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class BookService
    {
        public static List<Book> Get_All_Books(IBookRepository bookRepository)
        {
            List<Book> books = bookRepository.GetAll().ToList();
            try
            {
                books = bookRepository.GetAll().ToList();                
            }
            catch (Exception ex)
            {
            }

            return books;
        }

        public static async Task<bool> Insert_New_Book(IBookRepository bookRepository, BookDTO model)
        {
            try
            {
                await bookRepository.CreateAsync(new Book
                {
                    title = model.title,
                    description = model.description,
                    pageCount = model.pageCount,
                    excerpt = model.excerpt,
                    publishDate = model.publishDate,
                    AuthorId = model.AuthorId
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<(bool, BookDTO)> Get_Book_By_Id(IBookRepository bookRepository, int id)
        {
            try
            {
                Book book = await bookRepository.GetByIdAsync(id);
                if (book == null)
                {
                    return (false, null);
                }
                return (true, new BookDTO { 
                    title = book.title,
                    description = book.description,
                    pageCount = book.pageCount,
                    excerpt = book.excerpt,
                    publishDate = book.publishDate,
                    AuthorId = book.AuthorId
                });
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public static async Task<bool> Update_Book(IBookRepository bookRepository, BookDTO model, int id)
        {
            try
            {
                var book = await bookRepository.GetByIdAsync(id);
                if (book == null)
                {
                    return false;
                }

                book.title = model.title;
                book.description = model.description;
                book.pageCount = model.pageCount;
                book.excerpt = model.excerpt;
                book.publishDate = model.publishDate;
                book.AuthorId = model.AuthorId;

                await bookRepository.UpdateAsync(book);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> Delete_Book(IBookRepository bookRepository, int id)
        {
            try
            {
                var book = await bookRepository.GetByIdAsync(id);

                if (book == null)
                {
                    return false;
                }
                await bookRepository.DeleteAsync(book);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
