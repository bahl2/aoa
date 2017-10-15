using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField]
    private Jogadores _Jogador;
    [SerializeField]
    private float _TempoAtaque;
    private float _TempoAtacando;
    private CircleCollider2D _Colisor;

    private void Start()
    {
        _Colisor = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _TempoAtacando += Time.deltaTime;
        if (_TempoAtacando >= _TempoAtaque)
        {
            _TempoAtacando = 0;
            int lDano = -10;
            int lForca = 35;
            if (_Jogador._Acao == Personagens.EAcoes.Combo1)
            {
                lDano = -10;
                lForca = 35;
            }
            else if (_Jogador._Acao == Personagens.EAcoes.Combo2)
            {
                lDano = -20;
                lForca = 70;
            }
            else
            {
                lDano = -30;
                lForca = 0;
            }
            RaycastHit2D[] lHits = Physics2D.CircleCastAll(transform.position, _Colisor.radius, new Vector2(1, 1));
            foreach (RaycastHit2D lHit in lHits)
            {
                Inimigos lInimigo = lHit.transform.GetComponent<Inimigos>();
                if (lInimigo != null)
                {
                    lInimigo._BarraVida.gameObject.SetActive(true);
                    lInimigo._BarraVida.Add(lDano);
                    _Jogador._BarraSpecial.Add(lForca);
                    Vector3 lPosicaoInimigo = lInimigo.transform.position;
                    lPosicaoInimigo.x += -(int)lInimigo._Direcao;
                    lInimigo.transform.position = lPosicaoInimigo;
                    StartCoroutine(lInimigo._BarraVida.Desativa());
                }
            }
        }
    }


}
