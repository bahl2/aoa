using System;
using UnityEngine;
namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class TiposRecursos
    {
        public int _IdTiposRecursos;
        public string _Descricao;
        public Transform _Cheio;
        public Transform _Vazio;

        public TiposRecursos()
        {
            _IdTiposRecursos = 0;
            _Descricao = string.Empty;
            _Cheio = null;
            _Vazio = null;
        }
    }
}
