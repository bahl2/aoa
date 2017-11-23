using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Rancking
    {
        public int _Colocado;
        public string _Nome;
        public int _Mortes;
        public int _Tempo;

        public List<Rancking> Load()
        {
            List<Rancking> lRancking = new List<Rancking>();
            StreamReader lArquivo = File.OpenText(string.Format("{0}/Ranking.json", Application.dataPath));
            Rancking lJogador = JsonUtility.FromJson<Rancking>(lArquivo.Read().ToString());
            lRancking.Add(lJogador);
            lArquivo.Close();
            return lRancking;
        }
    }
}
