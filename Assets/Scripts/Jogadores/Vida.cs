using UnityEngine;

namespace Assets.Scripts.Jogadores
{
    public class Vida : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (lJogador != null)
            {
                lJogador._BarraVida.Add(lJogador._BarraVida._Maximo);
                Destroy(gameObject);
            }
        }
    }
}
