using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class ComponenteBase : MonoBehaviour
{
    [SerializeField]
    private bool _Focused;
    [SerializeField]
    private int _TabOrder;

    public bool Focused
    {
        get
        {
            return _Focused;
        }
        set
        {
            _Focused = value;
            GetComponent<Outline>().enabled = _Focused;
        }
    }

    public int TabOrder
    {
        get
        {
            return _TabOrder;
        }
        set
        {
            _TabOrder = value;
        }
    }

    public static void Focar(ComponenteBase[] pComponentes, int pInc, int pTabIndex = 0)
    {
        foreach (ComponenteBase lComponente in pComponentes)
        {
            if (pInc == 0)
            {
                lComponente.Focused = pTabIndex == lComponente.TabOrder;
            }
            else
            {
                if (lComponente.Focused)
                {
                    if (lComponente.TabOrder + pInc > pComponentes.Length - 1)
                    {
                        Focar(pComponentes, 0);
                    }
                    else if (lComponente.TabOrder + pInc < 0)
                    {
                        Focar(pComponentes, pComponentes.Length - 1);
                    }
                    else
                    {
                        Focar(pComponentes, 0, lComponente.TabOrder + pInc);
                    }
                    break;
                }
            }
        }
    }

    public static ComponenteBase Focado(ComponenteBase[] pComponentes)
    {
        ComponenteBase lComponenteFocado = null;
        foreach (ComponenteBase lComponente in pComponentes)
        {
            if (lComponente.Focused)
            {
                lComponenteFocado = lComponente;
                //break;
            }
        }
        return lComponenteFocado;
    }
}
