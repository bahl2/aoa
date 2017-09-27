using UnityEngine;

public class Pena : MonoBehaviour
{
    [SerializeField]
    private GameObject _Legenda;    

    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        Jogador lJogador = pColisao.GetComponent<Jogador>();
        if (lJogador != null)
        {
            _Legenda.SetActive(true);                                    
            lJogador._BarraMana.AddMana(lJogador._BarraMana._ManaMaxima);
            Destroy(gameObject);
        }
    }
}
