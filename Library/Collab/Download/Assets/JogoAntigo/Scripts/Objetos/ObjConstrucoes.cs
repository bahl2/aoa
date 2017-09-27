using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Construcoes : IBD
    {
        private bool _Retorno;
        private Vector3 _Posicao;
        [NonSerialized]
        private List<Construcoes> _Registros;
        public int _IdConstrucoes;
        public int _IdUsuarios;
        public int _IdTiposConstrucoes;
        public float _PosicaoX;
        public float _PosicaoY;
        public float _PosicaoZ;
        public bool _Posicionada;
        public int _Vida;
        public float _SegundosConstruindo;
        public int _Nivel;
        public Vector3 Posicao { get { return _Posicao; } }
        public bool Retorno { get { return _Retorno; } }
        public List<Construcoes> Registros { get { return _Registros; } }

        public Construcoes()
        {
            _IdConstrucoes = 0;
            _IdUsuarios = 0;
            _IdTiposConstrucoes = 0;
            _PosicaoX = 0;
            _PosicaoY = 0;
            _PosicaoZ = 0;
            _Posicao = Vector3.zero;
            _Posicionada = false;
            _Vida = 0;
            _Nivel = 0;
            _SegundosConstruindo = 0;
        }

        public IEnumerator Create()
        {
            string lSQL = string.Format("Insert Into Construcoes Set IdUsuarios = {0}, IdTiposConstrucoes = {1}, Vida = {2}, Posicionada = {3}" +
                ", Nivel = {4}", _IdUsuarios, _IdTiposConstrucoes, _Vida, _Posicionada, _Nivel);
            if (_Posicionada)
            {
                lSQL += string.Format(", PosicaoX = {0}, PosicaoY = {1}, PosicaoZ = {2}", _PosicaoX, _PosicaoY, _PosicaoZ);
            }
            if (_SegundosConstruindo > 0)
            {
                lSQL += string.Format(", SegundosConstruindo = {0}", _SegundosConstruindo);
            }
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdConstrucoes = (Select Max(IdConstrucoes) from Construcoes)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")
        {
            string lSQL = "Select IdConstrucoes, IdUsuarios, IdTiposConstrucoes, PosicaoX, PosicaoY, PosicaoZ, Vida, Posicionada, "
                + "SegundosConstruindo, Nivel from Construcoes";
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
                _Registros = new List<Construcoes>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        _IdConstrucoes = lJason[i]["IdConstrucoes"].AsInt;
                        _IdTiposConstrucoes = lJason[i]["IdTiposConstrucoes"].AsInt;
                        _IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        _PosicaoX = lJason[i]["PosicaoX"].AsFloat;
                        _PosicaoY = lJason[i]["PosicaoY"].AsFloat;
                        _PosicaoZ = lJason[i]["PosicaoZ"].AsFloat;
                        _Posicionada = lJason[i]["Posicionada"].AsBool;
                        _Posicao = new Vector3(_PosicaoX, _PosicaoY, _PosicaoZ);
                        _Vida = lJason[i]["Vida"].AsInt;
                        _SegundosConstruindo = lJason[i]["SegundosConstruindo"].AsFloat;
                        _Nivel = lJason[i]["Nivel"].AsInt;
                        Construcoes lConstrucaoAtual = new Construcoes()
                        {
                            _IdConstrucoes = lJason[i]["IdConstrucoes"].AsInt,
                            _IdUsuarios = lJason[i]["IdUsuarios"].AsInt,
                            _IdTiposConstrucoes = lJason[i]["IdTiposConstrucoes"].AsInt,
                            _PosicaoX = lJason[i]["PosicaoX"].AsFloat,
                            _PosicaoY = lJason[i]["PosicaoY"].AsFloat,
                            _PosicaoZ = lJason[i]["PosicaoZ"].AsFloat,
                            _Posicionada = lJason[i]["Posicionada"].AsBool
                        };
                        lConstrucaoAtual._Posicao = new Vector3(lConstrucaoAtual._PosicaoX, lConstrucaoAtual._PosicaoY, lConstrucaoAtual._PosicaoZ);
                        lConstrucaoAtual._Vida = lJason[i]["Vida"].AsInt;
                        lConstrucaoAtual._SegundosConstruindo = lJason[i]["SegundosConstruindo"].AsFloat;
                        lConstrucaoAtual._Nivel = lJason[i]["Nivel"].AsInt;
                        Registros.Add(lConstrucaoAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")
        {
            if (pId < 1)
            {
                pId = _IdConstrucoes;
            }
            yield return Read(string.Format("IdConstrucoes = {0}", pId), pOrdem);
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
            string lSQL = string.Format("Update Construcoes set Vida = {0}", _Vida);
            if (_IdTiposConstrucoes > 0)
            {
                lSQL += string.Format(", IdTiposConstrucoes = {0}, Posicionada = {1}", _IdTiposConstrucoes, _Posicionada);
            }
            if (_Posicionada)
            {
                lSQL += string.Format(", PosicaoX = {0}, PosicaoY = {1}, PosicaoZ = {2}", _PosicaoX, _PosicaoY, _PosicaoZ);
            }
            if (_SegundosConstruindo > 0)
            {
                lSQL += string.Format(", SegundosConstruindo = {0}", _SegundosConstruindo);
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
                pId = _IdConstrucoes;
            }
            yield return Update(string.Format("IdConstrucoes = {0}", pId));
        }

        public IEnumerator Delete(string pFiltro = "")
        {
            string lSQL = "Delete from Construcoes";
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
                pId = _IdConstrucoes;
            }
            yield return Delete(string.Format("IdConstrucoes = {0}", pId));
        }
    }
}
