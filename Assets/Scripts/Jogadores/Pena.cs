using UnityEngine;

namespace Assets.Scripts.Jogadores
{
    public class Pena : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Legenda;
        [SerializeField]
        private GameObject[] _Poderes;


        private void Start()
        {
            foreach (GameObject lPoder in _Poderes)
            {
                lPoder.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (lJogador != null)
            {
                _Legenda.SetActive(true);
                lJogador._BarraMana.Add(lJogador._BarraMana._Maximo);
                foreach (GameObject lPoder in _Poderes)
                {
                    lPoder.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
