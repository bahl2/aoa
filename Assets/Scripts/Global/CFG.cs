using Assets.Scripts.Objetos;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Global
{
    public class CFG : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Carregando;
        [SerializeField]
        private MenuOpcoes _MenuOpcoes;
        [SerializeField]
        private Canvas _TelaCarregando;
        [SerializeField]
        private MenuPrincipal _MenuPrincipal;
        private Slider _Progresso;
        private Text[] _TextosMenuPrincipal;
        private Text[] _TextosMenuOpcoes;
        private List<string> _Textos;

        public EIdiomas Idioma
        {
            get
            {
                return (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, PlayerPrefs.GetString("Idioma")));
            }
            set
            {
                PlayerPrefs.SetString("Idioma", GoogleTradutor._Siglas[(int)value]);
                StartCoroutine(TraduzTextos());
            }
        }

        public EIdiomas Legenda
        {
            get
            {
                return (EIdiomas)Enum.ToObject(typeof(EIdiomas), Array.IndexOf(GoogleTradutor._Siglas, PlayerPrefs.GetString("Legenda")));
            }
            set
            {
                PlayerPrefs.SetString("Legenda", GoogleTradutor._Siglas[(int)value]);
            }
        }

        public float Volume
        {
            get
            {
                return PlayerPrefs.GetFloat("Volume");
            }
            set
            {
                PlayerPrefs.SetFloat("Volume", value);
                AudioListener.volume = value;
            }
        }

        private void Awake()
        {
            _Textos = new List<string>();
            AudioListener.volume = Volume;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _Progresso = _Carregando.GetComponentInChildren<Slider>();
            _TextosMenuPrincipal = ProcurarComponentesCena<Text>(_MenuPrincipal.transform);
            foreach (Text lTexto in _TextosMenuPrincipal)
            {
                _Textos.Add(lTexto.text);
            }
            _TextosMenuOpcoes = ProcurarComponentesCena<Text>(_MenuOpcoes.transform);
            foreach (Text lTexto in _TextosMenuOpcoes)
            {
                _Textos.Add(lTexto.text);
            }
            CFGInicial();
            StartCoroutine(TraduzTextos());
        }

        private IEnumerator TraduzTextos()
        {
            _TelaCarregando.sortingOrder = 1;
            _MenuPrincipal.Ativo = false;
            _MenuOpcoes.Ativo = false;
            int lTextoMenuPrincipal = 0;
            int lTextoMenuOpcao = 0;
            for (int i = 0; i < _Textos.Count; i++)
            {
                GoogleTradutor lGoogleTradutor = new GoogleTradutor(GoogleTradutor._Siglas[(int)Idioma], _Textos[i]);
                yield return lGoogleTradutor.Traduzir();
                if (lTextoMenuPrincipal < _TextosMenuPrincipal.Length)
                {
                    _TextosMenuPrincipal[lTextoMenuPrincipal].text = string.IsNullOrEmpty(lGoogleTradutor._Resposta) ?
                        _TextosMenuPrincipal[lTextoMenuPrincipal].text : lGoogleTradutor._Resposta;
                    lTextoMenuPrincipal++;
                }
                else if (lTextoMenuOpcao < _TextosMenuOpcoes.Length)
                {
                    _TextosMenuOpcoes[lTextoMenuOpcao].text = string.IsNullOrEmpty(lGoogleTradutor._Resposta) ?
                        _TextosMenuOpcoes[lTextoMenuOpcao].text : lGoogleTradutor._Resposta;
                    lTextoMenuOpcao++;
                }
                _Progresso.value = _Textos.Count / (i + 1);
            }
            _TelaCarregando.sortingOrder = 0;
            _MenuPrincipal.Ativo = true;
            _MenuOpcoes.Ativo = false;
        }

        private void CFGInicial()
        {
            if (!PlayerPrefs.HasKey("Idioma"))
                Idioma = EIdiomas.PortuguesBR;
            if (!PlayerPrefs.HasKey("Legenda"))
                Legenda = EIdiomas.PortuguesBR;
            if (!PlayerPrefs.HasKey("Volume"))
                Volume = 1;
        }

        public static TipoComponente[] ProcurarComponentesCena<TipoComponente>(Transform pPai)
        {
            List<TipoComponente> lComponetes = new List<TipoComponente>();
            for (int i = 0; i < pPai.transform.childCount; i++)
            {
                if (pPai.transform.GetChild(i).gameObject.activeSelf)
                {
                    TipoComponente lComponeteAtual = pPai.transform.GetChild(i).GetComponent<TipoComponente>();
                    if (lComponeteAtual != null)
                    {
                        lComponetes.Add(lComponeteAtual);
                    }
                    if (pPai.transform.GetChild(i).childCount > 0)
                    {
                        ProcurarComponentesFilhos(pPai.transform.GetChild(i), ref lComponetes);
                    }
                }
            }
            return lComponetes.ToArray();
        }

        public static void ProcurarComponentesFilhos<TipoComponente>(Transform pPai, ref List<TipoComponente> pComponetes)
        {
            for (int i = 0; i < pPai.transform.childCount; i++)
            {
                if (pPai.transform.GetChild(i).gameObject.activeSelf)
                {
                    TipoComponente lComponetes = pPai.transform.GetChild(i).GetComponent<TipoComponente>();
                    if (lComponetes != null)
                    {
                        pComponetes.Add(lComponetes);
                    }
                    if (pPai.transform.GetChild(i).childCount > 0)
                    {
                        ProcurarComponentesFilhos(pPai.transform.GetChild(i), ref pComponetes);
                    }
                }
            }
        }
    }
}
