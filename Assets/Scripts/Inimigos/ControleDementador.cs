using UnityEngine;

public class ControleDementador : Inimigo
{
    [SerializeField]
    private float _Velocidade;
    [SerializeField]
    private float _DistanciaMin;
    [SerializeField]
    private float _TempoPatrulhar;
    [SerializeField]
    private float _TempoAtaque;
    [SerializeField]
    private Transform _PrefabPoder;
    [SerializeField]
    private Transform _Mao;
    private float _VelocidadeAtual;
    private float _TempoPatrulhando;
    private Jogador _Jogador;
    private Rigidbody2D _Controle;
    private float _TempoAtacando;
    private EEstados _EstadoAtual;

    private enum EEstados
    {
        Patrulhando,
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
                Atacar();
            }
            else
            {
                Patrulhar();
            }
        }
        else if (_EstadoAtual == EEstados.Atacando)
        {
            _VelocidadeAtual = _Velocidade;
            if (JogadorPerto())
            {
                Atacar();
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
    private bool VendoJogador()
    {
        bool lVendo = false;
        RaycastHit2D[] lHits = Physics2D.RaycastAll(transform.position, _Eixo, _DistanciaMin);
        Debug.DrawRay(transform.position, _Eixo, Color.red, _DistanciaMin);
        foreach (RaycastHit2D lHit in lHits)
        {
            Debug.Log(lHit.transform.name);
            if (_Jogador.transform == lHit.transform)
                lVendo = true;
        }
        return lVendo;
    }

    private void Atacar()
    {
        _EstadoAtual = EEstados.Atacando;
        if (_TempoAtacando > _TempoAtaque && VendoJogador())
        {
            _TempoAtacando = 0;
            _VelocidadeAtual = 0;
            Transform lPoderTransform = Instantiate(_PrefabPoder, _Mao.position, _Mao.rotation);
            Poder lPoder = lPoderTransform.GetComponent<Poder>();
            lPoder._Controle.velocity = lPoder._Velocidade * _Eixo;
        }
    }

    private void TrocarDirecao()
    {
        _Eixo = _Eixo * -1;
    }

    private void OnCollisionStay2D(Collision2D pColisao)
    {
        if (_Jogador.gameObject == pColisao.gameObject)
        {
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
            Atacar();
        }
        else
        {
            Patrulhar();
        }
    }
}
