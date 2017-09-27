using System;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class TiposUnidadesMoveis
    {
        public int _IdTiposUnidadesMoveis;
        public int _IdTiposConstrucoes;
        public string _Descricao;
        public int _Cristais;
        public int _Madeira;
        public Sprite _Foto;

        public TiposUnidadesMoveis()
        {
            _IdTiposUnidadesMoveis = 0;
            _Descricao = string.Empty;
            _Cristais = 0;
            _Madeira = 0;
            _Foto = null;
        }
    }
}
