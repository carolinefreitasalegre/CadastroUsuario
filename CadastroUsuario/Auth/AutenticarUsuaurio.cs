using CadastroUsuario.Models.UserModel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CadastroUsuario.Auth
{
    public static class AutenticarUsuaurio 
    {
       
        public static string GerarToken(Usuario usuario, string strinkKey)
        {

            if (usuario == null)
                throw new Exception("Usuário nulo/inválido!");


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(strinkKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                //new Claim("TipoUsuario", usuario.TipoUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())

            };

            var byteKey = Encoding.ASCII.GetBytes(strinkKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var infoToken = tokenHandler.WriteToken(token);
            return infoToken;
        }
    }
}
