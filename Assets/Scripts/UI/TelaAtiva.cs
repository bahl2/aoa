using Assets.Scripts.Global;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TelaAtiva : MonoBehaviour
    {
        private float _Tempo;

        private void Update()
        {
            _Tempo += Time.deltaTime;
            if (Input.anyKeyDown && _Tempo > 5)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Personagem[] lPersonagens = FindObjectsOfType<Personagem>();
            foreach (Personagem lPersonagem in lPersonagens)
            {
                lPersonagem.Ativo = false;
            }
        }

        private void OnDisable()
        {
            Personagem[] lPersonagens = FindObjectsOfType<Personagem>();
            foreach (Personagem lPersonagem in lPersonagens)
            {
                lPersonagem.Ativo = true;
            }
        }
    }
}
