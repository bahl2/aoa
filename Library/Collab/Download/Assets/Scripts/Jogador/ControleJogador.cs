using ArcadePUCCampinas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    [SerializeField]
    private float _Velocidade;
    private float _VelocidadeAtual;
    private int _Jogador;
    private Animator _Animacao;
    private Vector2 _Parado;
    private Vector2 _Eixo;
    private Rigidbody2D _Controle;
    private float _Tempo;
    private EAnimacao _AnimacaoAtual;
    public BarraVida _BarraVida;
    private bool _Atacando;

    private enum EAnimacao
    {
        Parado,
        Andando,
        Correndo,
        Atacando
    }

    private void Start()
    {
        _Animacao = GetComponent<Animator>();
        _Controle = GetComponent<Rigidbody2D>();
        _VelocidadeAtual = _Velocidade;
    }

    private void Update()
    {
        if (_BarraVida._VidaAtual <= 0)
        {
            SceneManager.LoadScene("Menu Inicial");
        }
        _Eixo.y = InputArcade.Eixo(_Jogador, EEixo.VERTICAL);
        _Eixo.x = InputArcade.Eixo(_Jogador, EEixo.HORIZONTAL);
        if (InputArcade.Apertou(_Jogador, EControle.VERDE))
            _Atacando = true;
        Animacao();
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    private void Movimento()
    {
        _Controle.velocity = new Vector2(_Eixo.x * Time.fixedDeltaTime * _VelocidadeAtual, _Eixo.y * Time.fixedDeltaTime * _VelocidadeAtual);
    }

    private void Animacao()
    {
        if (_Eixo.x > 0 || _Eixo.x < 0)
        {
            _Parado = new Vector2(Mathf.FloorToInt(_Eixo.x), 0);
        }
        if (_Eixo.y > 0 || _Eixo.y < 0)
        {
            _Parado = new Vector2(0, Mathf.FloorToInt(_Eixo.y));
        }
        if (_Atacando)
        {
            //_VelocidadeAtual = 0;
            _AnimacaoAtual = EAnimacao.Atacando;
        }
        else if (_Eixo == Vector2.zero)
        {
            _VelocidadeAtual = _Velocidade;
            _AnimacaoAtual = EAnimacao.Parado;
        }
        else if ((_Eixo.x >= -0.5f && _Eixo.x <= 0.5f) || (_Eixo.y >= -0.5 && _Eixo.y <= 0.5f))
        {
            _VelocidadeAtual = _Velocidade;
            _AnimacaoAtual = EAnimacao.Andando;
        }
        else
        {
            _VelocidadeAtual = _Velocidade;
            _AnimacaoAtual = EAnimacao.Correndo;
        }
        _Animacao.SetFloat("AndandoX", (int)_Eixo.x);
        _Animacao.SetFloat("AndandoY", (int)_Eixo.y);
        _Animacao.SetInteger("Animacao", (int)_AnimacaoAtual);
        _Animacao.SetFloat("ParadoY", _Parado.y);
        _Animacao.SetFloat("ParadoX", _Parado.x);
    }

    public void TerminaAtaque()
    {
        _Atacando = false;
    }
}
