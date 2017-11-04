using ArcadePUCCampinas;
using Assets.Scripts.Global;
using Assets.Scripts.Objetos;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MenuOpcoes : MenuBase
    {
        [SerializeField]
        private ComponenteSlider _Volume;
        [SerializeField]
        private ComponenteCombo _Idioma;
        [SerializeField]
        private ComponenteCombo _Legenda;
        [SerializeField]
        private ComponenteBase _BotaoVoltar;
        private string _IdiomaSelecionado;
        private string _LegendaSelecionada;
        private CFG _CFG;

        private void Awake()
        {
            _CFG = FindObjectOfType<CFG>();
        }

        private void Update()
        {
            if (_Ativo && Time.timeScale == 1)
            {
                if (InputArcade.Apertou(0, EControle.PRETO))
                {
                    BotaoVoltarMenu();
                }
                if (InputArcade.Apertou(0, EControle.BAIXO))
                {
                    ComponenteBase.Focar(_Componentes, 1);
                }
                if (InputArcade.Apertou(0, EControle.CIMA))
                {
                    ComponenteBase.Focar(_Componentes, -1);
                }
                if (ComponenteBase.Focado(_Componentes) == _Volume)
                {
                    if (InputArcade.Apertado(0, EControle.DIREITA))
                    {
                        _CFG.Volume = _Volume.Value += 0.1f;
                    }
                    else if (InputArcade.Apertado(0, EControle.ESQUERDA))
                    {
                        _CFG.Volume = _Volume.Value -= 0.1f;
                    }
                }
                else if (ComponenteBase.Focado(_Componentes) == _Idioma)
                {
                    if (InputArcade.Apertou(0, EControle.DIREITA))
                    {
                        _IdiomaSelecionado = _Idioma.Focar(1).Value;
                    }
                    else if (InputArcade.Apertou(0, EControle.ESQUERDA))
                    {
                        _IdiomaSelecionado = _Idioma.Focar(-1).Value;
                    }
                }
                else if (ComponenteBase.Focado(_Componentes) == _Legenda)
                {
                    if (InputArcade.Apertou(0, EControle.DIREITA))
                    {
                        _LegendaSelecionada = _Legenda.Focar(1).Value;
                    }
                    else if (InputArcade.Apertou(0, EControle.ESQUERDA))
                    {
                        _LegendaSelecionada = _Legenda.Focar(-1).Value;
                    }
                }
                if (InputArcade.Apertou(0, EControle.VERDE) || Input.GetKeyDown(KeyCode.Return))
                {
                    if (ComponenteBase.Focado(_Componentes) == _BotaoVoltar)
                    {
                        BotaoVoltarMenu();
                    }
                    else
                    {
                        ComponenteBase.Focar(_Componentes, 1);
                    }
                }
            }
        }

        internal override IEnumerator Ativa(bool pValor)
        {
            _IdiomaSelecionado = GoogleTradutor._Siglas[(int)_CFG.Idioma];
            _LegendaSelecionada = GoogleTradutor._Siglas[(int)_CFG.Legenda];
            _Idioma.Value = _IdiomaSelecionado;
            _Legenda.Value = _LegendaSelecionada;
            _Volume.Value = _CFG.Volume;
            return base.Ativa(pValor);
        }

        public void BotaoVoltarMenu()
        {
            _CFG.Idioma = (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, _IdiomaSelecionado));
            _CFG.Legenda = (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, _LegendaSelecionada));
        }
    }
}
