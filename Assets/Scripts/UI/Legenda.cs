using ArcadePUCCampinas;
using UnityEngine;

public class Legenda : MonoBehaviour
{
    private void Update()
    {
        if (InputArcade.Apertou(0, EControle.VERDE))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Personagens[] lPersonagens = FindObjectsOfType<Personagens>();
        foreach (Personagens lPersonagem in lPersonagens)
        {
            lPersonagem.Ativo = false;
        }
    }

    private void OnDisable()
    {
        Personagens[] lPersonagens = FindObjectsOfType<Personagens>();
        foreach (Personagens lPersonagem in lPersonagens)
        {
            lPersonagem.Ativo = true;
        }
    }
}
