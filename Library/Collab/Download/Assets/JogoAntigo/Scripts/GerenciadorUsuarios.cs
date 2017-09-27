using Assets.Scripts.Objetos;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorUsuarios : MonoBehaviour
{
    public Usuarios _UsuarioLogado;
    private static GerenciadorUsuarios _Instancia;
    public static GerenciadorUsuarios Instancia//singueton para guardar o usuario logado que vai ser utilizado varias vezes no jogo
    {
        get
        {
            if (!_Instancia)
            {
                GameObject GP = new GameObject("GerenciadorUsuarios");
                GP.AddComponent<GerenciadorUsuarios>();
            }
            return _Instancia;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _Instancia = this;
    }

    private void Update()
    {
        StartCoroutine(UpdateUltimoAcesso());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Login");
            Destroy(gameObject);
        }
    }

    private IEnumerator UpdateUltimoAcesso()
    {
        _UsuarioLogado._UltimoAcesso = DateTime.Now;
        yield return StartCoroutine(_UsuarioLogado.UpdateById());
    }
}
