using UnityEngine;
using UnityEngine.UI;

public class AcoesFonteRecursos : MonoBehaviour
{
    public Transform _Medidor;
    public Text _Titulo;
    public FonteRecursos _FonteRecursosMarcada;
    private Acoes _Acoes;
    private GerenciadorUI _GerenciadorUI;

    private void Awake()
    {
        _Acoes = FindObjectOfType<Acoes>();
        _GerenciadorUI = FindObjectOfType<GerenciadorUI>();
    }

    private void OnEnable()
    {
        if (_FonteRecursosMarcada)
            _Titulo.text = _FonteRecursosMarcada._TipoRecurso._Descricao;
    }

    public void Fechar()
    {
        gameObject.SetActive(false);
    }
}
