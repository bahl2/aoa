using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    public class RlcFontesRecursosExercitos : IBD
    {
        private bool _Retorno;
        [NonSerialized]
        private List<RlcFontesRecursosExercitos> _Registros;
        public int _IdRlcFontesRecursosExercitos;
        public int _IdFontesRecursos;
        public int _IdExercitos;
        public bool Retorno { get { return _Retorno; } }
        public List<RlcFontesRecursosExercitos> Registros { get { return _Registros; } }

        public RlcFontesRecursosExercitos()
        {
            _IdRlcFontesRecursosExercitos = 0;
            _IdFontesRecursos = 0;
            _IdExercitos = 0;
        }

        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert Into RlcFontesRecursosExercitos Set IdFontesRecursos = {0}, IdExercitos = {1}", _IdFontesRecursos,
                _IdExercitos);
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdRlcFontesRecursosExercitos = (Select Max(IdRlcFontesRecursosExercitos) from RlcFontesRecursosExercitos)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdRlcFontesRecursosExercitos, IdFontesRecursos, IdExercitos from RlcFontesRecursosExercitos";
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
                _Registros = new List<RlcFontesRecursosExercitos>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdRlcFontesRecursosExercitos = lJason[i]["IdRlcFontesRecursosExercitos"].AsInt;
                        _IdFontesRecursos = lJason[i]["IdFontesRecursos"].AsInt;
                        _IdExercitos = lJason[i]["IdExercitos"].AsInt;
                        RlcFontesRecursosExercitos lRlcFonteRecursosExercitosAtual = new RlcFontesRecursosExercitos();
                        lRlcFonteRecursosExercitosAtual._IdRlcFontesRecursosExercitos = lJason[i]["IdRlcFontesRecursosExercitos"].AsInt;
                        lRlcFonteRecursosExercitosAtual._IdFontesRecursos = lJason[i]["IdFontesRecursos"].AsInt;
                        lRlcFonteRecursosExercitosAtual._IdExercitos = lJason[i]["IdExercitos"].AsInt;
                        Registros.Add(lRlcFonteRecursosExercitosAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdRlcFontesRecursosExercitos;
            }
            yield return Read(string.Format("IdRlcFontesRecursosExercitos = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByIdFontesRecursos(int pIdFontesRecursos = 0, string pOrdem = "")
        {
            if (pIdFontesRecursos < 1)
            {
                pIdFontesRecursos = _IdFontesRecursos;
            }
            yield return Read(string.Format("IdFontesRecursos = {0}", pIdFontesRecursos), pOrdem);
        }

        public IEnumerator ReadByIdExercitos(int pIdExercitos = 0, string pOrdem = "")
        {
            if (pIdExercitos < 1)
            {
                pIdExercitos = _IdExercitos;
            }
            yield return Read(string.Format("IdExercitos = {0}", pIdExercitos), pOrdem);
        }

        public IEnumerator Update(string pFiltro = "")
        {
            string lSQL = string.Format("Update RlcFontesRecursosExercitos set IdFontesRecursos = {0}, IdExercitos = {1}", _IdFontesRecursos,
                _IdExercitos);
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
                pId = _IdRlcFontesRecursosExercitos;
            }
            yield return Update(string.Format("IdRlcFontesRecursosExercitos = {0}", pId));
        }

        public IEnumerator UpdateByIdFontesRecursos(int pIdFontesRecursos = 0)
        {
            if (pIdFontesRecursos < 1)
            {
                pIdFontesRecursos = _IdFontesRecursos;
            }
            yield return Update(string.Format("IdFontesRecursos = {0}", pIdFontesRecursos));
        }

        public IEnumerator UpdateByIdExercitos(int pIdExercitos = 0)
        {
            if (pIdExercitos < 1)
            {
                pIdExercitos = _IdExercitos;
            }
            yield return Update(string.Format("IdExercitos = {0}", pIdExercitos));
        }

        public IEnumerator Delete(string pFiltro = "")
        {
            string lSQL = "Delete from RlcFontesRecursosExercitos";
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
                pId = _IdRlcFontesRecursosExercitos;
            }
            yield return Update(string.Format("IdRlcFontesRecursosExercitos = {0}", pId));
        }

        public IEnumerator DeleteByIdFontesRecursos(int pIdFontesRecursos = 0)
        {
            if (pIdFontesRecursos < 1)
            {
                pIdFontesRecursos = _IdFontesRecursos;
            }
            yield return Delete(string.Format("IdFontesRecursos = {0}", pIdFontesRecursos));
        }

        public IEnumerator DeleteByIdExercitos(int pIdExercitos = 0)
        {
            if (pIdExercitos < 1)
            {
                pIdExercitos = _IdExercitos;
            }
            yield return Delete(string.Format("IdExercitos = {0}", pIdExercitos));
        }
    }
}
