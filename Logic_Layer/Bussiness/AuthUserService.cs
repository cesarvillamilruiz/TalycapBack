using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class AuthUserService
    {
        private readonly AppSettings appSetings;
        public AuthUserService(AppSettings appSetings)
        {
            this.appSetings = appSetings;
        }

        public static async Task<List<UserDTO>> Get_All_Users(IAuthUser AuthUserRepository)
        {
            List<UserDTO> UserDTO = new List<UserDTO>();
            try
            {
                List<User> users = await AuthUserRepository.GetAll().ToListAsync();
                users.ForEach(x => {
                    UserDTO user = new UserDTO
                    {
                        Id = x.Id,
                        User = x.userName
                    };
                    UserDTO.Add(user);
                });
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Get_All_Users {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }

            return UserDTO;
        }

        public static async Task<bool> Insert_New_AuthUser(IAuthUser AuthUserRepository, CreateAuthUserDTO model)
        {
            try
            {
                await AuthUserRepository.CreateAsync(new User
                {
                    userName = model.User,
                    password = model.Password
                });
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AuthUserExecution.Insert_New_AuthUser {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return false;
        }

        public  async Task<LoggedUSerResponse> Validate_User(IAuthUser AuthUserRepository,
                                         string encription,
                                         LoginAuthUserDTO model)
        {
            try
            {
                var authUser = await AuthUserRepository.Validate_Login(model);

                if (authUser != null)
                {
                    string token = Get_Token(encription, authUser);
                    LoggedUSerResponse helper = new LoggedUSerResponse
                    {
                        User = authUser.userName,
                        Token = token
                    };
                    return helper;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AuthUserExecution.Validate_User {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return null;
        }

        public string Get_Token(string encription, User model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(encription);

            var tkenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                        {
                            new Claim (ClaimTypes.NameIdentifier, model.userName.ToString())
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenHandler.CreateToken(tkenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static async Task<(bool, User)> Get_User_By_Id(IAuthUser AuthUserRepository, int id)
        {
            try
            {
                User student = await AuthUserRepository.GetByIdAsync(id);
                if (student == null)
                {
                    return (false, null);
                }
                return (true, student);
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Get_User_By_Id {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return (false, null);
            }
        }

        public static async Task<bool> Update_AuthUser(IAuthUser AuthUserRepository, LoginAuthUserDTO model, int id)
        {
            try
            {
                var user = await AuthUserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                user.userName = model.User;
                user.password = model.Password;

                await AuthUserRepository.UpdateAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> Delete_User(IAuthUser AuthUserRepository, int id)
        {
            try
            {
                var user = await AuthUserRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return false;
                }
                await AuthUserRepository.DeleteAsync(user);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
