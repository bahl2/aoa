using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;

public class Acoes : MonoBehaviour
{
    private GerenciadorConstrucoes _GerenciadorConstrucoes;
    private AcoesConstrucoes _AcoesConstrucoes;
    private AcoesFonteRecursos _AcoesFonteRecursos;
    public GerenciadorUI _GerenciadorUi;

    private void Awake()
    {
        _GerenciadorUi = FindObjectOfType<GerenciadorUI>();
        _GerenciadorConstrucoes = FindObjectOfType<GerenciadorConstrucoes>();
        _AcoesConstrucoes = FindObjectOfType<AcoesConstrucoes>();
        _AcoesFonteRecursos = FindObjectOfType<AcoesFonteRecursos>();
    }

    private void Start()
    {
        _GerenciadorConstrucoes.Fechar();
        _AcoesFonteRecursos.Fechar();
        _AcoesConstrucoes.Fechar();
    }

    public IEnumerator AtualizaRecursos()
    {
        int lAldeoes = 0;
        int lAldeoesDesocupados = 0;
        Exercitos lExercitos = new Exercitos();
        lExercitos._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
        lExercitos._IdTiposUnidadesMoveis = 1;
        yield return StartCoroutine(lExercitos.ReadByIdUsuario());
        if (lExercitos.Retorno)
        {
            lAldeoes = lExercitos.Registros.Count;
        }
        yield return StartCoroutine(lExercitos.ReadByIdUsuario(false));
        if (lExercitos.Retorno)
        {
            lAldeoesDesocupados = lExercitos.Registros.Count;
        }
        _GerenciadorUi.Aldeoes = lAldeoes;
        _GerenciadorUi.AldeoesDesocupados = lAldeoesDesocupados;
        _GerenciadorUi.Cristais = GerenciadorUsuarios.Instancia._UsuarioLogado._Cristal;
        _GerenciadorUi.Madeira = GerenciadorUsuarios.Instancia._UsuarioLogado._Madeira;
        _GerenciadorUi.Nivel = GerenciadorUsuarios.Instancia._UsuarioLogado._Nivel;
    }

    public void AbrirGerenciadorConstrucoes()
    {
        _GerenciadorConstrucoes.gameObject.SetActive(true);
    }

    /*public void Construir(int pIdTipoConstrucao)
    {
        TiposConstrucoes lTipoConstrucao = GerenciadorTiposContrucoes.Instancia.ReadById(pIdTipoConstrucao);
        bool lTemAldeoes = _GerenciadorUi.AldeoesDesocupados >= lTipoConstrucao._Aldeoes;
        bool lTemCristal = _GerenciadorUi.Cristal >= lTipoConstrucao._Cristais;
        bool lTemMadeira = _GerenciadorUi.Madeira >= lTipoConstrucao._Madeira;
        if (lTemAldeoes && lTemCristal && lTemMadeira)
        {
            GerenciadorConstrucoes(false);
            Construcoes lConstrucoes = new Construcoes();
            lConstrucoes._IdTiposConstrucoes = pIdTipoConstrucao;
            lConstrucoes._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
            lConstrucoes._Nivel = 1;
            StartCoroutine(lConstrucoes.Create());
            Transform lPrefabConstrucao = Instantiate(lTipoConstrucao._Prefab);
            Construcao lConstrucao = lPrefabConstrucao.GetComponent<Construcao>();
            StartCoroutine(CalculaValor(-lTipoConstrucao._Cristais, -lTipoConstrucao._Madeira));
            StartCoroutine(lConstrucao._Acoes.CarregarConstrutoresContrucao(lConstrucao._Construcao, lTipoConstrucao._Aldeoes));
        }
    }*/

    public IEnumerator CalculaValor(int pCristal, int pMadeira)
    {
        GerenciadorUsuarios.Instancia._UsuarioLogado._Cristal += pCristal;
        GerenciadorUsuarios.Instancia._UsuarioLogado._Madeira += pMadeira;
        yield return StartCoroutine(GerenciadorUsuarios.Instancia._UsuarioLogado.UpdateById());
    }
}
