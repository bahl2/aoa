﻿namespace Assets.Scripts.Global
{
    public static class GameTags
    {
        public enum ECenas
        {
            Carrega,
            Fase1,
            MenuPrincipal,
            CutScene
        }

        public static string[] _Cenas = { "Carrega", "Fase1", "Menu Principal", "CutScene" };

        public static string ServerHost()
        {
            return "http://104.236.78.90:3000/";
        }

        public static string ServerHostGoogleTradutor()
        {
            return "https://translate.googleapis.com/translate_a/single?client=gtx&sl=";
        }
    }
}
