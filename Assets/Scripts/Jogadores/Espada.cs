using System.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{

    [SerializeField]
    private Jogadores _Jogador;
    [SerializeField]
    private float _TempoAtaque;
    private float _TempoAtacando;
    private BoxCollider2D _Colisor;
    private int _Dano;

    private void Start()
    {
        _Colisor = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _TempoAtacando += Time.deltaTime;
        if (_TempoAtacando >= _TempoAtaque)
        {
            _TempoAtacando = 0;
            if (_Jogador._Acao == Personagens.EAcoes.Ataque1)
            {
                _Dano = 10;
            }
            else if (_Jogador._Acao == Personagens.EAcoes.Ataque2)
            {
                _Dano = 20;
            }
            else
            {
                _Dano = 30;
            }
            RaycastHit2D[] lHits = Physics2D.BoxCastAll(transform.position, _Colisor.size, _Colisor.edgeRadius,
                _Jogador.transform.TransformDirection(Vector2.right));
            /*GameObject test = new GameObject("test");
            test.transform.position = transform.position;
            test.AddComponent<BoxCollider2D>().size = _Colisor.size;
            test.transform.rotation = Quaternion.Euler(0, 0, _Colisor.edgeRadius);*/

            foreach (RaycastHit2D lHit in lHits)
            {
                Inimigos lInimigo = lHit.transform.GetComponent<Inimigos>();
                if (lInimigo != null)
                {
                    lInimigo._BarraVida.gameObject.SetActive(true);
                    lInimigo._BarraVida.Dano = _Dano;
                    StartCoroutine(DesativaVida(lInimigo._BarraVida.gameObject));
                }
            }
        }
    }

    private IEnumerator DesativaVida(GameObject pVida)
    {
        yield return new WaitForSeconds(5);
        pVida.SetActive(false);
    }
}
