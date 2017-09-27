using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;

public class FonteRecursos : UnidadeBase
{
    public TiposRecursos _TipoRecurso;
    public int _RecursosSegundo;
    public int _ChaveFonteRecursos;
    public int _IdTipoRecurso;
    public FontesRecursos _FonteRecursos;
    public RlcFontesRecursosExercitos[] _Mineradores;
    //public AcoesFonteRecursos gAcoes;
    private int _MineradoresAtivos;
    private Canvas _Canvas;

    public void CarregaRecusos()
    {
        bool lCheia = _FonteRecursos._SegundosColetagem < _FonteRecursos._SegundosDuracao;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "Recurso")
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = lCheia ? _TipoRecurso._Cheio.GetComponent<SpriteRenderer>().sprite :
                  _TipoRecurso._Vazio.GetComponent<SpriteRenderer>().sprite;//se estiver cheia ele carrega a imagem correta            
            }
        }
    }

    internal override void Start()
    {
        base.Start();
        _TipoRecurso = GerenciadorTiposRecursos.Instancia.ReadById(_IdTipoRecurso);
        /*gAcoes.gFonteRecursos = this;
        gCanvas = gAcoes.GetComponent<Canvas>();*/
        StartCoroutine(CarregaMineradores());
    }

    public IEnumerator CarregaMineradores()
    {
        RlcFontesRecursosExercitos lRlcFontesRecursosExercitos = new RlcFontesRecursosExercitos();
        yield return StartCoroutine(lRlcFontesRecursosExercitos.ReadByIdFontesRecursos(_FonteRecursos._IdFontesRecursos));
        if (lRlcFontesRecursosExercitos.Retorno)
        {
            _Mineradores = lRlcFontesRecursosExercitos.Registros.ToArray();
        }
    }

    private IEnumerator Coletar()
    {
        switch (_TipoRecurso._IdTiposRecursos)
        {
            case 1:
                GerenciadorUsuarios.Instancia._UsuarioLogado._Cristal += _FonteRecursos._RecursosColetados;
                break;
            case 2:
                GerenciadorUsuarios.Instancia._UsuarioLogado._Madeira += _FonteRecursos._RecursosColetados;
                break;
        }
        _FonteRecursos._RecursosColetados = 0;
        yield return StartCoroutine(GerenciadorUsuarios.Instancia._UsuarioLogado.UpdateById());
        if (GerenciadorUsuarios.Instancia._UsuarioLogado.Retorno)
            yield return StartCoroutine(_FonteRecursos.UpdateById());
        /*if (gFonteRecursos.Retorno)
            yield return StartCoroutine(gAcoes.gAcoes.AtualizaRecursos());*/
    }

    public IEnumerator Minerar()
    {
        while (_FonteRecursos._SegundosColetagem < _FonteRecursos._SegundosDuracao && _FonteRecursos._Ativa)
        {
            if (_FonteRecursos._Mineradores > 0)
            {
                _FonteRecursos._RecursosColetados += (int)((_RecursosSegundo * _FonteRecursos._SegundosColetagem) * _FonteRecursos._Mineradores);//calcula o valo de recursos por segundo                
                _FonteRecursos._SegundosColetagem++;
                yield return new WaitForSecondsRealtime(1);
                yield return StartCoroutine(_FonteRecursos.UpdateById());
            }
        }
        _FonteRecursos._Ativa = false;
        yield return StartCoroutine(_FonteRecursos.UpdateById());
        CarregaRecusos();
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if (_FonteRecursos._Mineradores > 0)
        {
            StartCoroutine(Reconstruir());
        }
    }

    public IEnumerator Reconstruir()
    {
        yield return StartCoroutine(Coletar());
        _FonteRecursos._RecursosColetados = 0;
        _FonteRecursos._SegundosColetagem = 0;
        _FonteRecursos._Ativa = _FonteRecursos._Mineradores > 0;
        yield return StartCoroutine(_FonteRecursos.UpdateById());
        CarregaRecusos();
        if (_FonteRecursos.Retorno)
            StartCoroutine(Minerar());
    }

    internal override void CarregaAcoes()
    {
        base.CarregaAcoes();
        /*if (!gAcoes.gPainelAcoes.gameObject.activeSelf && gCanvas != null)
        {
            Vector2 lPosAcoes = (Input.mousePosition - gCanvas.transform.localPosition);
            gAcoes.gPainelAcoes.localPosition = lPosAcoes;
        }
        gAcoes.gPainelAcoes.gameObject.SetActive(Marcada);*/
    }
}
