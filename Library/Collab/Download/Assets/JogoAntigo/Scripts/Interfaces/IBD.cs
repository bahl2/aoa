using System.Collections;

namespace Assets.Scripts.Interfaces
{
    interface IBD
    {
        IEnumerator Create();
        IEnumerator Read(string pFiltro = "", string pOrdem = "");
        IEnumerator Update(string pFiltro = "");
        IEnumerator Delete(string pFiltro = "");
    }
}
