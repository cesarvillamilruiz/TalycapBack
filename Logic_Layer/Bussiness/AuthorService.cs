using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class AuthorService
    {
        public static async Task<bool> Insert_New_Author(IAuthorRepository authorRepository, AuthorDTO model)
        {
            try
            {
                await authorRepository.CreateAsync(new Author
                {
                    firstName = model.firstName,
                    lastName = model.lastName
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Author> Get_All_Authors(IAuthorRepository authorRepository)
        {

            List<Author> authors = new List<Author>();
            try
            {
                authors = authorRepository.GetAll().ToList();                
            }
            catch (Exception ex)
            {
            }

            return authors;
        }

        public static async Task<(bool, AuthorDTO)> Get_Author_By_Id(IAuthorRepository authorRepository, int id)
        {
            try
            {
                Author author = await authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return (false, null);
                }
                return (true, new AuthorDTO { firstName = author .firstName, lastName = author.lastName});
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public static async Task<(bool, Author)> Get_Author_With_Books(IAuthorRepository authorRepository, int id)
        {
            try
            {
                Author author = await authorRepository.Get_Author_With_Books(id);
                if (author == null)
                {
                    return (false, null);
                }
                return (true, author);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public static async Task<bool> Update_Author(IAuthorRepository authorRepository, AuthorDTO model, int id)
        {
            try
            {
                var user = await authorRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                user.firstName = model.firstName;
                user.lastName = model.lastName;

                await authorRepository.UpdateAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> Delete_Author(IAuthorRepository authorRepository, int id)
        {
            try
            {
                var user = await authorRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return false;
                }
                await authorRepository.DeleteAsync(user);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
