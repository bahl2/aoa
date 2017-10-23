using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class ControleAudio : MonoBehaviour
    {
        public static AudioClip _Alerta;
        public static AudioClip _AlertaGrave;
        public static AudioClip _Btn;
        public static AudioClip _Btn2;
        public static AudioClip _AlertaAtiva;
        public static AudioClip _Passo;
        public static AudioClip _Passos;
        public static AudioClip _HitInimigo;
        public static AudioClip _HitJogador;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _Alerta = Resources.Load<AudioClip>("Audio/Interface/AlertSimple");
            _AlertaGrave = Resources.Load<AudioClip>("Audio/Interface/AlertSimple2Grave");
            _Btn = Resources.Load<AudioClip>("Audio/Interface/BTN-Hover");
            _Btn2 = Resources.Load<AudioClip>("Audio/Interface/BTN-Select");
            _AlertaAtiva = Resources.Load<AudioClip>("Audio/Interface/GetActiveAlert");
            _Passo = Resources.Load<AudioClip>("Audio/Jogador Actions/Passo");
            _Passos = Resources.Load<AudioClip>("Audio/Jogador Actions/PassoDuploSequencia");
            _HitInimigo = Resources.Load<AudioClip>("Audio/Jogador Danos/Hit-Dano-Inimigo");
            _HitJogador = Resources.Load<AudioClip>("Audio/Jogador Danos/Hit-Dano-Jogador");
        }
    }
}
