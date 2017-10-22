using ArcadePUCCampinas;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.Global
{
    public class CutScene : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _Audio;
        [SerializeField]
        private Text _Texto;
        [SerializeField]
        private Slider _Progresso;
        private static GameTags.ECenas _ProximaCena;
        private static float _Segundos;
        private static VideoClip _Video;
        private VideoPlayer _VideoPlayer;

        public static GameTags.ECenas ProximaCena
        {
            set
            {
                _ProximaCena = value;
                SceneManager.LoadScene(GameTags._Cenas[(int)GameTags.ECenas.CutScene]);
            }
        }

        public static VideoClip Video
        {
            set
            {
                _Video = value;
            }
        }

        public static float Segundos
        {
            set
            {
                _Segundos = value;
            }
        }

        private void Start()
        {
            if (_Segundos == 0)
            {
                _Segundos = Convert.ToSingle(_Video.length);
            }
            _VideoPlayer = GetComponent<VideoPlayer>();
            StartCoroutine(CarregaVideo());
            _Texto.CrossFadeAlpha(0, 5, true);
            _Progresso.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (InputArcade.Apertou(0, EControle.VERDE))
                StartCoroutine(Carrega.CarregaCena(GameTags._Cenas[(int)_ProximaCena], _Progresso));
        }

        private IEnumerator CarregaVideo()
        {
            _VideoPlayer.clip = _Video;
            _VideoPlayer.EnableAudioTrack(0, true);
            _VideoPlayer.SetTargetAudioSource(0, _Audio);
            _VideoPlayer.Prepare();
            while (!_VideoPlayer.isPrepared)
            {
                yield return null;
            }
            _VideoPlayer.Play();
            _Audio.Play();
            yield return new WaitForSeconds(_Segundos);
            StartCoroutine(Carrega.CarregaCena(GameTags._Cenas[(int)_ProximaCena], _Progresso));
        }
    }
}
