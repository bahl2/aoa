﻿using UnityEngine;

public class ControleFase : MonoBehaviour
{
    public Jogadores[] _Jogadores;
    public GameObject _Legenda;
    private Inimigos[] _Inimigos;

    private void Start()
    {
        _Legenda.SetActive(false);
        //Na fase 1 o jogador começa indo para esquerda
        _Jogadores = FindObjectsOfType<Jogadores>();
        foreach (Jogadores lJogador in _Jogadores)
        {
            lJogador._Direcao = Personagens.EDirecao.Esquerda;
        }
    }


    private void Update()
    {
        _Inimigos = FindObjectsOfType<Inimigos>();
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
