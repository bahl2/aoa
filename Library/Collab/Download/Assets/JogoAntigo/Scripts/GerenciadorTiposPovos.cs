using Assets.Scripts.Objetos;
using UnityEngine;

public class GerenciadorTiposPovos : MonoBehaviour
{
    private static GerenciadorTiposPovos _Instancia;
    public TiposPovos[] _TiposPovos;
    public static GerenciadorTiposPovos Instancia
    {
        get
        {
            if (!_Instancia)
            {
                GameObject lGO = new GameObject("GerenciadorTiposPovos");
                lGO.AddComponent<GerenciadorTiposPovos>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    public TiposPovos ReadById(int pId)
    {
        if (_TiposPovos == null)
        {
            Read();
        }
        foreach (TiposPovos lTipoPovoAtual in _TiposPovos)
        {
            if (lTipoPovoAtual._IdTiposPovos == pId)
            {
                return lTipoPovoAtual;
            }
        }
        return null;
    }

    public TiposPovos ReadByDescricao(string pDescricao)
    {
        if (_TiposPovos == null)
        {
            Read();
        }
        foreach (TiposPovos lTipoPovoAtual in _TiposPovos)
        {
            if (lTipoPovoAtual._Descricao == pDescricao)
            {
                return lTipoPovoAtual;
            }
        }
        return null;
    }

    public TiposPovos[] Read()
    {
        TextAsset lInfo = Resources.Load<TextAsset>("Infos/TiposPovos");
        _TiposPovos = XML.ParseTiposPovos(lInfo.text);
        return _TiposPovos;
    }

    public TiposPovos GetTipoPovoUsuarioLogado()
    {
        return ReadById(GerenciadorUsuarios.Instancia._UsuarioLogado._IdTiposPovos);
    }
}
