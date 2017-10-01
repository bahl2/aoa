using ArcadePUCCampinas;

public class ControleMiguel : Jogadores
{
    private bool _BotaoVoo;

    internal override void Start()
    {
        base.Start();
        Para();
    }

    internal override void Update()
    {
        if (Ativo)
        {
            base.Update();
            _Eixo.y = InputArcade.Eixo(0, EEixo.VERTICAL);
            _Eixo.x = InputArcade.Eixo(0, EEixo.HORIZONTAL);
            _BotaoAtaque = InputArcade.Apertou(0, EControle.VERMELHO);
            _BotaoVoo = InputArcade.Apertou(0, EControle.VERDE);
        }
    }

    internal override void Animacao()
    {
        base.Animacao();
        switch (_Acao)
        {
            case EAcoes.LevatandoVoo:
                {
                    LevantaVoo();
                    break;
                }
            case EAcoes.Voando:
                {
                    if (_BotaoVoo || _BarraMana._ManaAtual <= 0)
                    {
                        Pousa();
                    }
                    else
                    {
                        Voa();
                    }
                    break;
                }
            case EAcoes.Pousando:
                {
                    Pousa();
                    break;
                }
            case EAcoes.Andando:
            case EAcoes.Parado:
            case EAcoes.Correndo:
                {
                    if (_BotaoVoo && _BarraMana._ManaAtual > 0)
                        LevantaVoo();
                    break;
                }
        }
    }

    private void LevantaVoo()
    {
        _Acao = EAcoes.LevatandoVoo;
        _VelocidadeAtual = 0;
    }

    public void Pousa()
    {
        _Acao = EAcoes.Pousando;
        _VelocidadeAtual = 0;
    }

    public void Voa()
    {
        _Acao = EAcoes.Voando;
        _VelocidadeAtual = _Velocidade * 2;
    }
}
