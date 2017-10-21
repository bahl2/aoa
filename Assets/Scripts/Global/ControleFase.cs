using Assets.Scripts.Inimigos;
using Assets.Scripts.Jogadores;
using UnityEngine;

namespace Assets.Scripts.Global
{
    public class ControleFase : MonoBehaviour
    {
        private Inimigo[] _Inimigos;
        public Jogador[] _Jogadores;
        public GameObject _Legenda;

        private void Start()
        {
            _Legenda.SetActive(false);
            //Na fase 1 o jogador começa indo para esquerda
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
