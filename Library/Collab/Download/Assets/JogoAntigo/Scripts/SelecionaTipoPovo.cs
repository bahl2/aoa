using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecionaTipoPovo : MonoBehaviour
{
    private IEnumerator CarregaCena()// é usado para fazer a ceno de loading
    {
        AsyncOperation lCarregamento = SceneManager.LoadSceneAsync("Principal");//-> carrega a cena
        while (!lCarregamento.isDone)//->enquanto nao estiver carregado aguarda 
        {
            yield return null;
        }
    }

    public void Jogar(GameObject pTipoPovo)
    {
        StartCoroutine(ValidaTipoPovo(pTipoPovo));
    }

    private IEnumerator ValidaTipoPovo(GameObject pTipoPovo)
    {
        GerenciadorUsuarios.Instancia._UsuarioLogado._IdTiposPovos = GerenciadorTiposPovos.Instancia.ReadByDescricao(pTipoPovo.name)._IdTiposPovos;
        GerenciadorUsuarios.Instancia._UsuarioLogado._Nivel = 1;
        GerenciadorUsuarios.Instancia._UsuarioLogado._Cristal = 5;
        GerenciadorUsuarios.Instancia._UsuarioLogado._Madeira = 10;
        yield return StartCoroutine(GerenciadorUsuarios.Instancia._UsuarioLogado.UpdateById());
        if (GerenciadorUsuarios.Instancia._UsuarioLogado.Retorno)
        {
            print("Povo Salvo com Sucesso");
            Exercitos lExercitos = new Exercitos()
            {
                _IdTiposUnidadesMoveis = 1,
                _IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios,
                _Ocupado = true,
                _Nivel = 1
            };
            yield return StartCoroutine(lExercitos.Create());
            if (lExercitos.Retorno)
            {
                print("Exercito Salvo com Sucesso");
                Construcoes lConstrucoes = new Construcoes()
                {
                    _IdTiposConstrucoes = 1,
                    _IdUsuarios = GerenciadorUsuarios.Instancia._UsuarioLogado._IdUsuarios,
                    _Nivel = 1,
                    _SegundosConstruindo = 0,
                    _Posicionada = false,
                    _Vida = 100
                };
                yield return StartCoroutine(lConstrucoes.Create());
                if (lConstrucoes.Retorno)
                {
                    print("Construção Salvo com Sucesso");
                    RlcConstrucoesExercitos lRlcConstrucoesExercitos = new RlcConstrucoesExercitos()
                    {
                        _IdConstrucoes = lConstrucoes._IdConstrucoes,
                        _IdExercitos = lExercitos._IdExercitos
                    };
                    yield return StartCoroutine(lRlcConstrucoesExercitos.Create());
                    yield return StartCoroutine(CarregaCena());
                }
                else
                {
                    print("Erro ao Salvar Construção");
                }
            }
            else
            {
                print("Erro ao Salvar Exercito");
            }
        }
        else
        {
            print("Erro ao Slavar Povo");
        }
    }
}
