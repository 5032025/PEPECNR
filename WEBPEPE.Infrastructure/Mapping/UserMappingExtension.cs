using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Infrastructure.Identity;

namespace WEBPEPE.Infrastructure.Mapping
{
    public static class UserMappingExtension
    { /// <summary>
      /// Pasa un usuario del dominio a la base
      /// </summary>
      /// <param name="usuario"></param>
      /// <returns></returns>

        public static AppIdentityUser ToIdentityUser(this User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "El objeto usuario llegó nulo al mapeador.");

            return new AppIdentityUser
            {


                UserName = user.Email,
                Email = user.Email,
                PhoneNumber = user.Tel


            };

        }

        /// <summary>
        /// Pasa un usuario de la base a dominio
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public static User ToDomainUser(this AppIdentityUser identityUser)
        {
            return new User
            {
                Id = Guid.Parse(identityUser.Id),
                Email = identityUser.Email,
                Tel = identityUser.PhoneNumber,
                FirstName = identityUser.UserName,
                LastName = ""




            };

        }
    }
}
