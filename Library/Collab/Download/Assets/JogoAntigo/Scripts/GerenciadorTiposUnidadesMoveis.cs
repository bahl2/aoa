using Assets.Scripts.Objetos;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorTiposUnidadesMoveis : MonoBehaviour
{
    private static GerenciadorTiposUnidadesMoveis _Instancia;
    public TiposUnidadesMoveis[] _TiposUnidadesMoveis;
    public static GerenciadorTiposUnidadesMoveis Instancia
    {
        get
        {
            if (!_Instancia)
            {
                GameObject lGO = new GameObject("GerenciadorTiposUnidadesMoveis");
                lGO.AddComponent<GerenciadorTiposUnidadesMoveis>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    public TiposUnidadesMoveis ReadById(int pId)
    {
        if (_TiposUnidadesMoveis == null)
        {
            Read();
        }
        foreach (TiposUnidadesMoveis lTipoUnidadeMovelAtual in _TiposUnidadesMoveis)
        {
            if (lTipoUnidadeMovelAtual._IdTiposUnidadesMoveis == pId)
            {
                return lTipoUnidadeMovelAtual;
            }
        }
        return null;
    }

    public TiposUnidadesMoveis ReadByDescricao(string pDescricao)
    {
        if (_TiposUnidadesMoveis == null)
        {
            Read();
        }
        foreach (TiposUnidadesMoveis lTipoUnidadeMovelAtual in _TiposUnidadesMoveis)
        {
            if (lTipoUnidadeMovelAtual._Descricao == pDescricao)
            {
                return lTipoUnidadeMovelAtual;
            }
        }
        return null;
    }

    public TiposUnidadesMoveis[] Read()
    {
        TextAsset lInfo = Resources.Load<TextAsset>("Infos/TiposUnidadesMoveis");
        _TiposUnidadesMoveis = XML.ParseTiposUnidadesMoveis(lInfo.text);
        return _TiposUnidadesMoveis;
    }

    public TiposUnidadesMoveis[] ReadByIdConstrucao(int pIdConstrucao)
    {
        if (_TiposUnidadesMoveis == null)
        {
            Read();
        }
        List<TiposUnidadesMoveis> lTiposUnidadesMoveis = new List<TiposUnidadesMoveis>();
        foreach (TiposUnidadesMoveis lTipoUnidadeMovelAtual in _TiposUnidadesMoveis)
        {
            if (lTipoUnidadeMovelAtual._IdTiposConstrucoes == pIdConstrucao)
            {
                lTiposUnidadesMoveis.Add(lTipoUnidadeMovelAtual);
            }
        }
        return lTiposUnidadesMoveis.ToArray();
    }
}
