using Assets.Scripts.Objetos;
using UnityEngine;

public class GerenciadorTiposContrucoes : MonoBehaviour
{
    private static GerenciadorTiposContrucoes _Instancia;
    public TiposConstrucoes[] _TiposContrucoes;
    public static GerenciadorTiposContrucoes Instancia
    {
        get
        {
            if (!_Instancia)
            {
                GameObject lGO = new GameObject("GerenciadorTiposContrucoes");
                lGO.AddComponent<GerenciadorTiposContrucoes>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    public TiposConstrucoes ReadById(int pId)
    {
        if (_TiposContrucoes == null)
        {
            Read();
        }
        foreach (TiposConstrucoes lTipoConstrucaoAtual in _TiposContrucoes)
        {
            if (lTipoConstrucaoAtual._IdTiposConstrucoes == pId)
            {
                return lTipoConstrucaoAtual;
            }
        }
        return null;
    }

    public TiposConstrucoes ReadByDescricao(string pDescricao)
    {
        if (_TiposContrucoes == null)
        {
            Read();
        }
        foreach (TiposConstrucoes lTipoConstrucaoAtual in _TiposContrucoes)
        {
            if (lTipoConstrucaoAtual._Descricao == pDescricao)
            {
                return lTipoConstrucaoAtual;
            }
        }
        return null;
    }

    public TiposConstrucoes[] Read()
    {
        TextAsset lInfo = Resources.Load<TextAsset>("Infos/TiposConstrucoes");
        _TiposContrucoes = XML.ParseTiposConstrucoes(lInfo.text);
        return _TiposContrucoes;
    }
}
