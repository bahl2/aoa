using UnityEngine;
using UnityEngine.UI;

public class GerenciadorUI : MonoBehaviour
{
    [SerializeField]
    private Text _Aldeoes;
    [SerializeField]
    private Text _Madeira;
    [SerializeField]
    private Text _Cristais;
    [SerializeField]
    private Text _Nivel;
    private int _QtdAldeoesDesocupados;
    private int _QtdAldeoes;

    public int Aldeoes
    {
        get { return _QtdAldeoes; }
        set
        {
            _QtdAldeoes = value;
            _Aldeoes.text = string.Format("{0}/{1}", AldeoesDesocupados, _QtdAldeoes);
        }
    }
    public int AldeoesDesocupados
    {
        get { return _QtdAldeoesDesocupados; }
        set
        {
            _QtdAldeoesDesocupados = value;
            _Aldeoes.text = string.Format("{0}/{1}", AldeoesDesocupados, _QtdAldeoes);
        }
    }
    public int Madeira { get { return int.Parse(_Madeira.text); } set { _Madeira.text = value.ToString(); } }
    public int Cristais { get { return int.Parse(_Cristais.text); } set { _Cristais.text = value.ToString(); } }
    public int Nivel { get { return int.Parse(_Nivel.text); } set { _Nivel.text = value.ToString(); } }
}
