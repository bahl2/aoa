using UnityEngine;

namespace Assets.Scripts.Global
{
    public class SegueObjeto : MonoBehaviour
    {
        [SerializeField]
        private float _Velocidade;
        public Vector2 _LimiteMax;
        public Vector2 _LimiteMin;
        public Transform _Alvo;
        public bool _Limita;

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
            if (_Alvo != null)
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
    }
}
