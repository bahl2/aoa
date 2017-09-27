using UnityEngine;

public class Inimigo : MonoBehaviour
{
    internal Vector3 _Eixo;
    public BarraVida _BarraVida;
    internal Animator _Animacao;
    internal EDirecao _Direcao;
    internal EAnimacao _AnimacaoAtual;
    internal float _VelocidadeAtual;

    public enum EAnimacao
    {
        Patrulhando,
        Morrendo
    }

    public enum EDirecao
    {
        Direita = -1,
        Esquerda = 1
    }

    internal virtual void Start()
    {
        _Animacao = GetComponent<Animator>();
    }

    internal virtual void Update()
    {
        if (_Eixo.x < 0)
            _Direcao = EDirecao.Direita;
        else if (_Eixo.x > 0)
            _Direcao = EDirecao.Esquerda;
        Vector3 lScala = transform.localScale;
        lScala.x = (int)_Direcao;
        transform.localScale = lScala;
        if (_BarraVida.gameObject.activeSelf)
        {
            if (_BarraVida._VidaAtual <= 0)
            {
                Morre();
            }
        }
        _Animacao.SetInteger("Animacao", (int)_AnimacaoAtual);
    }

    private void Morre()
    {
        _AnimacaoAtual = EAnimacao.Morrendo;
        _VelocidadeAtual = 0;
    }

    public void Destroir()
    {
        Destroy(gameObject);
    }
}
