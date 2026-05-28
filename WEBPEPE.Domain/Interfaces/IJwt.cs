using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;

namespace WEBPEPE.Domain.Interfaces
{
    public interface IJwt
    {
        /// <summary>
        /// Para generar un token de acceso a recursos
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        string GenerateToken(User user, IList<string> roles);


    }
}
