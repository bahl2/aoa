using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public int Dano
    {
        set
        {
            _Dano = value;
            AddVida(-_Dano);
        }
    }
    public float _VidaMaxima;
    public float _VidaAtual;
    private Image _BarraVida;
    private int _Dano;

    private void Start()
    {
        _BarraVida = transform.Find("Vida").GetComponent<Image>();
        _BarraVida.fillAmount = _VidaAtual == 0 ? 0 : _VidaAtual / _VidaMaxima;// o fillamount impede numeros negativos        
    }

    public void AddVida(float pVida)
    {
        if (pVida == 0)
            pVida -= _VidaAtual;
        _VidaAtual += pVida;
        if (_BarraVida != null)
            _BarraVida.fillAmount = _VidaAtual == 0 ? 0 : _VidaAtual / _VidaMaxima;// o fillamount impede numeros negativos
    }
}
