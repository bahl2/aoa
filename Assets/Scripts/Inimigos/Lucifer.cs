using UnityEngine;

namespace Assets.Scripts.Inimigos
{
    public class Lucifer : Inimigo
    {
        [SerializeField]
        private Transform _PrefabPoder;
        [SerializeField]
        private Transform _Mao;

        public void Poder()
        {
            Transform lPoderTransform = Instantiate(_PrefabPoder, _Mao.position, _Mao.rotation);
            Poder lPoder = lPoderTransform.GetComponent<Poder>();
            lPoder._Controle.velocity = lPoder._Velocidade * _Eixo;
        }
    }
}
