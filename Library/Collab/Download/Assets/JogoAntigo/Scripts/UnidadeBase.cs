using UnityEngine;

public class UnidadeBase : MonoBehaviour
{
    public bool _Marcada;
    public bool Marcada
    {
        get
        {
            return _Marcada;
        }
        set
        {
            _Marcada = value;
            CarregaAcoes();
        }
    }

    internal virtual void Start()
    {
        Marcada = false;
    }

    internal virtual void OnMouseDown()
    {
        Unidades.Marcar(this);
    }

    internal virtual void CarregaAcoes()
    {

    }
}
