using Assets.Scripts.Objetos;
using UnityEngine;

public class GerenciadorTiposRecursos : MonoBehaviour
{
    private static GerenciadorTiposRecursos _Instancia;
    public TiposRecursos[] _TiposRecursos;
    public static GerenciadorTiposRecursos Instancia
    {
        get
        {
            if (!_Instancia)
            {
                GameObject lGO = new GameObject("GerenciadorTiposRecursos");
                lGO.AddComponent<GerenciadorTiposRecursos>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    public TiposRecursos ReadById(int pId)
    {
        if (_TiposRecursos == null)
        {
            Read();
        }
        foreach (TiposRecursos lTipoRecursoAtual in _TiposRecursos)
        {
            if (lTipoRecursoAtual._IdTiposRecursos == pId)
            {
                return lTipoRecursoAtual;
            }
        }
        return null;
    }

    public TiposRecursos[] Read()
    {
        TextAsset lInfo = Resources.Load<TextAsset>("Infos/TiposRecursos");
        _TiposRecursos = XML.ParseTiposRecursos(lInfo.text);
        return _TiposRecursos;
    }
}
