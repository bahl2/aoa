using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class ControleAudio : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
