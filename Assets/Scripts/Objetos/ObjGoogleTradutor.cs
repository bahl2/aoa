using SimpleJSON;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    public enum EIdiomas
    {
        PortuguesBR,
        InglesUS,
        Espanhol,
        Hindi,
        Arabe,
        PortuguesPT,
        Bengali,
        Russo,
        Japones,
        Alemao,
        Coreano,
        Frances,
        Italiano,
        Tailandes,
        Chines,
        Turco,
        Vietnamita,
        Sueco
    }
    [Serializable]
    public class GoogleTradutor
    {
        public static string[] _Siglas = { "pt-BR", "en", "es", "hi", "ar", "pt-PT", "bn", "ru", "ja", "de", "ko", "fr", "it",
            "th", "zh-CN", "tr", "vi", "sv" };
        public string _Resposta;
        private string _IdiomaOrigem;
        private string _IdiomaDestino;
        private string _Texto;

        public GoogleTradutor(string pSiglaDestino, string pTexto, string pSiglaOrigem = "pt-BR")
        {
            _Resposta = string.Empty;
            _IdiomaOrigem = pSiglaOrigem;
            _IdiomaDestino = pSiglaDestino;
            _Texto = pTexto;
        }

        public IEnumerator Traduzir()
        {
            if (_IdiomaOrigem == _IdiomaDestino)
            {
                _Resposta = _Texto;
            }
            else
            {
                string lUrl = string.Format("{0}{1}&tl={2}&dt=t&q={3}", GameTags.ServerHostGoogleTradutor(), _IdiomaOrigem, _IdiomaDestino,
                    WWW.EscapeURL(_Texto));
                WWW lWWW = new WWW(lUrl);
                yield return lWWW;
                if (lWWW.isDone)
                {
                    if (string.IsNullOrEmpty(lWWW.error))
                    {
                        JSONNode lResposta = JSONNode.Parse(lWWW.text);
                        _Resposta = lResposta[0][0][0];
                    }
                    else
                    {
                        Debug.Log(lWWW.error);
                    }
                }
            }
        }
    }
}
