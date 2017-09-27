using Assets.Scripts.Objetos;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorConstrucoes : MonoBehaviour
{
    [SerializeField]
    private Transform _Item;
    [SerializeField]
    private Transform _Itens;

    private void Start()
    {
        CarregaItens();
    }

    private void CarregaItens()
    {
        TiposConstrucoes[] lConstrucoes = GerenciadorTiposContrucoes.Instancia.Read();
        foreach (TiposConstrucoes lConstrucao in lConstrucoes)
        {
            _Item.name = lConstrucao._Descricao;
            BotaoConstrucao lBotaoConstrucao = _Item.GetComponent<BotaoConstrucao>();
            lBotaoConstrucao._TipoConstrucao = lConstrucao;
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
}
