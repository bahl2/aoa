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
        private float _Tempo;
        private bool _CarregandoCena;

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
            if (CFG._Plataforma == CFG.EPlataforma.Arcade)
                _Texto.text = "Verde ->  para pular";
            else
                _Texto.text = "Space ->  para pular";
            _Texto.CrossFadeAlpha(0, 5, true);
            Cursor.visible = false;
            if (_Segundos == 0)
            {
                _Segundos = Convert.ToSingle(_Video.length);
            }
            _VideoPlayer = GetComponent<VideoPlayer>();
            StartCoroutine(CarregaVideo());

            _Progresso.gameObject.SetActive(false);
            _CarregandoCena = false;
        }

        private void Update()
        {
            if (!_CarregandoCena)
            {
                if ((InputArcade.Apertou(0, EControle.VERDE) && Time.timeScale == 1) || _Tempo >= _Segundos)
                {
                    StartCoroutine(Carrega.CarregaCena(GameTags._Cenas[(int)_ProximaCena], _Progresso));
                    _CarregandoCena = true;
                }
            }
            if (Time.timeScale == 1)
            {
                _Tempo += Time.deltaTime;
                if (_Tempo < _Segundos)
                {
                    _VideoPlayer.Play();
                    _Audio.Play();
                }
                else
                {
                    _VideoPlayer.Stop();
                    _Audio.Stop();
                }
            }
            else
            {
                if (_Tempo > 0)
                    _Tempo -= Time.deltaTime;
                _VideoPlayer.Pause();
                _Audio.Pause();
            }
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
        }
    }
}
