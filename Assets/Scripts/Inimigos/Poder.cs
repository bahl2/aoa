using Assets.Scripts.Jogadores;
using UnityEngine;

namespace Assets.Scripts.Inimigos
{
    public class Poder : MonoBehaviour
    {
        [SerializeField]
        private bool _Jogador;
        public float _Velocidade;
        public Rigidbody2D _Controle;

        private void Awake()
        {
            _Controle = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 5);
        }

        private void OnTriggerStay2D(Collider2D pColisao)
        {
            if (_Jogador)
            {
                Inimigo lInimigo = pColisao.GetComponent<Inimigo>();
                if (lInimigo != null)
                {
                    lInimigo._BarraVida.Add(-10);
                    Destroy(gameObject);
                }
            }
            else
            {
                Jogador lJogador = pColisao.GetComponent<Jogador>();
                if (lJogador != null)
                {
                    lJogador._BarraVida.Add(-10);
                    Destroy(gameObject);
                }
            }
        }
    }
}
