﻿using ArcadePUCCampinas;
using Assets.Scripts.Objetos;
using System;
using System.Collections;
using UnityEngine;

public class MenuOpcoes : MonoBehaviour
{
    [SerializeField]
    private ComponenteSlider _Som;
    [SerializeField]
    private ComponenteCombo _Idioma;
    [SerializeField]
    private ComponenteCombo _Legenda;
    [SerializeField]
    private ComponenteBase _BotaoVoltar;
    private string _IdiomaSelecionado;
    private string _LegendaSelecionada;
    [SerializeField]
    private ComponenteBase[] _Componentes;
    private CFG _CFG;
    private bool _Ativo;

    public bool Ativo
    {
        get
        {
            return _Ativo;
        }
        set
        {
            StartCoroutine(Ativa(value));
            if (value)
            {
                ComponenteBase.Focar(_Componentes, 0);
                transform.parent.GetComponent<Canvas>().sortingOrder = 1;
            }
            else transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        }
    }

    private void Start()
    {
        _CFG = FindObjectOfType<CFG>();
        _IdiomaSelecionado = GoogleTradutor._Siglas[(int)_CFG.Idioma];
        _LegendaSelecionada = GoogleTradutor._Siglas[(int)_CFG.Legenda];
        _Idioma.Value = _IdiomaSelecionado;
        _Legenda.Value = _LegendaSelecionada;
        _Som.Value = _CFG.Som;
    }

    private void Update()
    {
        if (_Ativo)
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
            if (ComponenteBase.Focado(_Componentes) == _Som)
            {
                if (InputArcade.Apertado(0, EControle.DIREITA))
                {
                    _CFG.Som = _Som.Value += 0.1f;
                }
                else if (InputArcade.Apertado(0, EControle.ESQUERDA))
                {
                    _CFG.Som = _Som.Value -= 0.1f;
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
            if (InputArcade.Apertou(0, EControle.VERDE))
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

    private IEnumerator Ativa(bool pValor)
    {
        yield return new WaitForSeconds(0.5f);
        _Ativo = pValor;
    }

    public void BotaoVoltarMenu()
    {
        _CFG.Idioma = (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, _IdiomaSelecionado));
        _CFG.Legenda = (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, _LegendaSelecionada));
    }
}