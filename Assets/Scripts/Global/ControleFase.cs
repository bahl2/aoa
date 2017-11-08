using Assets.Scripts.Inimigos;
using Assets.Scripts.Jogadores;
using UnityEngine;

namespace Assets.Scripts.Global
{
    public class ControleFase : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _BotoesArcade;
        [SerializeField]
        private GameObject[] _BotoesPC;
        private Inimigo[] _Inimigos;
        public Jogador[] _Jogadores;
        public GameObject _Legenda;

        private void Start()
        {
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
        }

        private void Update()
        {
            _Inimigos = FindObjectsOfType<Inimigo>();
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
        }
    }
}
