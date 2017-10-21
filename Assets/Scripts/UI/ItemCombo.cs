﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ItemCombo : MonoBehaviour
    {
        [SerializeField]
        private bool _Focused;
        [SerializeField]
        private string _Value;

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
    }
}
