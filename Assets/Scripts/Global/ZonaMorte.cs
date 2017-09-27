using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMorte : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        Jogador lJogador = pColisao.GetComponent<Jogador>();
        if (lJogador != null)
        {
            if ((lJogador as ControleMiguel)._AnimacaoAtual != ControleMiguel.EAnimacao.Voando)
                lJogador._BarraVida.AddVida(0);
        }
    }
}
