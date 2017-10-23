using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Global
{
    [RequireComponent(typeof(AudioSource))]
    public class Personagem : MonoBehaviour
    {
        internal bool _Ativo;
        internal Vector2 _Eixo;
        internal Rigidbody2D _Controle;
        internal Animator _Animacao;
        internal float _VelocidadeAtual;
        internal AudioSource _ControleSons;
        public BarraProgresso _BarraVida;
        public float _Velocidade;
        public EDirecao _Direcao;
        public EAcoes _Acao;

        public bool Ativo
        {
            get
            {
                return _Ativo;
            }
            set
            {
                StartCoroutine(Ativa(value));
            }
        }

        public enum EDirecao
        {
            Direita = -1,
            Esquerda = 1
        }

        public enum EAcoes
        {
            #region Global
            Morrendo,
            #endregion
            #region Inimigos
            Atacando,
            Patrulhando,
            Perseguindo,
            #endregion
            #region Jogadores
            Parado,
            Andando,
            Correndo,
            Combo1,
            Combo2,
            Combo3,
            #endregion
            #region Miguel
            LevatandoVoo,
            Voando,
            Pousando
            #endregion
        }

        internal virtual void Start()
        {
            _Controle = GetComponent<Rigidbody2D>();
            _Animacao = GetComponent<Animator>();
            _ControleSons = GetComponent<AudioSource>();
            _VelocidadeAtual = _Velocidade;
            _Ativo = true;
        }

        internal virtual void Update()
        {
            if (_Eixo.x < 0)
                _Direcao = EDirecao.Direita;
            else if (_Eixo.x > 0)
                _Direcao = EDirecao.Esquerda;
            Vector3 lScala = transform.localScale;
            lScala.x = (int)_Direcao;
            transform.localScale = lScala;
            if (Ativo)
            {
                if (_BarraVida.gameObject.activeSelf)
                {
                    if (_BarraVida._Atual <= 0)
                    {
                        Morre();
                    }
                }
            }
            _Animacao.SetInteger("Acao", (int)_Acao);
        }

        internal virtual void FixedUpdate()
        {
            if (Ativo)
                _Controle.velocity = _Eixo * _VelocidadeAtual * Time.fixedDeltaTime;
        }

        internal virtual void Morre()
        {
            _Acao = EAcoes.Morrendo;
            _VelocidadeAtual = 0;
        }

        internal virtual IEnumerator Ativa(bool pValor)
        {
            yield return new WaitForSeconds(0.5f);
            _Ativo = pValor;
        }

        public virtual void Destruir()
        {
            Destroy(gameObject);
        }
    }
}
