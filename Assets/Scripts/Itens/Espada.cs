using UnityEngine;
using System.Collections;

public class Espada : MonoBehaviour
{

    [SerializeField]
    private GameObject _Pesonagem;
    [SerializeField]
    private float _TempoAtaque;
    private float _TempoAtacando;
    private BarraVida _Vida;
    private int _Dano;

    private void Start()
    {
        _Vida = null;
    }

    private void Update()
    {
        _TempoAtacando += Time.deltaTime;
        if (_Vida != null)
        {
            if (_TempoAtacando >= _TempoAtaque)
            {
                _TempoAtacando = 0;
                _Vida.Dano = _Dano;
                StartCoroutine(DesativaVida());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D pColisao)
    {
        Inimigo lInimigoAtacando = _Pesonagem.GetComponent<Inimigo>();
        Jogador lJogadorAtacando = _Pesonagem.GetComponent<Jogador>();
        if (lJogadorAtacando != null)
        {
            Inimigo lInimigo = pColisao.GetComponent<Inimigo>();
            if (lInimigo != null)
            {
                _Vida = lInimigo._BarraVida;
                _Vida.gameObject.SetActive(true);
                if (lJogadorAtacando.GetComponent<ControleMiguel>()._AnimacaoAtual == ControleMiguel.EAnimacao.Ataque1)
                {
                    _Dano = 10;
                }
                else if (lJogadorAtacando.GetComponent<ControleMiguel>()._AnimacaoAtual == ControleMiguel.EAnimacao.Ataque2)
                {
                    _Dano = 20;
                }
                else
                {
                    _Dano = 30;
                }
            }
        }
        else if (lInimigoAtacando != null)
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (lJogador != null)
            {
                _Vida = lJogador._BarraVida;
                _Dano = 10;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D pColisao)
    {
        _Vida = null;        
    }
    
    private IEnumerator DesativaVida()
    {
        yield return new WaitForSeconds(1);
        if (_Vida != null)
           _Vida.gameObject.SetActive(false);        
    }
}
