using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class Legenda : MonoBehaviour {

	private void Update()
    {
        if(InputArcade.Apertou(0, EControle.VERDE))
        {            
            gameObject.SetActive(false);
        }            
    }

    private void OnEnable()
    {
        Jogador[] lJogadores = FindObjectsOfType<Jogador>();
        foreach (Jogador lJogador in lJogadores)
        {
            lJogador.Ativo = false;
        }
    }

    private void OnDisable()
    {
        Jogador[] lJogadores = FindObjectsOfType<Jogador>();
        foreach (Jogador lJogador in lJogadores)
        {
            lJogador.Ativo = true;
        }
    }
}
