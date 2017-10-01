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
        Jogadores[] lJogadores = FindObjectsOfType<Jogadores>();
        foreach (Jogadores lJogador in lJogadores)
        {
            lJogador.Ativo = false;
        }
    }

    private void OnDisable()
    {
        Jogadores[] lJogadores = FindObjectsOfType<Jogadores>();
        foreach (Jogadores lJogador in lJogadores)
        {
            lJogador.Ativo = true;
        }
    }
}
