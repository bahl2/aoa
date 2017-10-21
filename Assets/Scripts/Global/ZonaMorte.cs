using UnityEngine;

namespace Assets.Scripts.Global
{
    public class ZonaMorte : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            Personagem lPersonagem = pColisao.GetComponent<Personagem>();
            if (lPersonagem != null)
            {
                if (lPersonagem._Acao != Personagem.EAcoes.Voando)
                    lPersonagem._BarraVida.Add(-lPersonagem._BarraVida._Atual);
            }
        }
    }
}
