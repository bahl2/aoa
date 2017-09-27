using Assets.Scripts.Objetos;
using UnityEngine;
using UnityEngine.UI;

public class BotaoUnidadeMovel : MonoBehaviour
{

    public Text _Titulo;
    public Image _Foto;
    public Text _Cristais;
    public Text _Madeira;
    public TiposUnidadesMoveis _TipoUnidadeMovel;
    private GerenciadorUI _GerenciadorUI;
    private Acoes _Acoes;
    private AcoesConstrucoes _AcoesConstrucoes;

    private void Awake()
    {
        _GerenciadorUI = FindObjectOfType<GerenciadorUI>();
        _Acoes = FindObjectOfType<Acoes>();
        _AcoesConstrucoes = FindObjectOfType<AcoesConstrucoes>();
    }

    private void Start()
    {
        _Titulo.text = _TipoUnidadeMovel._Descricao;
        _Foto.sprite = _TipoUnidadeMovel._Foto;
        _Cristais.text = _TipoUnidadeMovel._Cristais.ToString();
        _Madeira.text = _TipoUnidadeMovel._Madeira.ToString();
    }

    public void Treinar()
    {

    }
}
