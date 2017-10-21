using UnityEngine;

namespace Assets.Scripts.Inimigos
{
    public class ControleDementador : Inimigo
    {
        [SerializeField]
        private Transform _PrefabPoder;
        [SerializeField]
        private Transform _Mao;

        private bool VendoJogador()
        {
            bool lVendo = false;
            RaycastHit2D[] lHits = Physics2D.RaycastAll(transform.position, _Eixo, _DistanciaMin);
            Debug.DrawRay(transform.position, _Eixo, Color.red, _DistanciaMin);
            foreach (RaycastHit2D lHit in lHits)
            {
                if (_Jogador.transform == lHit.transform)
                    lVendo = true;
            }
            return lVendo;
        }

        internal override void Persegue()
        {
            base.Persegue();
            if (_TempoAtacando > _TempoAtaque)
            {
                _TempoAtacando = 0;
                Transform lPoderTransform = Instantiate(_PrefabPoder, _Mao.position, _Mao.rotation);
                Poder lPoder = lPoderTransform.GetComponent<Poder>();
                lPoder._Controle.velocity = lPoder._Velocidade * _Eixo;
            }
            if (VendoJogador())
            {
                _VelocidadeAtual = 0;
            }
            else
            {
                _VelocidadeAtual = _Velocidade;
                _Eixo = (_Jogador.transform.position - transform.position).normalized;
            }
        }
    }
}
