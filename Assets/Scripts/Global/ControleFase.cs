using Assets.Scripts.Inimigos;
using Assets.Scripts.Jogadores;
using Assets.Scripts.Objetos;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Global
{
    public class ControleFase : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _BotoesArcade;
        [SerializeField]
        private GameObject[] _BotoesPC;
        [SerializeField]
        private GameObject _TimeTrial;
        [SerializeField]
        private CarregaRanking _CarregaRanking;
        [SerializeField]
        private GameObject _MenuPause;
        [SerializeField]
        private Text[] _TextosBotoes;
        [SerializeField]
        private Slider _Progresso;
        [SerializeField]
        private GameObject _Carregando;
        [SerializeField]
        private GameObject _Save;
        private Inimigo[] _Inimigos;
        public Jogador[] _Jogadores;
        public static bool _FimJogo;
        public static GameObject _Legenda;
        public static GameObject _PoderPena;
        public static int _Caveiras;
        public static int _Dementadores;

        private void Start()
        {
            _FimJogo = false;
            _Legenda = GameObject.FindWithTag("Legenda");
            _Legenda.SetActive(false);
            foreach (GameObject lBotaoArcade in _BotoesArcade)
            {
                lBotaoArcade.SetActive(CFG._Plataforma == CFG.EPlataforma.Arcade);
            }
            foreach (GameObject lBotaoArcade in _BotoesPC)
            {
                lBotaoArcade.SetActive(CFG._Plataforma == CFG.EPlataforma.PC);
            }
            _Jogadores = FindObjectsOfType<Jogador>();
            foreach (Jogador lJogador in _Jogadores)
            {
                lJogador._Direcao = Personagem.EDirecao.Esquerda;
            }
            _PoderPena = GameObject.FindWithTag("PoderPena");
            _PoderPena.SetActive(false);
            _TimeTrial.SetActive(CFG.ModoJogo == CFG.EModosJogo.Desafio);
            _Caveiras = FindObjectsOfType<ControleCaveira>().Length;
            _Dementadores = FindObjectsOfType<ControleDementador>().Length;
            StartCoroutine(TraduzTextos());
        }

        private void Update()
        {
            _Inimigos = FindObjectsOfType<Inimigo>();
            _Jogadores = FindObjectsOfType<Jogador>();
            if (_Inimigos != null)
            {
                if (_Inimigos.Length <= 0)
                {
                    if (CFG.ModoJogo == CFG.EModosJogo.Desafio)
                    {
                        _FimJogo = true;
                        _CarregaRanking.gameObject.SetActive(true);
                    }
                    else
                    {
                        _FimJogo = true;
                        Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
                    }
                }
            }
            else
            {
                _FimJogo = true;
                Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
            }

            if (_Jogadores != null)
            {
                if (_Jogadores.Length <= 0)
                {
                    if (CFG.ModoJogo == CFG.EModosJogo.Desafio)
                    {
                        _FimJogo = true;
                        _CarregaRanking.gameObject.SetActive(true);
                    }
                    else
                    {
                        _FimJogo = true;
                        Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
                    }
                }
            }
            else
            {
                _FimJogo = true;
                Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
            }
        }

        private IEnumerator TraduzTextos()
        {
            for (int i = 0; i < _TextosBotoes.Length; i++)
            {
                GoogleTradutor lGoogleTradutor = new GoogleTradutor(GoogleTradutor._Siglas[(int)CFG.Idioma], _TextosBotoes[i].text);
                yield return lGoogleTradutor.Traduzir();
                _TextosBotoes[i].text = lGoogleTradutor._Resposta;
                _Progresso.value = _TextosBotoes.Length / (i + 1);
            }
            Destroy(_Carregando);
            _MenuPause.SetActive(false);
            _CarregaRanking.gameObject.SetActive(false);
        }
    }
}
