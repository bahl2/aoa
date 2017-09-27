using UnityEngine;

public class CarregaMascara : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _Mascara;

    private void OnEnable()
    {
        _Mascara.gameObject.SetActive(gameObject.activeSelf);
    }

    private void OnDisable()
    {
        if (_Mascara)
            _Mascara.gameObject.SetActive(gameObject.activeSelf);
    }
}
