﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Src.Repositorios.Implentacoes
{
    public class PostagemRepositorio : IPostagem
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion

        #region Construtor

        public PostagemRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Métodos

        public async Task<List<Postagem>> PegarTodosPostagensAsync()
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        public async Task<Postagem> PegarPostagemPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("id do Tema não encontrado");

            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);

            //Função Auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Postagens.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        public async Task NovoPostagemAsync(Postagem postagem)
        {
            if (!ExisteIdCriador(postagem.Criador.Id)) throw new Exception("id do Usuario não encontrado");

            if (!ExisteIdTema(postagem.Tema.Id)) throw new Exception("id do Tema não encontrado");

            await _contexto.Postagens.AddAsync(new Postagem
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                URL_Foto = postagem.URL_Foto,
                Criador = _contexto.Usuarios.FirstOrDefault(t => t.Id == postagem.Criador.Id),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Id == postagem.Tema.Id)
            });
            await _contexto.SaveChangesAsync();

            //Função Auxiliar
            bool ExisteIdCriador(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }

            bool ExisteIdTema(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        public async Task AtualizarPostagemAsync(Postagem postagem)
        {
            if (!ExisteIdTema(postagem.Tema.Id)) throw new Exception("id do Tema não encontrado");

            var auxiliar = await PegarPostagemPeloIdAsync(postagem.Id);
            auxiliar.Titulo = postagem.Titulo;
            auxiliar.Descricao = postagem.Descricao;
            auxiliar.URL_Foto = postagem.URL_Foto;
            auxiliar.Tema = _contexto.Temas.FirstOrDefault(t => t.Id == postagem.Tema.Id);
            _contexto.Postagens.Update(auxiliar);
            await _contexto.SaveChangesAsync();

            //Função Auxiliar
            bool ExisteIdTema(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);

                return auxiliar != null;
            }
        }

        public async Task DeletarPostagemAsync(int id)
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public Task<List<Postagem>> PegarTodasPostagensAsync()
        {
            throw new NotImplementedException();
        }

        public Task NovaPostagemAsync(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}