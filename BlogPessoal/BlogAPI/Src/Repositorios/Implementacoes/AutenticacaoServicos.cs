﻿using BlogAPI.Src.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios.Implementacoes
{
    public class AutenticacaoServicos : IAutenticacao
    {
        #region Atributos
        private IUsuarios _repositorio;
        public IConfiguration Configuracao { get; }
        #endregion

        #region construtores
        public AutenticacaoServicos(IUsuarios repositorio, IConfiguration configuration)
        {
            _repositorio = repositorio;
            Configuracao = configuration;
        }
        #endregion
        public string codificarSenha(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
                return Convert.ToBase64String(bytes);
        }

        public async Task CriarUsuarioSemDuplicarAsync(Usuario usuario)
        {
          var auxiliar  = await _repositorio.PegarUsuarioPeloEmailAsync(usuario.Email);

            if (auxiliar != null) throw new Exception("Esse email já está sendo utilizado");

            usuario.Senha = codificarSenha(usuario.Senha);

            await _repositorio.NovoUsuarioAsync(usuario);
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                        new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }
    }
}
