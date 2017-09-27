using Assets.Scripts.Interfaces;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Exercitos : IBD
    {
        private bool _Retorno;
        [NonSerialized]
        private List<Exercitos> _Registros;
        public int _IdExercitos; // o id é auto incremento por isso não é atribuido no create
        public int _IdUsuarios;
        public int _IdTiposUnidadesMoveis;
        public int _Vida;
        public bool _Ocupado;
        public int _Nivel;
        public bool Retorno { get { return _Retorno; } }// o retorno me traz se o codigo foi executado com sucesso        
        public List<Exercitos> Registros { get { return _Registros; } }// é uma lista com todos os registro retornados no read                
        // limpa as propriedades da classe e instancia um novo exercito
        public Exercitos()
        {
            _IdExercitos = 0;
            _IdUsuarios = 0;
            _IdTiposUnidadesMoveis = 0;
            _Vida = 0;
            _Ocupado = false;
            _Nivel = 0;
        }
        //todo sql tem ser executado em uma corrotina pois o retorno não é imediato
        public IEnumerator Create()// insere um registro na tabela vou melhorar para inserir apenas com os campos preenchidos
        {
            string lSQL = string.Format("Insert into Exercitos Set IdUsuarios = {0}, IdTiposUnidadesMoveis = {1}, Vida = {2}, Ocupado = {3}" +
                ", Nivel = {4}", _IdUsuarios, _IdTiposUnidadesMoveis, _Vida, _Ocupado, _Nivel);
            // carrega uma url onde o segundo parametro pode ser usado para dados do formulario php que é mais seguro, preciso melhorar isso                        
            WWWForm lForm = new WWWForm();
            lForm.AddField("SQL", lSQL);
            WWW lWWW = new WWW(GameTags.UrlExecQuery(), lForm);
            yield return lWWW;//sempre usar o yield return pois ele aquarda o retorno do php para executar o resto dos comandos            
            _Retorno = lWWW.error == null && lWWW.text.Contains("TRUE");
            yield return Read("IdExercitos = (Select Max(IdExercitos) from Exercitos)");
        }

        public IEnumerator Read(string pFiltro = "", string pOrdem = "")//le registros da tabela e adiciona a lista de registros
        {
            string lSQL = "Select IdExercitos, IdUsuarios, IdTiposUnidadesMoveis, Vida, Ocupado from Exercitos";
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
                _Registros = new List<Exercitos>();
                if (lJason != null)
                {
                    for (int i = 0; i < lJason.Count; i++)
                    {
                        //utiliso jason pois é mais serto que o dado vai vir da forma correta essa classe foi encontrada na internet como simplejason
                        _IdExercitos = lJason[i]["IdExercitos"].AsInt;
                        _IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        _IdTiposUnidadesMoveis = lJason[i]["IdTiposUnidadesMoveis"].AsInt;
                        _Vida = lJason[i]["Vida"].AsInt;
                        _Ocupado = lJason[i]["Ocupado"].AsBool;
                        _Nivel = lJason[i]["Nivel"].AsInt;
                        Exercitos lExercitoAtual = new Exercitos();
                        lExercitoAtual._IdExercitos = lJason[i]["IdExercitos"].AsInt;
                        lExercitoAtual._IdUsuarios = lJason[i]["IdUsuarios"].AsInt;
                        lExercitoAtual._IdTiposUnidadesMoveis = lJason[i]["IdTiposUnidadesMoveis"].AsInt;
                        lExercitoAtual._Vida = lJason[i]["Vida"].AsInt;
                        lExercitoAtual._Ocupado = lJason[i]["Ocupado"].AsBool;
                        lExercitoAtual._Nivel = lJason[i]["Nivel"].AsInt;
                        Registros.Add(lExercitoAtual);
                    }
                }
            }
        }

        public IEnumerator ReadById(int pId = 0, string pOrdem = "")//filtra como padrao o id da tabela
        {
            if (pId < 1)
            {
                pId = _IdExercitos;
            }
            yield return Read(string.Format("IdExercitos = {0}", pId), pOrdem);
        }

        public IEnumerator ReadByIdUsuario(int pIdUsuario = 0, int pIdTipoUnidadeMovel = 0, string pOrdem = "")// filtro do id do usuario dono do exercito
        {
            if (pIdUsuario < 1)
            {
                pIdUsuario = _IdUsuarios;
            }
            string lFiltro = string.Format("IdUsuarios = {0}", pIdUsuario);
            if (pIdTipoUnidadeMovel < 1)
            {
                pIdTipoUnidadeMovel = _IdTiposUnidadesMoveis;
            }
            if (pIdTipoUnidadeMovel > 0)
            {
                lFiltro += string.Format(" And IdTiposUnidadesMoveis = {0}", pIdTipoUnidadeMovel);
            }
            yield return Read(lFiltro, pOrdem);
        }

        public IEnumerator ReadByIdUsuario(bool pOcupado, int pIdUsuario = 0, int pIdTipoUnidadeMovel = 0, string pOrdem = "")
        {
            if (pIdUsuario < 1)
            {
                pIdUsuario = _IdUsuarios;
            }
            string lFiltro = string.Format("IdUsuarios = {0} And Ocupado = {1}", pIdUsuario, pOcupado);
            if (pIdTipoUnidadeMovel < 1)
            {
                pIdTipoUnidadeMovel = _IdTiposUnidadesMoveis;
            }
            if (pIdTipoUnidadeMovel > 0)
            {
                lFiltro += string.Format(" And IdTiposUnidadesMoveis = {0}", pIdTipoUnidadeMovel);
            }
            yield return Read(lFiltro, pOrdem);
        }

        public IEnumerator Update(string pFiltro = "")// atualiza os registros da tabela
        {
            string lSQL = string.Format("Update Exercitos set Vida = {0}, Ocupado = {1}", _Vida, _Ocupado);
            if (_IdUsuarios > 0)
            {
                lSQL += string.Format(", IdUsuarios = {0}", _IdUsuarios);
            }
            if (_IdTiposUnidadesMoveis > 0)
            {
                lSQL += string.Format(", IdTiposUnidadesMoveis = {0}", _IdTiposUnidadesMoveis);
            }
            if (_Nivel > 0)
            {
                lSQL += string.Format(", Nivel = {0}", _Nivel);
            }
            if (!string.IsNullOrEmpty(pFiltro))
            {
                lSQL += string.Format(" Where {0}", pFiltro); ;
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
                pId = _IdExercitos;
            }
            yield return Update(string.Format("IdExercitos = {0}", pId));
        }

        public IEnumerator UpdateByIds()
        {
            string lIds = string.Empty;
            foreach (Exercitos lExercitoAtual in Registros)
            {
                if (lIds != string.Empty)
                {
                    lIds += string.Format(", {0}", lExercitoAtual._IdExercitos);
                }
                else
                {
                    lIds = lExercitoAtual._IdExercitos.ToString();
                }
            }
            yield return Update(string.Format("IdExercitos in ({0})", lIds));
        }

        public IEnumerator Delete(string pFiltro = "")//apaga os registros da tabela ainda tenho que testar
        {
            string lSQL = "Delete from Exercitos";
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
                pId = _IdExercitos;
            }
            yield return Delete(string.Format("IdExercitos = {0}", pId));
        }

        public IEnumerator DeleteByIds()
        {
            string lIds = string.Empty;
            foreach (Exercitos lExercitoAtual in Registros)
            {
                if (lIds != string.Empty)
                {
                    lIds += string.Format(", {0}", lExercitoAtual._IdExercitos);
                }
                else
                {
                    lIds = lExercitoAtual._IdExercitos.ToString();
                }
            }
            yield return Delete(string.Format("IdExercitos in ({0})", lIds));
        }
    }
}
