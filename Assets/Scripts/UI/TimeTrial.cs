using Assets.Scripts.Jogadores;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TimeTrial : MonoBehaviour
    {
        public Text _Contador;
        public int _Tempo;

        private void Start()
        {
            _Tempo = 0;
            _Contador.text = _Tempo.ToString();
            StartCoroutine(Contador());
        }

        private IEnumerator Contador()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1);
                if (Time.timeScale > 0 && FindObjectOfType<Jogador>().Ativo)
                {
                    _Tempo++;
                    GetComponent<Animator>().enabled = true;
                }
                else
                    GetComponent<Animator>().enabled = false;
                _Contador.text = _Tempo.ToString();
            }
        }
    }
}
