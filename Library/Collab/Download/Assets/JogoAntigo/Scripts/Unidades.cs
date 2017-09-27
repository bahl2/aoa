using UnityEngine;

public class Unidades : MonoBehaviour
{
    public static void Desmarcar()
    {
        UnidadeBase[] lUnidades = FindObjectsOfType<UnidadeBase>();
        foreach (UnidadeBase lUnidade in lUnidades)
        {
            lUnidade.Marcada = false;
        }
    }

    public static void Marcar(UnidadeBase pMarcar)
    {
        Desmarcar();
        pMarcar.Marcada = true;
    }
}
