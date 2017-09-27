using Assets.Scripts.Objetos;
using UnityEngine;
using UnityEngine.UI;

public class BotaoConstrucao : MonoBehaviour
{

    public Text _Titulo;
    public Image _Foto;
    public Text _Cristais;
    public Text _Madeira;
    public Text _Aldeoes;
    public TiposConstrucoes _TipoConstrucao;
    private GerenciadorConstrucoes _GerenciadorConstrucoes;
    private GerenciadorUI _GerenciadorUI;
    private Acoes _Acoes;
    private AcoesConstrucoes _AcoesConstrucoes;

    private void Awake()
    {
        _AcoesConstrucoes = FindObjectOfType<AcoesConstrucoes>();
        _GerenciadorUI = FindObjectOfType<GerenciadorUI>();
        _GerenciadorConstrucoes = FindObjectOfType<GerenciadorConstrucoes>();
        _Acoes = FindObjectOfType<Acoes>();
    }

    private void Start()
    {
        _Titulo.text = _TipoConstrucao._Descricao;
        _Foto.sprite = _TipoConstrucao._Foto;
        _Cristais.text = _TipoConstrucao._Cristais.ToString();
        _Madeira.text = _TipoConstrucao._Madeira.ToString();
        _Aldeoes.text = _TipoConstrucao._Aldeoes.ToString();
    }

    public void Construir()
    {
        bool lTemAldeoes = _GerenciadorUI.AldeoesDesocupados >= _TipoConstrucao._Aldeoes;
        bool lTemCristais = _GerenciadorUI.Cristais >= _TipoConstrucao._Cristais;
        bool lTemMadeira = _GerenciadorUI.Madeira >= _TipoConstrucao._Madeira;
        if (lTemAldeoes && lTemCristais && lTemMadeira)
        {
            Construcoes lConstrucoes = new Construcoes();
            lConstrucoes._IdTiposConstrucoes = _TipoConstrucao._IdTiposConstrucoes;
            lConstrucoes._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
            lConstrucoes._Nivel = 1;
            lConstrucoes._SegundosConstruindo = 0;
            lConstrucoes._Posicionada = false;
            StartCoroutine(lConstrucoes.Create());
            if (_TipoConstrucao._IdTiposConstrucoes == 1)
            {
                Exercitos lExercitos = new Exercitos();
                lExercitos._IdTiposUnidadesMoveis = 1;
                lExercitos._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
                lExercitos._Nivel = 1;
                lExercitos._Ocupado = false;
                lExercitos._Vida = 100;
                StartCoroutine(lExercitos.Create());
            }
            Transform lPrefabConstrucao = Instantiate(_TipoConstrucao._Prefab);
            Construcao lConstrucao = lPrefabConstrucao.GetComponent<Construcao>();
            StartCoroutine(_Acoes.CalculaValor(-_TipoConstrucao._Cristais, -_TipoConstrucao._Madeira));
            StartCoroutine(_AcoesConstrucoes.CarregarConstrutoresContrucao(lConstrucao._Construcao, _TipoConstrucao._Aldeoes));
            _GerenciadorConstrucoes.gameObject.SetActive(false);
        }
    }
}
