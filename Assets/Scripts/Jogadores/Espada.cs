using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField]
    private Jogadores _Jogador;
    [SerializeField]
    private float _TempoAtaque;
    [SerializeField]
    private float[] _DanoCombos;
    [SerializeField]
    private float[] _SpecialForcaCombos;
    [SerializeField]
    private float[] _DistanciaCombos;
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
            int lCombo = 0;
            if (_Jogador._Acao == Personagens.EAcoes.Combo1)
            {
                lCombo = 0;
            }
            else if (_Jogador._Acao == Personagens.EAcoes.Combo2)
            {
                lCombo = 1;
            }
            else
            {
                lCombo = 2;
            }
            RaycastHit2D[] lHits = Physics2D.CircleCastAll(transform.position, _Colisor.radius, new Vector2(1, 1), _DistanciaCombos[lCombo]);
            foreach (RaycastHit2D lHit in lHits)
            {
                Inimigos lInimigo = lHit.transform.GetComponent<Inimigos>();
                if (lInimigo != null)
                {
                    lInimigo._BarraVida.gameObject.SetActive(true);
                    lInimigo._BarraVida.Add(-_DanoCombos[lCombo]);
                    _Jogador._BarraSpecial.Add(_SpecialForcaCombos[lCombo]);
                    Vector3 lPosicaoInimigo = lInimigo.transform.position;
                    lPosicaoInimigo.x += -(int)lInimigo._Direcao;
                    lInimigo.transform.position = lPosicaoInimigo;
                    StartCoroutine(lInimigo._BarraVida.Desativa());
                }
            }
        }
    }


}
