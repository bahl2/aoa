using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Usuarios : IBD
    {
        private bool _Retorno;
        [NonSerialized]
        private List<Usuarios> _Registros;
        public int _IdUsuarios;
        public string _Nome;
        public string _Senha;
        public int _Cristal;
        public int _Madeira;
        public int _IdTiposPovos;
        public DateTime _UltimoAcesso;
        public int _Nivel;
        public bool Retorno { get { return _Retorno; } }
        public List<Usuarios> Registros { get { return _Registros; } }

        public Usuarios()
        {
            _IdUsuarios = 0;
            _Nome = string.Empty;
            _Senha = string.Empty;
            _Cristal = 0;
            _Madeira = 0;
            _IdTiposPovos = 0;
            _UltimoAcesso = DateTime.MinValue;
            _Nivel = 0;
        }

        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert into Usuarios set Nome = '{0}', Senha = '{1}', Cristal = {2}, Madeira = {3}" +
                ", UltimoAcesso = '{4}', Nivel = {5}", _Nome, _Senha, _Cristal, _Madeira, _UltimoAcesso.ToString("yyyy-MM-dd H:mm:ss"), _Nivel);
            if (_IdTiposPovos > 0)
            {
                lSQL += string.Format(", IdTiposPovos = {0} ", _IdTiposPovos);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdUsuarios = (Select Max(IdUsuarios) from Usuarios)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdUsuarios, Nome, Senha, Cristal, Madeira, IdTiposPovos, UltimoAcesso, Nivel from Usuarios";
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
                _Registros = new List<Usuarios>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        _Nome = lJason[i]["Nome"];
                        _Senha = lJason[i]["Senha"];
                        _Cristal = lJason[i]["Cristal"].AsInt;
                        _Madeira = lJason[i]["Madeira"].AsInt;
                        _IdTiposPovos = lJason[i]["IdTiposPovos"].AsInt;
                        _UltimoAcesso = DateTime.Parse(lJason[i]["UltimoAcesso"]);
                        _Nivel = lJason[i]["Nivel"].AsInt;
                        Usuarios lUsuarioAtual = new Usuarios();
                        lUsuarioAtual._IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        lUsuarioAtual._Nome = lJason[i]["Nome"];
                        lUsuarioAtual._Senha = lJason[i]["Senha"];
                        lUsuarioAtual._Cristal = lJason[i]["Cristal"].AsInt;
                        lUsuarioAtual._Madeira = lJason[i]["Madeira"].AsInt;
                        lUsuarioAtual._IdTiposPovos = lJason[i]["IdTiposPovos"].AsInt;
                        lUsuarioAtual._UltimoAcesso = DateTime.Parse(lJason[i]["UltimoAcesso"]);
                        lUsuarioAtual._Nivel = lJason[i]["Nivel"].AsInt;
                        Registros.Add(lUsuarioAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdUsuarios;
            }
            yield return Read(string.Format("IdUsuarios = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByNome(string pNome = "")
        {
            if (string.IsNullOrEmpty(pNome))
            {
                pNome = _Nome;
            }
            yield return Read(string.Format("Nome = '{0}'", pNome));
        }

        public IEnumerator Update(string pFiltro = "")
        {
            string lSQL = string.Format("Update Usuarios set UltimoAcesso = '{0}'", _UltimoAcesso.ToString("yyyy-MM-dd H:mm:ss"));
            if (!string.IsNullOrEmpty(_Nome))
            {
                lSQL += string.Format(", Nome = '{0}'", _Nome);
            }
            if (!string.IsNullOrEmpty(_Senha))
            {
                lSQL += string.Format(", Senha = '{0}'", _Senha);
            }
            if (_Cristal >= 0)
            {
                lSQL += string.Format(", Cristal = '{0}'", _Cristal);
            }
            if (_Madeira >= 0)
            {
                lSQL += string.Format(", Madeira = {0}", _Madeira);
            }
            if (_IdTiposPovos > 0)
            {
                lSQL += string.Format(", IdTiposPovos = {0}", _IdTiposPovos);
            }
            if (_Nivel > 0)
            {
                lSQL += string.Format(", Nivel = {0}", _Nivel);
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
                pId = _IdUsuarios;
            }
            yield return Update(string.Format("IdUsuarios = {0}", pId));
        }

        public IEnumerator Delete(string pFiltro = "")
        {
            string lSQL = "Delete from Usuarios";
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
                pId = _IdUsuarios;
            }
            yield return Delete(string.Format("IdUsuarios = {0}", pId));
        }
    }
}
