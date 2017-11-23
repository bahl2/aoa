using Assets.Scripts.Global;
using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuPause : MonoBehaviour
    {
        [SerializeField]
        private Text[] _TextosBotoes;
        [SerializeField]
        private Slider _Progresso;
        [SerializeField]
        private GameObject _Carregando;

        private void Awake()
        {
            if (CFG._Plataforma == CFG.EPlataforma.PC)
                StartCoroutine(TraduzTextos());
            else
            {
                Destroy(_Carregando);
                Destroy(gameObject);
            }
        }

        private IEnumerator TraduzTextos()
        {
            for (int i = 0; i < _TextosBotoes.Length; i++)
            {
                GoogleTradutor lGoogleTradutor = new GoogleTradutor(GoogleTradutor._Siglas[(int)CFG.Idioma], _TextosBotoes[i].text);
                yield return lGoogleTradutor.Traduzir();
                _TextosBotoes[i].text = lGoogleTradutor._Resposta;
                _Progresso.value = _TextosBotoes.Length / (i + 1);
            }
            Destroy(_Carregando);
            gameObject.SetActive(false);
        }

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
