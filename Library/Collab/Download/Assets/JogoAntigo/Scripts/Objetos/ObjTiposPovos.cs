using System;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class TiposPovos
    {
        public int _IdTiposPovos;
        public string _Descricao;

        public TiposPovos()
        {
            _IdTiposPovos = 0;
            _Descricao = string.Empty;
        }
    }
}
