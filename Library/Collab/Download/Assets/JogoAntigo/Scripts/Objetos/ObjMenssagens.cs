using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Menssagens : IBD
    {
        private bool _Retorno;
        [NonSerialized]
        private List<Menssagens> _Registros;
        public int _IdMenssagens;
        public int _IdUsuarios;
        public string _Menssagem;
        public bool Retorno { get { return _Retorno; } }
        public List<Menssagens> Registros { get { return _Registros; } }

        public Menssagens()
        {
            _IdMenssagens = 0;
            _IdUsuarios = 0;
            _Menssagem = string.Empty;
        }
        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert Into Menssagens Set IdUsuarios = {0}, Menssagem = '{1}'", _IdUsuarios, _Menssagem);
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdMenssagens = (Select Max(IdMenssagens) from Menssagens)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdMenssagens, IdUsuarios, Menssagem from Menssagens";
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
                _Registros = new List<Menssagens>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdMenssagens = lJason[i]["IdMenssagens"].AsInt;
                        _IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        _Menssagem = lJason[i]["Menssagem"];
                        Menssagens lMenssagemAtual = new Menssagens();
                        lMenssagemAtual._IdMenssagens = lJason[i]["IdMenssagens"].AsInt;
                        lMenssagemAtual._IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        lMenssagemAtual._Menssagem = lJason[i]["Menssagem"];
                        Registros.Add(lMenssagemAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdMenssagens;
            }
            yield return Read(string.Format("IdMenssagens = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByIdUsuario(int pIdUsuarios = 0, string pOrdem = "")
        {
            if (pIdUsuarios < 1)
            {
                pIdUsuarios = _IdUsuarios;
            }
            yield return Read(string.Format("IdUsuarios = {0}", pIdUsuarios), pOrdem);
        }

        public IEnumerator Update(string pFiltro = "")
        {
            string lSQL = string.Format("Update Menssagens set Menssagem = '{0}'", _Menssagem);
            if (_IdUsuarios > 0)
            {
                lSQL += string.Format(", IdUsuarios = {0}", _IdUsuarios);
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
                pId = _IdMenssagens;
            }
            yield return Update(string.Format("IdMenssagens = {0}", pId));
        }

        public IEnumerator Delete(string pFiltro = "")
        {
            string lSQL = "Delete from Menssagens";
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
                pId = _IdMenssagens;
            }
            yield return Delete(string.Format("IdMenssagens = {0}", pId));
        }
    }
}
