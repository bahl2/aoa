using ArcadePUCCampinas;
using UnityEngine;

public class ControleMiguel : Jogador
{
    [SerializeField]
    private float _Velocidade;
    [SerializeField]
    private float _TempoEscolhaCombo;    
    private float _TempoCombo;
    private Animator _Animacao;
    private Vector2 _Parado;
    private bool _BotaoAtaque;
    private bool _BotaoVoo;
    private int _Ataque;
    public EAnimacao _AnimacaoAtual;

    public enum EAnimacao
    {
        Parado,
        Andando,
        Correndo,
        LevatandoVoo,
        Voando,
        Pousando,
        Ataque1,
        Ataque2,
        Ataque3,
        Ataque,
        Morrendo
    }

    internal override void Start()
    {
        base.Start();
        _Animacao = GetComponent<Animator>();
        _VelocidadeAtual = _Velocidade;
        Para();
    }

    internal override void Update()
    {
        if (Ativo)
        {
            base.Update();
            _TempoCombo += Time.deltaTime;
            _Eixo.y = InputArcade.Eixo(0, EEixo.VERTICAL);
            _Eixo.x = InputArcade.Eixo(0, EEixo.HORIZONTAL);
            _BotaoAtaque = InputArcade.Apertou(0, EControle.VERMELHO);
            _BotaoVoo = InputArcade.Apertou(0, EControle.VERDE);
        }
        else Para();
            Animacao();
        
    }

    private void Animacao()
    {        
        switch (_AnimacaoAtual)
        {
            case EAnimacao.LevatandoVoo:
                {
                    LevantaVoo();
                    break;
                }
            case EAnimacao.Voando:
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
            case EAnimacao.Pousando:
                {
                    Pousa();
                    break;
                }
            case EAnimacao.Ataque:
                {
                    if (_TempoCombo <= _TempoEscolhaCombo)
                    {
                        if (_BotaoAtaque && _Ataque < 3)
                            Ataque();
                    }
                    else
                    {
                        _TempoCombo = 0;
                        switch (_Ataque)
                        {
                            case 2:
                                Ataque2();
                                break;
                            case 3:
                                Ataque3();
                                break;
                            default:
                                Ataque1();
                                break;
                        }
                    }
                    break;
                }
            case EAnimacao.Ataque1:
                {
                    Ataque1();
                    break;
                }
            case EAnimacao.Ataque2:
                {
                    Ataque2();
                    break;
                }
            case EAnimacao.Ataque3:
                {
                    Ataque3();
                    break;
                }
            case EAnimacao.Morrendo:
                {
                    Morre();
                    break;
                }
            default:
                {
                    if (_Eixo == Vector2.zero)
                    {
                        Para();
                    }
                    else if (_Eixo.x > 0 || _Eixo.y > 0)
                    {
                        if (_Eixo.y > 0)
                        {
                            if (_Eixo.y <= 0.3f)
                            {
                                Anda();
                            }
                            else
                            {
                                Corre();
                            }
                        }
                        else
                        {
                            if (_Eixo.x <= 0.3f)
                            {
                                Anda();
                            }
                            else
                            {
                                Corre();
                            }
                        }
                    }
                    else if (_Eixo.x < 0 || _Eixo.y < 0)
                    {
                        if (_Eixo.y < 0)
                        {
                            if (_Eixo.y >= -0.3f)
                            {
                                Anda();
                            }
                            else
                            {
                                Corre();
                            }
                        }
                        else
                        {
                            if (_Eixo.x >= -0.3f)
                            {
                                Anda();
                            }
                            else
                            {
                                Corre();
                            }
                        }
                    }
                    if (_BotaoAtaque)
                        Ataque();
                    if (_BotaoVoo && _BarraMana._ManaAtual > 0)
                        LevantaVoo();

                    break;
                }
        }
        if (_BarraVida._VidaAtual <= 0)
        {
            Morre();
        }
        _Animacao.SetInteger("Animacao", (int)_AnimacaoAtual);
    }

    public void Para()
    {
        _Ataque = 0;
        _TempoCombo = 0;
        _AnimacaoAtual = EAnimacao.Parado;
        _VelocidadeAtual = 0;
    }

    private void Anda()
    {
        _AnimacaoAtual = EAnimacao.Andando;
        _VelocidadeAtual = _Velocidade;
    }

    private void Corre()
    {
        _AnimacaoAtual = EAnimacao.Correndo;
        _VelocidadeAtual = _Velocidade * 2;
    }

    private void LevantaVoo()
    {
        _AnimacaoAtual = EAnimacao.LevatandoVoo;
        _VelocidadeAtual = 0;
    }

    public void Pousa()
    {
        _AnimacaoAtual = EAnimacao.Pousando;
        _VelocidadeAtual = 0;
    }

    public void Voa()
    {
        _AnimacaoAtual = EAnimacao.Voando;
        _VelocidadeAtual = _Velocidade * 2;      
    }

    private void Ataque1()
    {
        _AnimacaoAtual = EAnimacao.Ataque1;
        _VelocidadeAtual = 0;
    }

    private void Ataque2()
    {
        _AnimacaoAtual = EAnimacao.Ataque2;
        _VelocidadeAtual = 0;
    }

    private void Ataque3()
    {
        _AnimacaoAtual = EAnimacao.Ataque3;
        _VelocidadeAtual = 0;
    }    

    private void Ataque()
    {
        _AnimacaoAtual = EAnimacao.Ataque;
        _Ataque++;
    }

    public void Morre()
    {
        _AnimacaoAtual = EAnimacao.Morrendo;
        _VelocidadeAtual = 0;
    }
}
