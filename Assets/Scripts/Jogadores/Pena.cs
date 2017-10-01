using UnityEngine;

public class Pena : MonoBehaviour
{
    [SerializeField]
    private GameObject _Legenda;

    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        Jogadores lJogador = pColisao.GetComponent<Jogadores>();
        if (lJogador != null)
        {
            _Legenda.SetActive(true);
            lJogador._BarraMana.AddMana(lJogador._BarraMana._ManaMaxima);
            Destroy(gameObject);
        }
    }
}
