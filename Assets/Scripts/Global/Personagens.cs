using UnityEngine;

public class Personagens : MonoBehaviour
{
    public BarraVida _BarraVida;
    public float _Velocidade;
    public EDirecao _Direcao;
    internal Vector2 _Eixo;
    internal Rigidbody2D _Controle;
    internal Animator _Animacao;
    internal float _VelocidadeAtual;
    public EAcoes _Acao;

    public enum EDirecao
    {
        Direita = -1,
        Esquerda = 1
    }

    public enum EAcoes
    {
        #region Global
        Morrendo = 0,
        Atacando = 1,
        #endregion
        #region Inimigos
        Patrulhando = 2,
        Perseguindo = 3,
        #endregion 
        #region Jogadores
        Parado = 4,
        Andando = 5,
        Correndo = 6,
        Ataque1 = 7,
        Ataque2 = 8,
        Ataque3 = 9,
        #endregion
        #region Miguel
        LevatandoVoo = 10,
        Voando = 11,
        Pousando = 12
        #endregion
    }

    internal virtual void Start()
    {
        _Controle = GetComponent<Rigidbody2D>();
        _Animacao = GetComponent<Animator>();
        _VelocidadeAtual = _Velocidade;
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
        if (_BarraVida.gameObject.activeSelf)
        {
            if (_BarraVida._VidaAtual <= 0)
            {
                Morre();
            }
        }
        _Animacao.SetInteger("Acao", (int)_Acao);
    }

    internal virtual void FixedUpdate()
    {
        _Controle.velocity = _Eixo * _VelocidadeAtual * Time.fixedDeltaTime;
    }

    internal virtual void Morre()
    {
        _Acao = EAcoes.Morrendo;
        _VelocidadeAtual = 0;
    }

    public virtual void Destruir()
    {
        Destroy(gameObject);
    }
}
