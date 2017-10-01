using System.Collections;
using UnityEngine;

public class Jogadores : Personagens
{
    [SerializeField]
    internal float _TempoEscolhaCombo;
    internal bool _Ativo;
    internal float _TempoCombo;
    internal int _Ataque;
    internal bool _BotaoAtaque;
    public BarraMana _BarraMana;

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

    internal override void Start()
    {
        base.Start();
        _Ativo = true;
    }

    internal override void Update()
    {
        if (Ativo)
        {
            base.Update();
            _TempoCombo += Time.deltaTime;
            Animacao();
        }
        else Para();
    }

    internal override void FixedUpdate()
    {
        if (Ativo)
            base.FixedUpdate();
    }

    internal virtual void Animacao()
    {
        switch (_Acao)
        {
            case EAcoes.Atacando:
                {
                    if (_TempoCombo <= _TempoEscolhaCombo)
                    {
                        if (_BotaoAtaque && _Ataque < 3)
                            Ataca();
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
            case EAcoes.Ataque1:
                {
                    Ataque1();
                    break;
                }
            case EAcoes.Ataque2:
                {
                    Ataque2();
                    break;
                }
            case EAcoes.Ataque3:
                {
                    Ataque3();
                    break;
                }
            case EAcoes.Morrendo:
                {
                    Morre();
                    break;
                }
            case EAcoes.Andando:
            case EAcoes.Parado:
            case EAcoes.Correndo:
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
                    {
                        Ataca();
                    }
                    break;
                }
        }
    }

    internal virtual IEnumerator Ativa(bool pValor)
    {
        yield return new WaitForSeconds(0.5f);
        _Ativo = pValor;
    }

    internal virtual void Para()
    {
        _Acao = EAcoes.Parado;
        _Ataque = 0;
        _TempoCombo = 0;
        _VelocidadeAtual = 0;
    }

    internal virtual void Anda()
    {
        _Acao = EAcoes.Andando;
        _VelocidadeAtual = _Velocidade;
    }

    internal virtual void Corre()
    {
        _Acao = EAcoes.Correndo;
        _VelocidadeAtual = _Velocidade * 2;
    }

    internal virtual void Ataque1()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Ataque1;
    }

    internal virtual void Ataque2()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Ataque2;
    }

    internal virtual void Ataque3()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Ataque3;
    }

    internal virtual void Ataca()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Atacando;
        _Ataque++;
    }

    public virtual void Reinicia()
    {
        Carrega.Cena = GameTags.Cenas[(int)GameTags.ECenas.MenuPrincipal];
    }
}
