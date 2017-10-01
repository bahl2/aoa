using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMorte : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        Jogadores lJogador = pColisao.GetComponent<Jogadores>();
        if (lJogador != null)
        {
            if ((lJogador as ControleMiguel)._Acao != ControleMiguel.EAcoes.Voando)
                lJogador._BarraVida.AddVida(0);
        }
    }
}
