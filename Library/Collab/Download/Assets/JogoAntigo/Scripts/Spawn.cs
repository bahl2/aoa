using Assets.Scripts.Objetos;
using System;
using System.Collections;
using UnityEngine;

//usei namespace pois carrega menos o jogo com classes desnecessarias aqui por exemplo se não tivesse isso a classe de contruções tambem seria carregada e isso em um jogo grande calsa lentidao

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _Loading;
    private TimeSpan _DiferencaUltimoAcesso;
    private Acoes _Acoes;

    private void Start()
    {
        _DiferencaUltimoAcesso = DateTime.Now.Subtract(GerenciadorUsuarios.Instancia._UsuarioLogado._UltimoAcesso);
        _Acoes = FindObjectOfType<Acoes>();
        StartCoroutine(CarregaInformacoesUsuario());
    }

    private IEnumerator CarregaInformacoesUsuario()
    {
        _Loading.SetActive(true);
        yield return StartCoroutine(CarregaConstrucoesUsuarioLogado());
        yield return StartCoroutine(CarregaFontesRecursos());
        yield return StartCoroutine(_Acoes.AtualizaRecursos());
        _Loading.SetActive(false);
    }

    private IEnumerator CarregaConstrucoesUsuarioLogado()
    {
        Construcoes lConstrucoes = new Construcoes();
        yield return StartCoroutine(lConstrucoes.ReadByIdUsuario(GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios));
        if (lConstrucoes.Retorno)
        {
            foreach (Construcoes lConstrucaoAtual in lConstrucoes.Registros)
            {
                TiposConstrucoes lTipoConstrucaoAtual = GerenciadorTiposContrucoes.Instancia.ReadById(lConstrucaoAtual._IdTiposConstrucoes);
                Vector3 lPosicao = Camera.main.transform.position;
                lPosicao.z = 0;
                if (lConstrucaoAtual._Posicionada)
                {
                    lPosicao = lConstrucaoAtual.Posicao;
                    float lTotalSegundosConstruindo = Convert.ToSingle(_DiferencaUltimoAcesso.TotalSeconds);
                    if (lTotalSegundosConstruindo > lTipoConstrucaoAtual._Segundos)
                        lTotalSegundosConstruindo = lTipoConstrucaoAtual._Segundos;
                    if (lTotalSegundosConstruindo > 0)
                        lConstrucaoAtual._SegundosConstruindo = lTotalSegundosConstruindo;
                    yield return StartCoroutine(lConstrucaoAtual.UpdateById());
                }
                Transform lPrefab = Instantiate(lTipoConstrucaoAtual._Prefab, lPosicao, transform.rotation);
                Construcao lConstrucao = lPrefab.GetComponent<Construcao>();
                lConstrucao._Construcao = lConstrucaoAtual;
                /*if (lConstrucaoAtual.gSegundosConstruindo >= lTipoConstrucaoAtual.gSegundosConstruir)
                {
                    yield return StartCoroutine(lConstrucao.gAcoes.CarregaConstrutoresContrucao(lConstrucaoAtual, -lTipoConstrucaoAtual.gAldeoes));
                }*/
            }
        }
    }

    private IEnumerator CarregaFontesRecursos()
    {
        FonteRecursos[] lFontesRecursosMapa = FindObjectsOfType<FonteRecursos>();
        for (int i = 0; i < lFontesRecursosMapa.Length; i++)
        {
            FontesRecursos lFontesRecursos = new FontesRecursos();
            lFontesRecursos._IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios;
            lFontesRecursos._Chave = lFontesRecursosMapa[i]._ChaveFonteRecursos;
            yield return StartCoroutine(lFontesRecursos.ReadByIdUsuario());
            bool lGravar = false;
            if (lFontesRecursos.Retorno)
            {
                if (lFontesRecursos.Registros.Count == 1)
                {
                    if (GerenciadorUsuarios.Instancia._UsuarioLogado._UltimoAcesso > DateTime.MinValue)
                    {
                        int lMaximoColetar = (int)(lFontesRecursosMapa[i]._RecursosSegundo * lFontesRecursos._SegundosDuracao) * lFontesRecursos._Mineradores;
                        int lTotalRecursosColetados = (int)((lFontesRecursosMapa[i]._RecursosSegundo * _DiferencaUltimoAcesso.TotalSeconds) *
                            lFontesRecursos._Mineradores) + lFontesRecursos._RecursosColetados;
                        if (lTotalRecursosColetados > lMaximoColetar)
                            lTotalRecursosColetados = lMaximoColetar;
                        if (lTotalRecursosColetados > 0)
                            lFontesRecursos._RecursosColetados = lTotalRecursosColetados;
                    }
                    StartCoroutine(lFontesRecursos.UpdateById());
                }
                else
                {
                    lGravar = true;
                }
            }
            else
            {
                lGravar = true;
            }
            if (lGravar)
            {
                lFontesRecursos._IdTiposRecursos = lFontesRecursosMapa[i]._IdTipoRecurso;
                lFontesRecursos._SegundosDuracao = 10;
                lFontesRecursos._Nivel = 1;
                yield return StartCoroutine(lFontesRecursos.Create());
            }
            lFontesRecursosMapa[i]._FonteRecursos = lFontesRecursos;
            lFontesRecursosMapa[i].CarregaRecusos();
            if (lFontesRecursosMapa[i]._FonteRecursos._Ativa)
            {
                StartCoroutine(lFontesRecursosMapa[i].Minerar());
            }
            yield return StartCoroutine(lFontesRecursosMapa[i].CarregaMineradores());
        }
    }

    /*IEnumerator CarregaUsuarios()
    {
        Usuarios lUsuarios = new Usuarios();
        yield return StartCoroutine(lUsuarios.Read());
        if (lUsuarios.Retorno)
        {
            gUsuariosAmigos.options.Clear();
            for (int i = 0; i < lUsuarios.Registros.Count; i++)
            {
                gUsuariosAmigos.options.Add(new Dropdown.OptionData() { text = lUsuarios.Registros[i].gNome });
                if (lUsuarios.Registros[i].gIdUsuarios == GerenciadorUsuarios.Instancia.gUsuarioLogado.gIdUsuarios)
                {
                    gUsuariosAmigos.value = i;
                }
            }
        }
    }

    IEnumerator CarregaMenssagensUsuario()
    {
        Transform lMenssagensRecebidas = gMenssagens.transform.Find("MenssagensRecebidas");
        lMenssagensRecebidas.gameObject.SetActive(gUsuariosAmigos.captionText.text == GerenciadorUsuarios.Instancia.gUsuarioLogado.gNome);
        Transform lEnviarMenssagem = gMenssagens.transform.Find("EnviarMenssagem");
        lEnviarMenssagem.gameObject.SetActive(gUsuariosAmigos.captionText.text == GerenciadorUsuarios.Instancia.gUsuarioLogado.gNome);
        if (gUsuariosAmigos.captionText.text == GerenciadorUsuarios.Instancia.gUsuarioLogado.gNome)
        {
            Menssagens lMenssagens = new Menssagens();
            yield return StartCoroutine(lMenssagens.ReadByIdUsuario(GerenciadorUsuarios.Instancia.gUsuarioLogado.gIdUsuarios));
            if (lMenssagens.Retorno)
            {
                for (int i = 0; i < lMenssagens.Registros.Count; i++)
                {
                    Transform lMenssagemAtual = lMenssagensRecebidas.Find("Caixa").Find("CaixaMenssagens").Find("Menssagem");
                    if (i > 0)
                    {
                        lMenssagemAtual = Instantiate(lMenssagemAtual, lMenssagensRecebidas.Find("Caixa").Find("CaixaMenssagens"));
                    }
                    lMenssagemAtual.GetComponent<Text>().text = lMenssagens.Registros[i].gMenssagem;
                }
            }
        }
    }

    IEnumerator EnviarMenssagemUsuario(String pMenssagem)
    {
        Usuarios lUsuarioSelecionado = new Usuarios();
        yield return StartCoroutine(lUsuarioSelecionado.ReadByNome(gUsuariosAmigos.captionText.text));
        if (lUsuarioSelecionado.Retorno)
        {
            Menssagens lMenssagens = new Menssagens();
            lMenssagens.gIdUsuarios = lUsuarioSelecionado.gIdUsuarios;
            lMenssagens.gMenssagem = pMenssagem;
            yield return StartCoroutine(lMenssagens.Create());
        }
    }

    public void CarregaFaccao()
    {
        StartCoroutine(CarregaUsuarios());
    }

    public void FecharMenssagens()
    {
        gMenssagens.SetActive(false);
    }

    public void CarregaMenssagens()
    {
        StartCoroutine(CarregaMenssagensUsuario());
    }

    public void EnviarMenssagem(InputField Menssagem)
    {
        StartCoroutine(EnviarMenssagemUsuario(Menssagem.text));
    }*/
}
