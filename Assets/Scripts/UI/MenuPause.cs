using Assets.Scripts.Global;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MenuPause : MonoBehaviour
    {
        public void RetornarJogo()
        {
            gameObject.SetActive(false);
        }

        public void Menu()
        {
            Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
        }

        public void Sair()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            Cursor.visible = true;
            Personagem[] lPersonagens = FindObjectsOfType<Personagem>();
            foreach (Personagem lPersonagem in lPersonagens)
            {
                lPersonagem.Ativo = false;
            }
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Cursor.visible = false;
            Personagem[] lPersonagens = FindObjectsOfType<Personagem>();
            foreach (Personagem lPersonagem in lPersonagens)
            {
                lPersonagem.Ativo = true;
            }
            Time.timeScale = 1;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
