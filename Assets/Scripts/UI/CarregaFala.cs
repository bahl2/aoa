using Assets.Scripts.Inimigos;
using Assets.Scripts.Jogadores;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CarregaFala : MonoBehaviour
    {
        [SerializeField]
        private string[] _Falas;
        [SerializeField]
        private Inimigo _Espirito;
        private Text _Fala;
        private bool _TextoApareceu;

        private void Start()
        {
            _TextoApareceu = false;
            _Fala = GameObject.FindGameObjectWithTag("Falas").GetComponent<Text>();
        }

        private void OnTriggerEnter2D(Collider2D pColisao)
        {
            Jogador lJogador = pColisao.GetComponent<Jogador>();
            if (!_TextoApareceu && lJogador != null)
            {
                SpriteRenderer lSprite = GetComponent<SpriteRenderer>();
                if (lSprite != null)
                {
                    lSprite.enabled = false;
                }
                if (_Espirito != null)
                {
                    _Espirito._Acao = Global.Personagem.EAcoes.Aparecendo;
                }
                StartCoroutine(TrocaFala());
            }
        }

        private IEnumerator TrocaFala()
        {
            _TextoApareceu = true;
            foreach (string lFala in _Falas)
            {
                _Fala.text = lFala;
                _Fala.CrossFadeAlpha(100, 0, true);
                yield return new WaitForSeconds(5);
                _Fala.CrossFadeAlpha(0, 0, true);
            }
        }
    }
}
