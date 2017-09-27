using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    public class RlcConstrucoesExercitos
    {
        private bool _Retorno;
        [NonSerialized]
        private List<RlcConstrucoesExercitos> _Registros;
        public int _IdRlcConstrucoesExercitos;
        public int _IdConstrucoes;
        public int _IdExercitos;
        public bool Retorno { get { return _Retorno; } }
        public List<RlcConstrucoesExercitos> Registros { get { return _Registros; } }

        public RlcConstrucoesExercitos()
        {
            _IdRlcConstrucoesExercitos = 0;
            _IdConstrucoes = 0;
            _IdExercitos = 0;
        }

        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert Into RlcConstrucoesExercitos Set IdConstrucoes = {0}, IdExercitos = {1}", _IdConstrucoes, _IdExercitos);
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdRlcConstrucoesExercitos = (Select Max(IdRlcConstrucoesExercitos) from RlcConstrucoesExercitos)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdRlcConstrucoesExercitos, IdConstrucoes, IdExercitos from RlcConstrucoesExercitos";
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
                _Registros = new List<RlcConstrucoesExercitos>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdRlcConstrucoesExercitos = lJason[i]["IdRlcConstrucoesExercitos"].AsInt;
                        _IdConstrucoes = lJason[i]["IdConstrucoes"].AsInt;
                        _IdExercitos = lJason[i]["IdExercitos"].AsInt;
                        RlcConstrucoesExercitos lRlcConstrucoesExercitosAtual = new RlcConstrucoesExercitos()
                        {
                            _IdRlcConstrucoesExercitos = lJason[i]["IdRlcConstrucoesExercitos"].AsInt,
                            _IdConstrucoes = lJason[i]["IdConstrucoes"].AsInt,
                            _IdExercitos = lJason[i]["IdExercitos"].AsInt
                        };
                        Registros.Add(lRlcConstrucoesExercitosAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdRlcConstrucoesExercitos;
            }
            yield return Read(string.Format("IdRlcConstrucoesExercitos = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByIdConstrucoes(int pIdConstrucoes = 0, string pOrdem = "")
        {
            if (pIdConstrucoes < 1)
            {
                pIdConstrucoes = _IdConstrucoes;
            }
            yield return Read(string.Format("IdConstrucoes = {0}", pIdConstrucoes), pOrdem);
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
            string lSQL = string.Format("Update RlcConstrucoesExercitos set IdConstrucoes = {0}, IdExercitos = {1}", _IdConstrucoes,
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
                pId = _IdRlcConstrucoesExercitos;
            }
            yield return Update(string.Format("IdRlcConstrucoesExercitos = {0}", pId));
        }

        public IEnumerator UpdateByIdConstrucoes(int pIdConstrucoes = 0)
        {
            if (pIdConstrucoes < 1)
            {
                pIdConstrucoes = _IdConstrucoes;
            }
            yield return Update(string.Format("IdConstrucoes = {0}", pIdConstrucoes));
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
            string lSQL = "Delete from RlcConstrucoesExercitos";
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
                pId = _IdRlcConstrucoesExercitos;
            }
            yield return Delete(string.Format("IdRlcConstrucoesExercitos = {0}", pId));
        }

        public IEnumerator DeleteByIdConstrucoes(int pIdConstrucoes = 0)
        {
            if (pIdConstrucoes < 1)
            {
                pIdConstrucoes = _IdConstrucoes;
            }
            yield return Delete(string.Format("IdConstrucoes = {0}", pIdConstrucoes));
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
