using ArcadePUCCampinas;
using Assets.Scripts.Global;
using Assets.Scripts.Inimigos;
using Assets.Scripts.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CarregaRanking : MonoBehaviour
    {
        [SerializeField]
        private Text _Caveiras;
        [SerializeField]
        private Text _Dementadores;
        [SerializeField]
        private Text _Tempo;
        [SerializeField]
        private Text _Nomes;
        [SerializeField]
        private InputField _SalvaNome;
        [SerializeField]
        private GameObject _Save;
        [SerializeField]
        private GameObject _Ranking;
        private int _QtdEsqueletos;
        private int _QtdDementadores;

        private void OnEnable()
        {
            if (ControleFase._FimJogo)
            {
                _Ranking.SetActive(false);
                _Save.SetActive(true);
                Time.timeScale = 0;
                _QtdEsqueletos = ControleFase._Caveiras - FindObjectsOfType<ControleCaveira>().Length;
                _QtdDementadores = ControleFase._Dementadores - FindObjectsOfType<ControleDementador>().Length;
                Cursor.visible = CFG._Plataforma == CFG.EPlataforma.PC;
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
            {
                if (_Ranking.activeSelf)
                {
                    if (InputArcade.Apertou(0, EControle.VERDE))
                        Sair();
                }
                else
                {
                    if (InputArcade.Apertou(0, EControle.VERDE))
                    {
                        Salvar();
                    }
                }
            }
        }

        private IEnumerator ListaRanking()
        {
            RankingList lRankingList = new RankingList();
            yield return StartCoroutine(lRankingList.ListRanking());
            _Save.SetActive(false);
            _Ranking.SetActive(true);
            _Nomes.text = string.Empty;
            _Caveiras.text = string.Empty;
            _Dementadores.text = string.Empty;
            _Tempo.text = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                Ranking lRanking = lRankingList._Ranking[i];
                _Nomes.text += string.Format("{0} - {1}\n", i + 1, lRanking.nome.Substring(0, 5));
                _Caveiras.text += lRanking.esqueletos + "\n";
                _Dementadores.text += lRanking.dementadores + "\n";
                _Tempo.text += lRanking.tempo + "\n";
            }
        }

        private IEnumerator SavarRanking()
        {
            RankingList lRankingList = new RankingList();
            Ranking lRanking = new Ranking
            {
                nome = _SalvaNome.text,
                tempo = FindObjectOfType<TimeTrial>()._Contador.text,
                dementadores = _QtdDementadores.ToString(),
                esqueletos = _QtdEsqueletos.ToString()
            };
            yield return StartCoroutine(lRankingList.SaveRanking(lRanking));
            StartCoroutine(ListaRanking());
        }

        public void Sair()
        {
            Carrega.Cena = GameTags._Cenas[(int)GameTags.ECenas.MenuPrincipal];
        }

        public void Salvar()
        {
            StartCoroutine(SavarRanking());
        }
    }
}
