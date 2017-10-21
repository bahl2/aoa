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
            StartCoroutine(CarregaCena(_Cena));
        }

        private IEnumerator CarregaCena(string pCena)
        {
            AsyncOperation lCarrega = SceneManager.LoadSceneAsync(pCena);//carrega a cena
            lCarrega.allowSceneActivation = false;
            while (!lCarrega.isDone)//enquanto nao estiver carregado aguarda 
            {
                _Progresso.value = lCarrega.progress;
                if (lCarrega.progress == 0.9f)
                {
                    _Progresso.value = 1f;
                    lCarrega.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
