using ArcadePUCCampinas;

public class ControleMiguel : Jogadores
{
    private bool _BotaoVoo;

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
            _BotaoCombo1 = InputArcade.Apertou(0, EControle.AZUL);
            _BotaoCombo2 = InputArcade.Apertou(0, EControle.AMARELO);
            _BotaoCombo3 = InputArcade.Apertou(0, EControle.BRANCO);
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
}
