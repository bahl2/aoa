using System;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class TiposHerois
    {
        public int _IdTiposHerois;
        public string _Descricao;
        public Sprite _Foto;
        public Transform _Prefab;

        public TiposHerois()
        {
            _IdTiposHerois = 0;
            _Descricao = string.Empty;
        }
    }
}
