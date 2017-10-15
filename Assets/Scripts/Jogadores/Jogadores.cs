using System.Collections;
using UnityEngine;

public class Jogadores : Personagens
{
    public BarraProgresso _BarraMana;
    public BarraProgresso _BarraSpecial;
    internal bool _BotaoCombo1;
    internal bool _BotaoCombo2;
    internal bool _BotaoCombo3;

    public virtual void Reinicia()
    {
        Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
    }

    internal override void Update()
    {
        if (Ativo)
        {
            base.Update();
            Animacao();
        }
        else Para();
    }

    internal virtual void Animacao()
    {
        switch (_Acao)
        {
            case EAcoes.Combo1:
                {
                    Combo1();
                    break;
                }
            case EAcoes.Combo2:
                {
                    Combo2();
                    break;
                }
            case EAcoes.Combo3:
                {
                    Combo3();
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
                    if (_BotaoCombo1)
                        Combo1();
                    else if (_BotaoCombo2)
                        Combo2();
                    else if (_BotaoCombo3)
                    {
                        Combo3();
                        _BarraSpecial.Add(-35);
                    }
                    break;
                }
        }
    }

    internal virtual void Para()
    {
        _Acao = EAcoes.Parado;
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

    internal virtual void Combo1()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Combo1;
    }

    internal virtual void Combo2()
    {
        _VelocidadeAtual = _Velocidade;
        _Acao = EAcoes.Combo2;
    }

    internal virtual void Combo3()
    {
        if (_BarraSpecial._Atual > 0)
        {
            _VelocidadeAtual = _Velocidade;
            _Acao = EAcoes.Combo3;
        }
    }

    internal virtual IEnumerator Mana()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            _BarraMana.Add(-10);
        }
    }
}
