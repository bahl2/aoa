using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class FontesRecursos : IBD
    {
        private bool _Retorno;
        [NonSerialized]
        private List<FontesRecursos> _Registros;
        public int _IdFontesRecursos;
        public int _IdUsuarios;
        public int _IdTiposRecursos;
        public int _Chave;
        public int _Mineradores;
        public int _RecursosColetados;
        public float _SegundosDuracao;
        public float _SegundosColetagem;
        public int _Nivel;
        public bool _Ativa;
        public bool Retorno { get { return _Retorno; } }
        public List<FontesRecursos> Registros { get { return _Registros; } }

        public FontesRecursos()
        {
            _IdFontesRecursos = 0;
            _IdUsuarios = 0;
            _IdTiposRecursos = 0;
            _Mineradores = 0;
            _SegundosDuracao = 0;
            _RecursosColetados = 0;
            _SegundosColetagem = 0;
            _Nivel = 0;
            _Ativa = false;
        }

        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert Into FontesRecursos Set Chave = {0}, IdUsuarios = {1}, IdTiposRecursos = {2}, SegundosDuracao = {3}" +
                ", Nivel = {4}, Ativa = {5}", _Chave, _IdUsuarios, _IdTiposRecursos, _SegundosDuracao, _Nivel, _Ativa);
            if (_Ativa)
            {
                lSQL += string.Format(", RecursosColetados = {0}, Mineradores = {1}, SegundosColetagem", _RecursosColetados, _Mineradores, _SegundosColetagem);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdFontesRecursos = (Select Max(IdFontesRecursos) from FontesRecursos)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdFontesRecursos, Chave, IdUsuarios, IdTiposRecursos, Mineradores, SegundosDuracao, RecursosColetados" +
                ", SegundosColetagem, Nivel, Ativa from FontesRecursos";
            if (!string.IsNullOrEmpty(pFiltro))
            {
                lSQL += string.Format(" Where {0}", pFiltro);
            }
            if (!string.IsNullOrEmpty(pOrdem))
            {
                lSQL += string.Format(" Order By {0}", pOrdem);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && !lWWW.text.Contains("FALSE");
            if (_Retorno)
            {
                JSONNode lJason = JSONNode.Parse(lWWW.text);
                _Registros = new List<FontesRecursos>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdFontesRecursos = lJason[i]["IdFontesRecursos"].AsInt;
                        _Chave = lJason[i]["Chave"].AsInt;
                        _IdTiposRecursos = lJason[i]["IdTiposRecursos"].AsInt;
                        _IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        _Mineradores = lJason[i]["Mineradores"].AsInt;
                        _SegundosDuracao = lJason[i]["SegundosDuracao"].AsInt;
                        _RecursosColetados = lJason[i]["RecursosColetados"].AsInt;
                        _Nivel = lJason[i]["Nivel"].AsInt;
                        _SegundosColetagem = lJason[i]["SegundosColetagem"].AsFloat;
                        _Ativa = lJason[i]["Ativa"].AsBool;
                        FontesRecursos lFonteRecursosAtual = new FontesRecursos();
                        lFonteRecursosAtual._IdFontesRecursos = lJason[i]["IdFontesRecursos"].AsInt;
                        lFonteRecursosAtual._Chave = lJason[i]["Chave"].AsInt;
                        lFonteRecursosAtual._IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        lFonteRecursosAtual._IdTiposRecursos = lJason[i]["IdTiposRecursos"].AsInt;
                        lFonteRecursosAtual._Mineradores = lJason[i]["Mineradores"].AsInt;
                        lFonteRecursosAtual._SegundosDuracao = lJason[i]["SegundosDuracao"].AsInt;
                        lFonteRecursosAtual._RecursosColetados = lJason[i]["RecursosColetados"].AsInt;
                        lFonteRecursosAtual._Nivel = lJason[i]["Nivel"].AsInt;
                        lFonteRecursosAtual._SegundosColetagem = lJason[i]["SegundosColetagem"].AsFloat;
                        lFonteRecursosAtual._Ativa = lJason[i]["Ativa"].AsBool;
                        Registros.Add(lFonteRecursosAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdFontesRecursos;
            }
            yield return Read(string.Format("IdFontesRecursos = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByIdUsuario(int pIdUsuario = 0, int pIdTipoRecuso = 0, int pChave = 0, string pOrdem = "")
        {

            if (pIdUsuario < 1)
            {
                pIdUsuario = _IdUsuarios;
            }
            string lFiltro = string.Format("IdUsuarios = {0}", pIdUsuario);
            if (pIdTipoRecuso < 1)
            {
                pIdTipoRecuso = _IdTiposRecursos;
            }
            if (pIdTipoRecuso > 0)
            {
                lFiltro += string.Format(" and IdTiposRecursos = {0}", pIdTipoRecuso);
            }
            if (pChave < 1)
            {
                pChave = _Chave;
            }
            if (pChave > 0)
            {
                lFiltro += string.Format(" and Chave = {0}", pChave);
            }
            yield return Read(lFiltro, pOrdem);
        }

        public IEnumerator Update(string pFiltro = "")
        {
            string lSQL = string.Format("Update FontesRecursos set Mineradores = {0}, RecursosColetados = {1}", _Mineradores, _RecursosColetados);
            if (_Chave > 0)
            {
                lSQL += string.Format(", Chave = {0}", _Chave);
            }
            if (_IdTiposRecursos > 0)
            {
                lSQL += string.Format(", IdTiposRecursos = {0}", _IdTiposRecursos);
            }
            if (_IdUsuarios > 0)
            {
                lSQL += string.Format(", IdUsuarios = {0}", _IdUsuarios);
            }
            if (_Nivel > 0)
            {
                lSQL += string.Format(", Nivel = {0}", _Nivel);
            }
            if (_SegundosDuracao > 0)
            {
                lSQL += string.Format(", SegundosDuracao = {0}", _SegundosDuracao);
            }
            if (_Ativa)
            {
                lSQL += string.Format(", RecursosColetados = {0}, Mineradores = {1}, SegundosColetagem = {2}, Ativa = {3}", _RecursosColetados,
                    _Mineradores, _SegundosColetagem, _Ativa);
            }
            if (!string.IsNullOrEmpty(pFiltro))
            {
                lSQL += string.Format(" Where {0}", pFiltro);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
        }

        public IEnumerator UpdateById(int pId = 0)
        {
            if (pId < 1)
            {
                pId = _IdFontesRecursos;
            }
            yield return Update(string.Format("IdFontesRecursos = {0}", pId));
        }

        public IEnumerator Delete(string pFiltro = "")
        {
            string lSQL = "Delete from FontesRecursos";
            if (!string.IsNullOrEmpty(pFiltro))
            {
                lSQL += string.Format(" Where {0}", pFiltro);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
        }

        public IEnumerator DeleteById(int pId = 0)
        {
            if (pId < 1)
            {
                pId = _IdFontesRecursos;
            }
            yield return Delete(string.Format("IdFontesRecursos = {0}", pId));
        }
    }
}
