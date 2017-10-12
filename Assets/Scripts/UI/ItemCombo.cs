using UnityEngine;
using UnityEngine.UI;

public class ItemCombo : MonoBehaviour
{
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

    public string Value
    {
        get
        {
            return _Value;
        }
    }

    [SerializeField]
    private string _Value;
    [SerializeField]
    private bool _Focused;
}
