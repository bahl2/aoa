using Assets.Scripts.Global;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TelaAtiva : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
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
