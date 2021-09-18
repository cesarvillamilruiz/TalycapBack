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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet("Get_All_Authors")]
        
        public IActionResult Index()
        {
            var result = AuthorService.Get_All_Authors(this.authorRepository);

            return Ok(result);
        }

        [HttpPost("Add_New_Author")]
        public async Task<IActionResult> Add_New_Author(AuthorDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AuthorService.Insert_New_Author(this.authorRepository,
                                                                model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Author,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpGet("Get_Author_By_Id/{id}")]
        public async Task<IActionResult> Get_Author_By_Id(int id)
        {
            var result = await AuthorService.Get_Author_By_Id(this.authorRepository, id);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }

            return Ok(string.Format("{0} {1} {2}",
                      ResponseStrings.Author, ResponseStrings.Not, ResponseStrings.Exist));
        }

        [HttpGet("Get_Author_With_Books/{id}")]
        public async Task<IActionResult> Get_Author_With_Books(int id)
        {
            var result = await AuthorService.Get_Author_With_Books(this.authorRepository, id);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }

            return Ok(string.Format("{0} {1} {2}",
                      ResponseStrings.Author, ResponseStrings.Not, ResponseStrings.Exist));
        }

        [HttpPut("Update_Author/{id}")]
        public async Task<IActionResult> Update_Author(AuthorDTO model, int id)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AuthorService.Update_Author(this.authorRepository,
                                                                model,
                                                                id))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.User,
                            ResponseStrings.Updated,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpDelete("Delete_Author/{id}")]
        public async Task<IActionResult> Delete_Author(int id)
        {

            if (await AuthorService.Delete_Author(this.authorRepository, id))
            {
                return Ok(string.Format("{0} {1} {2}",
                          ResponseStrings.Author,
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
