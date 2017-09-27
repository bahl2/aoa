using UnityEngine;

public class SegueObjeto : MonoBehaviour
{
    public Transform _Alvo;
    [SerializeField]
    private float _Velocidade;
    public bool _Limita;
    [SerializeField]
    private Vector2 _LimiteMax;
    [SerializeField]
    private Vector2 _LimiteMin;

    private void Start()
    {
        transform.position = new Vector3(_LimiteMin.x, _LimiteMax.y, transform.position.z);
    }

    private void Update()
    {
        Seguir();
    }

    private void Seguir()
    {
        Vector3 lPosAlvo = _Alvo.position;
        lPosAlvo.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, lPosAlvo, _Velocidade);
        Vector3 lPosTransform = transform.position;
        if (_Limita)
            transform.position = new Vector3(Mathf.Clamp(lPosTransform.x, _LimiteMin.x, _LimiteMax.x),
                Mathf.Clamp(lPosTransform.y, _LimiteMin.y, _LimiteMax.y), lPosTransform.z);
    }
}
