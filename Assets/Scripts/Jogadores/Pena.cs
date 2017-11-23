using Assets.Scripts.Global;
using UnityEngine;

namespace Assets.Scripts.Jogadores
{
    public class Pena : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (lJogador != null)
            {
                ControleFase._Legenda.SetActive(true);
                lJogador._BarraMana.Add(lJogador._BarraMana._Maximo);
                ControleFase._PoderPena.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
