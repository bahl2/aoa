using UnityEngine;
using UnityEngine.UI;

public class ComponenteSlider : ComponenteBase
{
    public float Value
    {
        get
        {
            return _Slider.value;
        }
        set
        {
            _Slider.value = value;
        }
    }

    [SerializeField]
    private Slider _Slider;
}
