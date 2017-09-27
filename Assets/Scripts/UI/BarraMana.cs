using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarraMana : MonoBehaviour
{

    [SerializeField]
    private Jogador _Jogador;
    public float _ManaMaxima;
    public float _ManaAtual;
    private Image _BarraMana;

    private void Start()
    {
        StartCoroutine(Mana());
        _BarraMana = transform.Find("Mana").GetComponent<Image>();
        _ManaAtual = 0;
        _BarraMana.fillAmount = _ManaAtual == 0 ? 0 : _ManaAtual / _ManaMaxima;// o fillamount impede numeros negativos
    }

    private IEnumerator Mana()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (_ManaAtual > 0 && _ManaAtual <= _ManaMaxima)
            {                
                if (_Jogador as ControleMiguel)
                {
                    ControleMiguel lMiguel = _Jogador as ControleMiguel;
                    if (lMiguel._AnimacaoAtual == ControleMiguel.EAnimacao.Voando)
                    {
                        AddMana(-10);
                    }
                }
            }
        }
    }

    public void AddMana(float pMana)
    {
        if (pMana == 0)
            pMana -= _ManaAtual;
        _ManaAtual += pMana;
        _BarraMana.fillAmount = _ManaAtual == 0 ? 0 : _ManaAtual / _ManaMaxima;// o fillamount impede numeros negativos
    }
}
