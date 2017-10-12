using UnityEngine;

public class Poder : MonoBehaviour
{
    public float _Velocidade;
    public Rigidbody2D _Controle;
    [SerializeField]
    private bool _Jogador;

    private void Awake()
    {
        _Controle = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void OnTriggerStay2D(Collider2D pColisao)
    {
        if (_Jogador)
        {
            Inimigos lInimigo = pColisao.GetComponent<Inimigos>();
            if (lInimigo != null)
            {
                lInimigo._BarraVida.Dano = 10;
                Destroy(gameObject);
            }
        }
        else
        {
            Jogadores lJogador = pColisao.GetComponent<Jogadores>();
            if (lJogador != null)
            {
                lJogador._BarraVida.Dano = 10;
                Destroy(gameObject);
            }
        }
    }
}
