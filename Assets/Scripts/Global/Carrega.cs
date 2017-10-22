using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Global
{
    public class Carrega : MonoBehaviour
    {
        [SerializeField]
        private Slider _Progresso;
        private static string _Cena;

        public static string Cena
        {
            set
            {
                _Cena = value;// prepara cena de loading com a cena que deve ser carregada
                SceneManager.LoadScene(GameTags._Cenas[(int)GameTags.ECenas.Carrega]);
            }
        }

        private void Start()
        {
            StartCoroutine(CarregaCena(_Cena, _Progresso));
        }

        public static IEnumerator CarregaCena(string pCena, Slider pProgresso = null)
        {
            AsyncOperation lCarrega = SceneManager.LoadSceneAsync(pCena);//carrega a cena
            lCarrega.allowSceneActivation = false;
            if (pProgresso != null)
                pProgresso.gameObject.SetActive(true);
            while (!lCarrega.isDone)//enquanto nao estiver carregado aguarda 
            {
                if (pProgresso != null)
                    pProgresso.value = lCarrega.progress;
                if (lCarrega.progress == 0.9f)
                {
                    if (pProgresso != null)
                        pProgresso.value = 1f;
                    lCarrega.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
