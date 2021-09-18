using Logic_Layer.Bussiness;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthUserController: ControllerBase
    {
        private readonly IAuthUser authUserRepository;
        private readonly AppSettings appSetings;
        public IConfiguration Configuration { get; }
        public AuthUserController(IAuthUser authUserRepository, 
                                  IOptions<AppSettings> appSetings,
                                  IConfiguration configuration)
        {
            this.authUserRepository = authUserRepository;
            this.appSetings = appSetings.Value;
            this.Configuration = configuration;
        }

        [HttpGet("Get_All_Users")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await AuthUserService.Get_All_Users(this.authUserRepository);

            return Ok(result);
        }

        [HttpPost("Add_New_User")]
        [Authorize]
        public async Task<IActionResult> Add_New_User(CreateAuthUserDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AuthUserService.Insert_New_AuthUser(this.authUserRepository,
                                                                model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.User,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthUserDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                AuthUserService helper = new AuthUserService(this.appSetings);
                string encription = this.Configuration["AppSettings:Encryption"];

                LoggedUSerResponse objResponse = await helper.Validate_User(this.authUserRepository, encription, model);

                if (objResponse != null)
                {
                    return Ok(objResponse);
                }
                return BadRequest(string.Format("{0} {1}", ResponseStrings.Logged, ResponseStrings.Error));
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpGet("Get_User_By_Id/{id}")]
        [Authorize]
        public async Task<IActionResult> Get_Student_By_Id(int id)
        {
            var result = await AuthUserService.Get_User_By_Id(this.authUserRepository, id);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }

            return Ok(string.Format("{0} {1} {2}",
                      ResponseStrings.User, ResponseStrings.Not, ResponseStrings.Exist));
        }

        [HttpPut("Update_AuthUser/{id}")]
        [Authorize]
        public async Task<IActionResult> Update_AuthUser(LoginAuthUserDTO model, int id)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AuthUserService.Update_AuthUser(this.authUserRepository,
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

        [HttpDelete("Delete_User/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete_User(int id)
        {

            if (await AuthUserService.Delete_User(this.authUserRepository, id))
            {
                return Ok(string.Format("{0} {1} {2}",
                          ResponseStrings.User,
                          ResponseStrings.Deleted,
                          ResponseStrings.Success));
            }

            return BadRequest(string.Format("{0} {1} {2}",
                              ResponseStrings.User,
                              ResponseStrings.Not,
                              ResponseStrings.Exist));
        }
    }
}
