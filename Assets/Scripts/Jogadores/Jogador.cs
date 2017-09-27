using UnityEngine;
using System.Collections;

public class Jogador : MonoBehaviour
{
    public BarraVida _BarraVida;
    public BarraMana _BarraMana;
    internal Vector2 _Eixo;
    internal Rigidbody2D _Controle;
    internal float _VelocidadeAtual;
    public EDirecao _Direcao;
    internal bool _Ativo;
   
    public bool Ativo
    {
        get
        {
            return _Ativo;
        }
        set
        {
            StartCoroutine(Ativa(value));
        }
    }    

    public enum EDirecao
    {
        Direita = -1,
        Esquerda = 1
    }

    internal virtual void Start()
    {
        _Controle = GetComponent<Rigidbody2D>();
        _Ativo = true;
    }

    internal virtual void Update()
    {
        if (_Ativo)
        {
            if (_Eixo.x < 0)
                _Direcao = EDirecao.Direita;
            else if (_Eixo.x > 0)
                _Direcao = EDirecao.Esquerda;
            Vector3 lScala = transform.localScale;
            lScala.x = (int)_Direcao;
            transform.localScale = lScala;
        }
    }

    private void FixedUpdate()
    {
        if (_Ativo)
           Movimento();
    }

    private IEnumerator Ativa(bool pValor)
    {
        yield return new WaitForSeconds(0.5f);
        _Ativo = pValor;
    }

    private void Movimento()
    {
        _Controle.velocity = new Vector2(_Eixo.x * Time.fixedDeltaTime * _VelocidadeAtual, _Eixo.y * Time.fixedDeltaTime * _VelocidadeAtual);
    }

    public void Reinicia()
    {
        Carrega.Cena = GameTags.Cenas[(int)GameTags.ECenas.MenuPrincipal];
    }    
}
