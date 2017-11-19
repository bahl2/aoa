using ArcadePUCCampinas;
using UnityEngine;

namespace Assets.Scripts.Global
{
    public class Controles : MonoBehaviour
    {
        public enum EJogador
        {
            Jogador1,
            Jogador2
        }

        public static bool Combo1(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Apertou((int)pJogador, EControle.AZUL);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Apertou(0, EControle.AZUL);
                    case 1: return InputArcade.Apertou(1, EControle.AZUL);
                    case 2: return Input.GetButtonDown("Set2Combo1");
                    case 3: return Input.GetButtonDown("Set3Combo1");
                }
            }
        }


        public static bool Combo2(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Apertou((int)pJogador, EControle.AMARELO);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Apertou(0, EControle.AMARELO);
                    case 1: return InputArcade.Apertou(1, EControle.AMARELO);
                    case 2: return Input.GetButtonDown("Set2Combo2");
                    case 3: return Input.GetButtonDown("Set3Combo2");
                }
            }
        }

        public static bool Combo3(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Apertou((int)pJogador, EControle.BRANCO);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Apertou(0, EControle.BRANCO);
                    case 1: return InputArcade.Apertou(1, EControle.BRANCO);
                    case 2: return Input.GetButtonDown("Set2Combo3");
                    case 3: return Input.GetButtonDown("Set3Combo3");
                }
            }
        }

        public static float EixoX(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Eixo((int)pJogador, EEixo.HORIZONTAL);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Eixo(0, EEixo.HORIZONTAL);
                    case 1: return InputArcade.Eixo(1, EEixo.HORIZONTAL);
                }
            }
        }

        public static float EixoY(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Eixo((int)pJogador, EEixo.VERTICAL);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Eixo(0, EEixo.VERTICAL);
                    case 1: return InputArcade.Eixo(1, EEixo.VERTICAL);
                }
            }
        }

        public static bool Habilidade(EJogador pJogador)
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                return InputArcade.Apertou((int)pJogador, EControle.VERDE);
            }
            else
            {
                switch (CFG.Controles)
                {
                    default: return InputArcade.Apertou(0, EControle.VERDE);
                    case 1: return InputArcade.Apertou(1, EControle.VERDE);
                    case 2: return Input.GetButtonDown("Set2Habilidade");
                    case 3: return Input.GetButtonDown("Set3Habilidade");
                }
            }
        }

        public static bool Mapa(EJogador pJogador)
        {
            return InputArcade.Apertou((int)pJogador, EControle.PRETO);
        }

        public static bool Missoes(EJogador pJogador)
        {
            return InputArcade.Apertou((int)pJogador, EControle.VERMELHO);
        }
    }
}
