using UnityEngine;

public class Poder : MonoBehaviour
{
    [SerializeField]
    public float _Velocidade;
    public Rigidbody2D _Controle;
    public bool _Jogador;

    private void Awake()
    {
        _Controle = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D pColisao)
    {
        if (_Jogador)
        {
            Inimigo lInimigo = pColisao.GetComponent<Inimigo>();
            if (lInimigo != null)
            {
                lInimigo._BarraVida.Dano = 10;
                Destroy(gameObject);
            }
        }
        else
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (lJogador != null)
            {
                lJogador._BarraVida.Dano = 10;
                Destroy(gameObject);
            }
        }
    }
}
