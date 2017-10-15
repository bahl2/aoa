using UnityEngine;

public class ZonaMorte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        Jogadores lJogador = pColisao.GetComponent<Jogadores>();
        if (lJogador != null)
        {
            if (lJogador._Acao != Personagens.EAcoes.Voando)
                lJogador._BarraVida.Add(0);
        }
    }
}
