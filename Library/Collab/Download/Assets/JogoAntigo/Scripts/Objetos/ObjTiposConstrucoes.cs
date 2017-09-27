using System;
using UnityEngine;
namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class TiposConstrucoes
    {
        public int _IdTiposConstrucoes;
        public string _Descricao;
        public int _Cristais;
        public int _Madeira;
        public int _Aldeoes;
        public float _Segundos;
        public Transform _Prefab;
        public Sprite _Foto;

        public TiposConstrucoes()
        {
            _IdTiposConstrucoes = 0;
            _Descricao = string.Empty;
            _Cristais = 0;
            _Madeira = 0;
            _Aldeoes = 0;
            _Segundos = 0;
            _Prefab = null;
            _Foto = null;
        }
    }
}
