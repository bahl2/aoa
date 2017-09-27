using UnityEngine;

public class ControleFase1 : MonoBehaviour
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
            lJogador._Direcao = Jogador.EDirecao.Esquerda;
        }
    }


    private void Update()
    {
        _Inimigos = FindObjectsOfType<Inimigo>();
        if (_Inimigos != null)
        {
            if (_Inimigos.Length <= 0)
            {
                Carrega.Cena = GameTags.Cenas[(int)GameTags.ECenas.MenuPrincipal];
            }
        }
        else
        {
            Carrega.Cena = GameTags.Cenas[(int)GameTags.ECenas.MenuPrincipal];
        }
    }
}
