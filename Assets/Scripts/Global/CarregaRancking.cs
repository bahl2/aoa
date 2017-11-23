using ArcadePUCCampinas;
using Assets.Scripts.Inimigos;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Global
{
    public class CarregaRancking : MonoBehaviour
    {
        [SerializeField]
        private Text _Caveiras;
        [SerializeField]
        private Text _Dementadores;
        [SerializeField]
        private Text _Tempo;
        public Text _Titulo;
        public Text _Sair;

        private void OnEnable()
        {
            Time.timeScale = 0;
            int lQtdCaveiras = FindObjectsOfType<ControleCaveira>().Length;
            int lQtdDementadores = FindObjectsOfType<ControleDementador>().Length;
            _Caveiras.text = string.Format("{0}/{1}", ControleFase._Caveiras - lQtdCaveiras, ControleFase._Caveiras);
            _Dementadores.text = string.Format("{0}/{1}", ControleFase._Dementadores - lQtdDementadores, ControleFase._Dementadores);
            _Tempo.text = FindObjectOfType<TimeTrial>()._Contador.text;
            Cursor.visible = CFG._Plataforma == CFG.EPlataforma.PC;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void Sair()
        {
            Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
        }

        private void Update()
        {
            if (InputArcade.Apertou(0, EControle.VERDE))
                Sair();
        }
    }
}
