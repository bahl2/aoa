using Assets.Scripts.Inimigos;
using Assets.Scripts.Jogadores;
using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;

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
        private CarregaRancking _Rancking;
        private Inimigo[] _Inimigos;
        public Jogador[] _Jogadores;
        public static GameObject _Legenda;
        public static GameObject _PoderPena;
        public static int _Caveiras;
        public static int _Dementadores;

        private void Start()
        {
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
                    Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
                }
            }
            else
            {
                Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
            }
            if (_Jogadores != null)
            {
                if (_Jogadores.Length <= 0)
                {
                    if (CFG.ModoJogo == CFG.EModosJogo.Desafio)
                    {
                        _Rancking.gameObject.SetActive(true);
                    }
                    else
                    {
                        Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
                    }
                }
            }
            else
            {
                Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
            }
        }

        private IEnumerator TraduzTextos()
        {

            GoogleTradutor lGoogleTradutor = new GoogleTradutor(GoogleTradutor._Siglas[(int)CFG.Idioma], _Rancking._Titulo.text);
            yield return lGoogleTradutor.Traduzir();
            _Rancking._Titulo.text = lGoogleTradutor._Resposta;
            lGoogleTradutor = new GoogleTradutor(GoogleTradutor._Siglas[(int)CFG.Idioma], _Rancking._Sair.text);
            yield return lGoogleTradutor.Traduzir();
            _Rancking._Sair.text = lGoogleTradutor._Resposta;
            _Rancking.gameObject.SetActive(false);
        }
    }
}
