using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ComponenteCombo : ComponenteBase
    {
        [SerializeField]
        private List<ItemCombo> _Itens;
        private string _Value;

        public string Value
        {
            set
            {
                foreach (ItemCombo lItem in _Itens)
                {
                    if (lItem.Value == value)
                        lItem.Focused = true;
                    else
                        lItem.Focused = false;
                }
            }
        }

        private void Start()
        {
            FocarItem(0);
        }

        public ItemCombo FocarItem(int pItem)
        {
            foreach (ItemCombo lItem in _Itens)
            {
                lItem.Focused = false;
            }
            _Itens[pItem].Focused = true;
            return _Itens[pItem];
        }

        public ItemCombo Focar(int pInc = 0)
        {
            ItemCombo lProximoItem = _Itens[0];
            for (int i = 0; i < _Itens.Count; i++)
            {
                if (_Itens[i].Focused)
                {
                    _Itens[i].Focused = false;
                    if (pInc > 0 && i + pInc >= _Itens.Count)
                    {
                        pInc = 0;
                    }
                    else if (pInc < 0 && i + pInc < 0)
                    {
                        pInc = _Itens.Count - 1;
                    }
                    else
                    {
                        pInc += i;
                    }
                    lProximoItem = _Itens[pInc];
                }
            }
            lProximoItem.Focused = true;
            return lProximoItem;
        }
    }
}
