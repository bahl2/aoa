using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;

public class Construcao : UnidadeBase // erda de UnidadeBase
{
    private Animator _Animacoes;
    private GerenciadorUI _GerenciadorUI;
    private Acoes _Acoes;
    public AcoesConstrucoes _AcoesConstrucoes;
    public bool _Completa;
    public bool Completa
    {
        get { return _Completa; }
        set { _Completa = value; CarregaConstrucao(); }
    }
    public TiposConstrucoes _TipoConstrucao;
    public Construcoes _Construcao;
    public bool _Posiciona;
    [SerializeField]
    private Transform _ParticulaConstrucao;
    [SerializeField]
    private Transform _ParticulaCompleta;

    private void Awake()
    {
        _Animacoes = GetComponent<Animator>();
        _AcoesConstrucoes = FindObjectOfType<AcoesConstrucoes>();
        _GerenciadorUI = FindObjectOfType<GerenciadorUI>();
        _Acoes = FindObjectOfType<Acoes>();
    }

    internal override void Start()
    {
        base.Start();
        _TipoConstrucao = GerenciadorTiposContrucoes.Instancia.ReadById(_Construcao._IdTiposConstrucoes);
        Completa = _Construcao._SegundosConstruindo >= _TipoConstrucao._Segundos;
    }

    private void CarregaConstrucao()
    {
        if (_Construcao._Posicionada)
        {
            _Construcao._PosicaoX = transform.position.x;
            _Construcao._PosicaoY = transform.position.y;
            _Construcao._PosicaoZ = transform.position.z;
            if (Completa)
            {
                _Animacoes.SetBool("Construindo", false);
                _ParticulaCompleta.gameObject.SetActive(true);
            }
            else
            {
                StartCoroutine(ContagemTempo());
                _Animacoes.SetBool("Construindo", true);
            }
        }
        else
        {
            _Animacoes.SetBool("Construindo", false);
        }
        StartCoroutine(_Construcao.UpdateById());
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if (Completa && _Construcao._Posicionada)
        {
            _AcoesConstrucoes._ConstrucaoMarcada = this;
            _AcoesConstrucoes.gameObject.SetActive(true);
        }
    }

    private void OnMouseDrag()
    {
        float lDistancia = 10;
        Vector3 lPosicaoMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, lDistancia);
        Vector3 lPosicaoConstrucao = Camera.main.ScreenToWorldPoint(lPosicaoMouse);
        transform.position = lPosicaoConstrucao;
        if (Input.GetMouseButtonDown(1))
        {
            if (_Construcao._Posicionada)
                transform.position = _Construcao.Posicao;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _Construcao._Posicionada = true;
            CarregaConstrucao();
        }
    }

    private void Update()
    {
        if (!_Construcao._Posicionada)
        {
            OnMouseDrag();
        }
    }

    private void OnTriggerEnter2D(Collider2D pColisao)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        _Posiciona = false;
    }

    private void OnTriggerExit2D(Collider2D pColisao)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        _Posiciona = true;
    }

    private IEnumerator ContagemTempo()
    {
        while (_Construcao._SegundosConstruindo < _TipoConstrucao._Segundos)
        {
            _Construcao._SegundosConstruindo++;
            yield return new WaitForSecondsRealtime(1);
            yield return StartCoroutine(_Construcao.UpdateById());
        }
        //yield return StartCoroutine(_AcoesConstrucoes.CarregarConstrutoresContrucao(_Construcao, -_TipoConstrucao._Aldeoes));
        _ParticulaConstrucao.gameObject.SetActive(true);
        Completa = true;
    }
}
