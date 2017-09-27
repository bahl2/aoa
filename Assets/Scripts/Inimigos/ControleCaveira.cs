using UnityEngine;

public class ControleCaveira : Inimigo
{
    [SerializeField]
    private float _Velocidade;
    [SerializeField]
    private float _DistanciaMin;
    [SerializeField]
    private float _TempoPatrulhar;
    [SerializeField]
    private float _TempoAtaque;    
    private float _TempoPatrulhando;
    private Jogador _Jogador;
    private Rigidbody2D _Controle;
    private float _TempoAtacando;
    private EEstados _EstadoAtual;

    private enum EEstados
    {
        Patrulhando,
        Perseguindo,
        Atacando
    }

    internal override void Start()
    {
        base.Start();
        _Jogador = FindObjectOfType<Jogador>();
        _Controle = GetComponent<Rigidbody2D>();
        _EstadoAtual = EEstados.Patrulhando;
        _Eixo = transform.TransformDirection(Vector2.right);
        _TempoPatrulhando = 0;
        _TempoAtacando = 0;
        _VelocidadeAtual = _Velocidade;
    }

    internal override void Update()
    {
        base.Update();       
        MovimentoIA();
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    private void Movimento()
    {
        _Controle.velocity = _Eixo * _VelocidadeAtual * Time.fixedDeltaTime;
    }

    private void MovimentoIA()
    {
        if (_EstadoAtual == EEstados.Patrulhando)
        {
            _VelocidadeAtual = _Velocidade;
            if (JogadorPerto())
            {
                Perseguir();
            }
            else
            {
                Patrulhar();
            }
        }
        else if (_EstadoAtual == EEstados.Perseguindo)
        {
            _VelocidadeAtual = _Velocidade;
            if (JogadorPerto())
            {
                Perseguir();
            }
            else
            {
                Patrulhar();
            }
        }
        else if (_EstadoAtual == EEstados.Atacando)
        {
            Atacar();
        }
        _TempoAtacando += Time.deltaTime;
        _TempoPatrulhando += Time.deltaTime;
    }

    private bool JogadorPerto()
    {
        Vector3 lDistancia = transform.position - _Jogador.transform.position;
        return lDistancia.magnitude <= _DistanciaMin;
    }

    private void Patrulhar()
    {
        _EstadoAtual = EEstados.Patrulhando;
        if (_TempoPatrulhando > _TempoPatrulhar)
        {
            TrocarDirecao();
            _TempoPatrulhando = 0;
        }
    }

    private void Atacar()
    {
        _EstadoAtual = EEstados.Atacando;        
        if (_TempoAtacando > _TempoAtaque)
        {
            _TempoAtacando = 0;
            _VelocidadeAtual = 0;
            _Jogador._BarraVida.Dano = 10;
        }
    }

    private void TrocarDirecao()
    {
        _Eixo = _Eixo * -1;
    }

    private void Perseguir()
    {
        _EstadoAtual = EEstados.Perseguindo;
        _Eixo = (_Jogador.transform.position - transform.position).normalized;
    }

    private void OnCollisionStay2D(Collision2D pColisao)
    {
        if (_Jogador.gameObject == pColisao.gameObject)
        {
            if ((_Jogador as ControleMiguel)._AnimacaoAtual != ControleMiguel.EAnimacao.Voando)
                Atacar();
        }
    }

    private void OnCollisionEnter2D(Collision2D pColisao)
    {
        if (_Jogador.gameObject != pColisao.gameObject)
        {
            TrocarDirecao();
        }
    }

    private void OnCollisionExit2D(Collision2D pColisao)
    {
        if (_Jogador.gameObject == pColisao.gameObject)
        {
            Perseguir();
        }
        else
        {
            Patrulhar();
        }
    }
}
