using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Login : MonoBehaviour
{
    [SerializeField]
    private InputField _Usuario;
    [SerializeField]
    private InputField _Senha;
    [SerializeField]
    private Text _Menssagem;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//impede que o celular hiberne
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Entrar()
    {
        if (string.IsNullOrEmpty(_Usuario.text.Trim()) || string.IsNullOrEmpty(_Senha.text.Trim()))
        {
            Menssagem("Blank username or password", true);
        }
        else
        {
            StartCoroutine(ValidaLogin());
        }
    }

    private IEnumerator ValidaLogin()
    {
        Usuarios lUsuario = new Usuarios();
        yield return StartCoroutine(lUsuario.ReadByNome(_Usuario.text.Trim()));
        if (lUsuario.Retorno)
        {
            if (lUsuario.Registros.Count == 1)
            {
                if (Criptografia.ComparaMD5(lUsuario._Senha, _Senha.text.Trim()))// trim remove espaços em branco da string
                {
                    GerenciadorUsuarios.Instancia._UsuarioLogado = lUsuario;
                    Menssagem("Login Successful! Loading the game...", false);
                    if (GerenciadorUsuarios.Instancia._UsuarioLogado._IdTiposPovos > 0)
                    {
                        yield return StartCoroutine(CarregaCena("Principal"));
                    }
                    else
                    {
                        yield return StartCoroutine(CarregaCena("Menu Principal"));
                    }
                }
                else
                {
                    Menssagem("Incorrect Password", true);
                }
            }
            else
            {
                Menssagem("User not found", true);
            }
        }
        else
        {
            Menssagem("Error while Login", true);
        }
    }

    public void Registrar()
    {
        if (string.IsNullOrEmpty(_Usuario.text.Trim()) || string.IsNullOrEmpty(_Senha.text.Trim()))
        {
            Menssagem("Blank username or password", true);
        }
        else
        {
            StartCoroutine(ValidarRegistro());
        }
    }

    private IEnumerator CarregaCena(string pCena)
    {
        AsyncOperation lCarregamento = SceneManager.LoadSceneAsync(pCena);//-> carrega a cena
        while (!lCarregamento.isDone)//->enquanto nao estiver carregado aguarda 
        {
            yield return null;
        }
    }

    private IEnumerator ValidarRegistro()
    {
        Usuarios lUsuario = new Usuarios();
        yield return StartCoroutine(lUsuario.ReadByNome(_Usuario.text.Trim()));
        if (lUsuario.Retorno)
        {
            if (lUsuario.Registros.Count > 0)
            {
                Menssagem("User already registered", true);
            }
            else
            {
                lUsuario._Nome = _Usuario.text.Trim();
                lUsuario._Senha = Criptografia.MD5(_Senha.text.Trim());
                yield return StartCoroutine(lUsuario.Create());
                if (lUsuario.Retorno)
                {
                    Menssagem("Registered User Successfully", false);
                }
                else
                {
                    Menssagem("Error registering user", true);
                }
            }
        }

    }

    private void Menssagem(string pMenssagem, bool pErro)
    {
        _Menssagem.CrossFadeAlpha(100, 0, false);
        _Menssagem.color = pErro ? Color.red : Color.blue;
        _Menssagem.text = pMenssagem;
        _Menssagem.CrossFadeAlpha(0, 2, false);
        if (pErro)
        {
            _Usuario.text = "";
            _Senha.text = "";
        }
    }
}
