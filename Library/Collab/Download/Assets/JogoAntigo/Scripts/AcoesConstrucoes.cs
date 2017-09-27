using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AcoesConstrucoes : MonoBehaviour
{
    public Transform _Medidor;
    public Text _Titulo;
    public Construcao _ConstrucaoMarcada;
    private Acoes _Acoes;
    [SerializeField]
    private Transform _Item;
    [SerializeField]
    private Transform _Itens;

    private void Awake()
    {
        _Acoes = FindObjectOfType<Acoes>();
    }

    private void OnEnable()
    {
        _Medidor.gameObject.SetActive(false);
        if (_ConstrucaoMarcada)
            _Titulo.text = _ConstrucaoMarcada._TipoConstrucao._Descricao;
    }

    private void CarregaItens()
    {
        TiposUnidadesMoveis[] lUnidadesMoveis = GerenciadorTiposUnidadesMoveis.Instancia.ReadByIdConstrucao(_ConstrucaoMarcada._TipoConstrucao._IdTiposConstrucoes);
        foreach (TiposUnidadesMoveis lUnidadeMovel in lUnidadesMoveis)
        {
            _Item.name = lUnidadeMovel._Descricao;
            BotaoUnidadeMovel lBotaoUnidadeMovel = _Item.GetComponent<BotaoUnidadeMovel>();
            lBotaoUnidadeMovel._TipoUnidadeMovel = lUnidadeMovel;
            Instantiate(_Item, _Itens);
            Vector2 lTamanhoItens = _Itens.GetComponent<RectTransform>().sizeDelta;
            lTamanhoItens.y += _Itens.GetComponent<GridLayoutGroup>().cellSize.y;
            _Itens.GetComponent<RectTransform>().sizeDelta = lTamanhoItens;
        }
    }

    public void Fechar()
    {
        gameObject.SetActive(false);
    }

    /*private IEnumerator CarregarExercitosDisponiveis()
    {
        Exercitos lExercitos = new Exercitos();
        lExercitos._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
        yield return StartCoroutine(lExercitos.ReadByIdUsuario());
        if (lExercitos.Retorno)
        {
            foreach (Exercitos lExercito in lExercitos.Registros)
            {
                TiposUnidadesMoveis lTipoUnidadeMovel = GerenciadorTiposUnidadesMoveis.Instancia.ReadById(lExercito._IdTiposUnidadesMoveis);
                if (lTipoUnidadeMovel._IdTiposConstrucoes == _ConstrucaoMarcada._IdTiposConstrucoes)
                {
                    Transform lUnidadeMovelPrefab = Instantiate(_UnidadeMovelPrefab, _UnidadeMovelPrefab.parent);
                    lUnidadeMovelPrefab.GetComponent<UnidadeMovel>()._Exercito = lExercito;
                    CarregarInfoUnidadeMovel(lUnidadeMovelPrefab, lTipoUnidadeMovel._IdTiposUnidadesMoveis);
                }
            }
        }
    }*/

    public IEnumerator CarregarConstrutoresContrucao(Construcoes pConstrucao, int pQuantidade = 0)
    {
        if (pQuantidade != 0)
        {
            Exercitos lExercitos = new Exercitos();
            RlcConstrucoesExercitos lRlcConstrucoesExercitos = new RlcConstrucoesExercitos();
            for (int i = 0; i < Mathf.Abs(pQuantidade); i++)
            {
                if (pQuantidade > 0)
                {
                    lExercitos._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
                    lExercitos._IdTiposUnidadesMoveis = 1;
                    yield return StartCoroutine(lExercitos.ReadByIdUsuario(false));
                    if (lExercitos.Retorno)
                    {
                        if (i < lExercitos.Registros.Count)
                        {
                            lExercitos.Registros[i]._Ocupado = true;
                            lRlcConstrucoesExercitos._IdExercitos = lExercitos.Registros[i]._IdExercitos;
                            lRlcConstrucoesExercitos._IdConstrucoes = pConstrucao._IdConstrucoes;
                            yield return StartCoroutine(lRlcConstrucoesExercitos.Create());
                            if (lRlcConstrucoesExercitos.Retorno)
                            {
                                yield return StartCoroutine(lExercitos.Registros[i].UpdateById());
                            }
                        }
                    }
                }
                else if (pQuantidade < 0)
                {
                    yield return StartCoroutine(lRlcConstrucoesExercitos.ReadByIdConstrucoes(pConstrucao._IdConstrucoes));
                    if (lRlcConstrucoesExercitos.Retorno)
                    {
                        if (i < lRlcConstrucoesExercitos.Registros.Count)
                        {
                            yield return StartCoroutine(lExercitos.ReadById(lRlcConstrucoesExercitos.Registros[i]._IdExercitos));
                            if (lExercitos.Retorno)
                            {
                                lExercitos._Ocupado = false;
                                yield return StartCoroutine(lExercitos.UpdateById());
                                if (lExercitos.Retorno)
                                {
                                    yield return StartCoroutine(lRlcConstrucoesExercitos.Registros[i].DeleteById());
                                }
                            }
                        }
                    }
                }
            }
        }
        yield return StartCoroutine(_Acoes.AtualizaRecursos());
    }
}
