using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarraProgresso : MonoBehaviour
{
    public float _Maximo;
    public float _Atual;
    private Image _Barra;

    public IEnumerator Desativa()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    public void Add(float pAdd)
    {
        if (pAdd < 0 && _Atual < -pAdd)
            pAdd = -_Atual;
        if (pAdd > 0 && _Atual + pAdd >= _Maximo)
            pAdd = _Maximo - _Atual;
        _Atual += pAdd;
        if (_Barra != null)
            Atualiza();
    }

    private void Start()
    {
        _Barra = transform.Find("Barra").GetComponent<Image>();
        Atualiza();
    }

    private void Atualiza()
    {
        _Barra.fillAmount = _Atual == 0 ? 0 : _Atual / _Maximo;// o fillamount impede numeros negativos        
    }
}
