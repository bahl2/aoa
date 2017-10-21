using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ComponenteSlider : ComponenteBase
    {
        [SerializeField]
        private Slider _Slider;

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
    }
}
