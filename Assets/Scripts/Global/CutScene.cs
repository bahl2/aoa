using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public static GameTags.ECenas ProximaCena
    {
        set
        {
            _ProximaCena = value;
            SceneManager.LoadScene(GameTags.Cenas[(int)GameTags.ECenas.CutScene]);
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

    [SerializeField]
    private AudioSource _Audio;
    [SerializeField]
    private Text _Texto;
    private static GameTags.ECenas _ProximaCena;
    private static float _Segundos;
    private static VideoClip _Video;    
    private VideoPlayer _VideoPlayer;

    private void Start()
    {
        if (_Segundos == 0)
        {
            _Segundos = Convert.ToSingle(_Video.length);
        }
        _VideoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(CarregaVideo());
        _Texto.CrossFadeAlpha(0, 5, true);
    }

    private void Update()
    {
        if (InputArcade.Apertou(0,EControle.VERDE))
           SceneManager.LoadScene(GameTags.Cenas[(int)_ProximaCena]);
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
        SceneManager.LoadScene(GameTags.Cenas[(int)_ProximaCena]);
    }
}
