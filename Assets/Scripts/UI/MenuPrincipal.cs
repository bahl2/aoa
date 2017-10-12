﻿using ArcadePUCCampinas;
using UnityEngine;
using UnityEngine.Video;

public class MenuPrincipal : MenuBase
{
    [SerializeField]
    private ComponenteBase _BotaoJogar;
    [SerializeField]
    private ComponenteBase _BotaoOpcoes;
    [SerializeField]
    private ComponenteBase _BotaoSair;
    [SerializeField]
    private MenuOpcoes _MenuOpcoes;
    [SerializeField]
    private VideoClip _Video;

    public void BotaoSair()
    {
        Application.Quit();
    }

    public void BotaoJogar()
    {
        CutScene.Video = _Video;
        CutScene.ProximaCena = GameTags.ECenas.Fase1;
    }

    public void BotaoOpcoes()
    {
        Ativo = false;
        _MenuOpcoes.Ativo = true;
    }

    private void Update()
    {
        if (_Ativo)
        {
            if (InputArcade.Apertou(0, EControle.PRETO))
            {
                BotaoSair();
            }
            if (InputArcade.Apertou(0, EControle.BAIXO))
            {
                ComponenteBase.Focar(_Componentes, 1);
            }
            if (InputArcade.Apertou(0, EControle.CIMA))
            {
                ComponenteBase.Focar(_Componentes, -1);
            }
            if (InputArcade.Apertou(0, EControle.VERDE))
            {
                if (ComponenteBase.Focado(_Componentes) == _BotaoJogar)
                {
                    BotaoJogar();
                }
                else if (ComponenteBase.Focado(_Componentes) == _BotaoOpcoes)
                {
                    BotaoOpcoes();
                }
                else if (ComponenteBase.Focado(_Componentes) == _BotaoSair)
                {
                    BotaoSair();
                }
            }
        }
    }
}
