public class ControleCaveira : Inimigos
{
    internal override void MovimentoIA()
    {
        base.MovimentoIA();
        switch (_Acao)
        {
            case EAcoes.Atacando:
                {
                    if (_Bateu == _Jogador.transform)
                    {
                        Ataca();
                    }
                    else if (JogadorPerto())
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
                    if (_Bateu == _Jogador.transform)
                    {
                        Ataca();
                    }
                    break;
                }
        }
    }

    private void Ataca()
    {
        _Acao = EAcoes.Atacando;
        _VelocidadeAtual = 0;
        if (_TempoAtacando > _TempoAtaque)
        {
            _TempoAtacando = 0;
            _Jogador._BarraVida.Dano = 10;
        }
    }

    internal override void Persegue()
    {
        base.Persegue();
        _Eixo = (_Jogador.transform.position - transform.position).normalized;
    }
}
