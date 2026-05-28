using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;

namespace WEBPEPE.Domain.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Esto me permite buscar usuarios por email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// Para crear un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="usuario">Informacion del usuario</param>
        /// <returns>Devuelve un usuario creado en la base y con un Id</returns>

        Task<User> CreateUser(User user);



        /// <summary>
        /// Para agregar un usuario a un rol específico, lo que es útil para gestionar permisos y accesos dentro de la aplicación. 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>

        Task<User> AddToRolesAsync(User user, string roleName);

        /// <summary>
        /// Para validar que la contraseña pertenece al usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(string userId, string password);


        Task<bool> UserExists(string email);

        Task<List<string>> GetUserRoles(string email);
    }
}
