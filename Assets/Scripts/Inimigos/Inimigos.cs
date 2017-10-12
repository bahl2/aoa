using UnityEngine;

public class Inimigos : Personagens
{
    [SerializeField]
    internal float _DistanciaMin;
    [SerializeField]
    internal float _TempoPatrulhar;
    [SerializeField]
    internal float _TempoAtaque;
    internal float _TempoPatrulhando;
    internal Jogadores _Jogador;
    internal float _TempoAtacando;
    internal Transform _Bateu;

    internal override void Start()
    {
        base.Start();
        _Jogador = FindObjectOfType<Jogadores>();
        _Acao = EAcoes.Patrulhando;
        _Eixo = transform.TransformDirection(Vector2.right);
        _TempoPatrulhando = 0;
        _TempoAtacando = 0;
        _Bateu = null;
    }

    internal override void Update()
    {
        if (Ativo)
        {
            base.Update();
            Vector3 lScala = transform.localScale;
            lScala.x = (int)_Direcao;
            _BarraVida.transform.localScale = lScala;
            MovimentoIA();
        }
        else _VelocidadeAtual = 0;
    }

    internal virtual void MovimentoIA()
    {
        switch (_Acao)
        {
            case EAcoes.Patrulhando:
                {
                    if (JogadorPerto())
                    {
                        Persegue();
                    }
                    else
                    {
                        Patrulha();
                    }
                    break;
                }
            case EAcoes.Perseguindo:
                {
                    if (JogadorPerto() && _Bateu == null)
                    {
                        Persegue();
                    }
                    else if (!JogadorPerto() && _Bateu == null)
                    {
                        Patrulha();
                    }
                    break;
                }
        }
        _TempoAtacando += Time.deltaTime;
        _TempoPatrulhando += Time.deltaTime;
    }

    internal virtual bool JogadorPerto()
    {
        Vector3 lDistancia = transform.position - _Jogador.transform.position;
        return lDistancia.magnitude <= _DistanciaMin;
    }

    internal virtual void Patrulha()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Patrulhando;
        if (_TempoPatrulhando > _TempoPatrulhar)
        {
            TrocarDirecao();
            _TempoPatrulhando = 0;
        }
        if (_Bateu != _Jogador.transform && _Bateu != null)
        {
            TrocarDirecao();
        }
    }

    internal virtual void TrocarDirecao()
    {
        _Eixo = _Eixo * -1;
    }

    internal virtual void Persegue()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Perseguindo;
    }

    private void OnCollisionEnter2D(Collision2D pColisao)
    {
        _Bateu = pColisao.transform;
    }

    private void OnCollisionExit2D(Collision2D pColisao)
    {
        _Bateu = null;
    }
}
