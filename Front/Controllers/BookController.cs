using Logic_Layer.Bussiness;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController: ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet("Get_All_Books")]
        public IActionResult Index()
        {
            var result = BookService.Get_All_Books(this.bookRepository);

            return Ok(result);
        }

        [HttpPost("Add_New_book")]
        public async Task<IActionResult> Add_New_book(BookDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await BookService.Insert_New_Book(this.bookRepository,
                                                                model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Book,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpGet("Get_Book_By_Id/{id}")]
        public async Task<IActionResult> Get_Book_By_Id(int id)
        {
            var result = await BookService.Get_Book_By_Id(this.bookRepository, id);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }

            return Ok(string.Format("{0} {1} {2}",
                      ResponseStrings.Book, ResponseStrings.Not, ResponseStrings.Exist));
        }

        [HttpPut("Update_Book/{id}")]
        public async Task<IActionResult> Update_Book(BookDTO model, int id)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await BookService.Update_Book(this.bookRepository,
                                                                model,
                                                                id))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Book,
                            ResponseStrings.Updated,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpDelete("Delete_Book/{id}")]
        public async Task<IActionResult> Delete_Book(int id)
        {

            if (await BookService.Delete_Book(this.bookRepository, id))
            {
                return Ok(string.Format("{0} {1} {2}",
                          ResponseStrings.Book,
                          ResponseStrings.Deleted,
                          ResponseStrings.Success));
            }

            return BadRequest(string.Format("{0} {1} {2}",
                              ResponseStrings.Author,
                              ResponseStrings.Not,
                              ResponseStrings.Exist));
        }
    }
}
