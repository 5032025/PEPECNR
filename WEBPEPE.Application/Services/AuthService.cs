using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Application.Services
{
    public class AuthService
    {

        private readonly IUser _userRepository;
        private readonly IJwt _jwtService;

        public AuthService(IUser userRepository, IJwt jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }




        public async Task<User> RegisterUser(User user)
        {

            var createdUser = await _userRepository.CreateUser(user);

            if (createdUser != null)
            {

                await _userRepository.AddToRolesAsync(createdUser, "User");

                return createdUser;
            }

            return null;
        }



        public async Task<string> Login(string email, string password, bool rememberme)
        {

            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return "Credenciales invalidas";
            }


            var credencialesValidas = await _userRepository.CheckPasswordAsync(user.Id.ToString(), password);

            if (!credencialesValidas)
            {
                return "Credenciales invalidas";
            }

            // todo -> JWT



            var roles = await _userRepository.GetUserRoles(email);



            return _jwtService.GenerateToken(user, roles);
        }





    }
}
