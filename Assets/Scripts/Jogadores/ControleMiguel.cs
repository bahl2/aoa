using Assets.Scripts.Audio;
using Assets.Scripts.Global;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Jogadores
{
    public class ControleMiguel : Jogador
    {
        private bool _BotaoVoo;

        private void LevantaVoo()
        {
            _Acao = EAcoes.LevatandoVoo;
            _VelocidadeAtual = 0;
        }

        private IEnumerator DecMana()
        {
            while (true)
            {
                if (_Acao == EAcoes.Voando)
                {
                    yield return new WaitForSeconds(3);
                    _BarraMana.Add(-10);
                }
                yield return new WaitForSeconds(2);
            }
        }

        internal override void Start()
        {
            base.Start();
            StartCoroutine(DecMana());
            Para();
        }

        internal override void Update()
        {
            base.Update();
            if (Ativo)
            {
                _Eixo.y = Controles.EixoY(_Jogador);
                _Eixo.x = Controles.EixoX(_Jogador);
                _BotaoCombo1 = Controles.Combo1(_Jogador);
                _BotaoCombo2 = Controles.Combo2(_Jogador);
                _BotaoCombo3 = Controles.Combo3(_Jogador);
                _BotaoVoo = Controles.Habilidade(_Jogador);
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
                        if (_BotaoVoo || _BarraMana._Atual <= 0)
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
                        if (_BotaoVoo && _BarraMana._Atual > 0)
                            LevantaVoo();
                        break;
                    }
            }
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

        public void Passo()
        {
            _ControleSons.clip = ControleAudio._Passo;
            _ControleSons.Play();
        }
    }
}
