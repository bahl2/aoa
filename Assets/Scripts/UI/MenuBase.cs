using System.Collections;
using UnityEngine;

public class MenuBase : MonoBehaviour
{

    public bool Ativo
    {
        get
        {
            return _Ativo;
        }
        set
        {
            StartCoroutine(Ativa(value));
            if (value)
            {
                ComponenteBase.Focar(_Componentes, 0);
                transform.parent.GetComponent<Canvas>().sortingOrder = 1;
            }
            else transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        }
    }

    [SerializeField]
    internal ComponenteBase[] _Componentes;
    internal bool _Ativo;

    internal virtual IEnumerator Ativa(bool pValor)
    {
        yield return new WaitForSeconds(0.5f);
        _Ativo = pValor;
    }
}
