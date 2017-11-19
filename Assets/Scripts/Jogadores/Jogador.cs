using ArcadePUCCampinas;
using Assets.Scripts.Global;
using Assets.Scripts.UI;
using EZCameraShake;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Jogadores
{
    public class Jogador : Personagem
    {
        [SerializeField]
        internal GameObject _MenuPause;
        [SerializeField]
        internal GameObject _MapaMundi;
        [SerializeField]
        internal GameObject _Missoes;
        [SerializeField]
        internal Controles.EJogador _Jogador;
        [SerializeField]
        internal float _MagShake;
        [SerializeField]
        internal float _RougShake;
        [SerializeField]
        internal float _FadeinShake;
        [SerializeField]
        internal float _FadeOutShake;
        internal bool _BotaoCombo1;
        internal bool _BotaoCombo2;
        internal bool _BotaoCombo3;
        internal CameraShakeInstance _CameraShake;
        public BarraProgresso _BarraMana;
        public BarraProgresso _BarraSpecial;


        internal override void Start()
        {
            base.Start();
            _MapaMundi.SetActive(false);
            _Missoes.SetActive(false);
        }

        internal override void Update()
        {
            base.Update();
            if (Ativo)
            {
                if (!ArcadeJogo._noMenu && CFG._Plataforma == CFG.EPlataforma.Arcade)
                {
                    if (InputArcade.Apertou(0, EControle.MENU))
                    {
                        ArcadeJogo.MostrarMenu();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        _MenuPause.SetActive(true);
                    }
                }
                if (Controles.Mapa(_Jogador))
                {
                    _MapaMundi.SetActive(true);
                }
                else if (Controles.Missoes(_Jogador))
                {
                    _Missoes.SetActive(true);
                }
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
                            _BarraSpecial.Add(-50);
                        }
                        break;
                    }
            }
        }

        internal virtual void Para()
        {
            _Acao = EAcoes.Parado;
            _VelocidadeAtual = 0;
            if (_CameraShake != null)
            {
                _CameraShake.DeleteOnInactive = true;
                _CameraShake.StartFadeOut(_FadeOutShake);
                _CameraShake = null;
            }
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
                if (_CameraShake == null)
                {
                    _CameraShake = CameraShaker.Instance.StartShake(_MagShake, _RougShake, _FadeinShake);
                    _CameraShake.DeleteOnInactive = false;
                }
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

        public virtual void Reinicia()
        {
            Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
        }
    }
}
