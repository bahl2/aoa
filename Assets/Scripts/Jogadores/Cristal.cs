using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField]
    private float _TempoCurar;
    private float _TempoCurando;

    private void Update()
    {
        _TempoCurando += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D pColisao)
    {
        if (_TempoCurando > _TempoCurar)
        {
            _TempoCurando = 0;
            Jogadores lJogador = pColisao.GetComponent<Jogadores>();
            if (lJogador != null)
            {
                lJogador._BarraVida.AddVida(20);
            }
        }
    }
}
