using System;
using BCrypt.Net;


namespace Valkimia.ABMClientes.Helpers
{
    public class Helper
    {
        public string HashPassword(string password)
        {
            // Generar un salt aleatorio
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hashear el password con el salt generado
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verificar si el password coincide con el hash almacenado
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            return passwordMatches;
        }
    }
}
