using ArcadePUCCampinas;
using Assets.Scripts.Global;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.UI
{
    public class MenuPrincipal : MenuBase
    {
        [SerializeField]
        private ComponenteBase _BotaoHistoria;
        [SerializeField]
        private ComponenteBase _BotaoDesafio;
        [SerializeField]
        private ComponenteBase _BotaoOpcoes;
        [SerializeField]
        private ComponenteBase _BotaoSair;
        [SerializeField]
        private MenuOpcoes _MenuOpcoes;
        [SerializeField]
        private VideoClip _Video;

        private void Update()
        {
            if (_Ativo && Time.timeScale == 1)
            {
                if (CFG._Plataforma == CFG.EPlataforma.Arcade)
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
                    if (InputArcade.Apertou(0, EControle.VERDE) || Input.GetKeyDown(KeyCode.Return))
                    {
                        if (ComponenteBase.Focado(_Componentes) == _BotaoHistoria)
                        {
                            BotaoHistoria();
                        }
                        else if (ComponenteBase.Focado(_Componentes) == _BotaoDesafio)
                        {
                            BotaoDesafio();
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

        public void BotaoSair()
        {
            Application.Quit();
        }

        public void BotaoHistoria()
        {
            CFG.ModoJogo = CFG.EModosJogo.Historia;
            CutScene.Video = _Video;
            CutScene.ProximaCena = GameTags.ECenas.Fase1;
        }

        public void BotaoDesafio()
        {
            CFG.ModoJogo = CFG.EModosJogo.Desafio;
            Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.Fase1];
        }

        public void BotaoOpcoes()
        {
            Ativo = false;
            _MenuOpcoes.Ativo = true;
        }
    }
}
