using UnityEngine;

namespace Assets.Scripts.Global
{
    public class TransportaCenario : MonoBehaviour
    {
        [SerializeField]
        private Transform _CenarioDestino;
        [SerializeField]
        private ETransporte _TipoTransporte;
        [SerializeField]
        private Vector2 _LimiteMaxCenario;
        [SerializeField]
        private Vector2 _LimiteMinCenario;
        private Camera _Camera;

        private enum ETransporte
        {
            Ida,
            Volta
        }

        private void Start()
        {
            _Camera = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            pColisao.transform.position = _CenarioDestino.position;
            _Camera.GetComponent<SegueObjeto>()._Limita = false;
            _Camera.GetComponent<SegueObjeto>()._LimiteMax = _LimiteMaxCenario;
            _Camera.GetComponent<SegueObjeto>()._LimiteMin = _LimiteMinCenario;
            _Camera.transform.position = _CenarioDestino.position + new Vector3(0, 0, _Camera.transform.position.z);
            _Camera.GetComponent<SegueObjeto>()._Limita = true;
        }
    }
}
