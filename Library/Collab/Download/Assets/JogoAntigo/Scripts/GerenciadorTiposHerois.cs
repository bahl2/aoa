using Assets.Scripts.Objetos;
using UnityEngine;

public class GerenciadorTiposHerois : MonoBehaviour
{
    private static GerenciadorTiposHerois _Instancia;
    public TiposHerois[] _TiposHerois;
    public static GerenciadorTiposHerois Instancia
    {
        get
        {
            if (!_Instancia)
            {
                GameObject lGO = new GameObject("GerenciadorTiposHerois");
                lGO.AddComponent<GerenciadorTiposHerois>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    public TiposHerois ReadById(int pId)
    {
        if (_TiposHerois == null)
        {
            Read();
        }
        foreach (TiposHerois lTipoHeroiAtual in _TiposHerois)
        {
            if (lTipoHeroiAtual._IdTiposHerois == pId)
            {
                return lTipoHeroiAtual;
            }
        }
        return null;
    }

    public TiposHerois ReadByDescricao(string pDescricao)
    {
        if (_TiposHerois == null)
        {
            Read();
        }
        foreach (TiposHerois lTipoHeroiAtual in _TiposHerois)
        {
            if (lTipoHeroiAtual._Descricao == pDescricao)
            {
                return lTipoHeroiAtual;
            }
        }
        return null;
    }

    public TiposHerois[] Read()
    {
        TextAsset lInfo = Resources.Load<TextAsset>("Infos/TiposHerois");
        _TiposHerois = XML.ParseTiposHerois(lInfo.text);
        return _TiposHerois;
    }
}
