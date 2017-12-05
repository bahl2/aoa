using Assets.Scripts.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    [Serializable]
    public class Ranking
    {
        public string Id, nome, tempo, dementadores, esqueletos;
    }

    public class RankingList
    {
        public List<Ranking> _Ranking;
        public IEnumerator ListRanking()
        {
            WWW www = new WWW(GameTags.ServerHost() + "ranking/listRankingOrdenado");
            yield return www;
            if (www.error == null)
            {
                RankingData lRankingData = JsonUtility.FromJson<RankingData>(www.text);
                _Ranking = new List<Ranking>();
                foreach (Ranking lRanking in lRankingData.data)
                {
                    _Ranking.Add(lRanking);
                    Debug.Log(lRanking.Id + " - " + lRanking.nome + " - Tempo: " + lRanking.tempo + " - Dementadores: " +
                        lRanking.dementadores + " - Esqueletos: " + lRanking.esqueletos);
                }
            }
        }

        public IEnumerator SaveRanking(Ranking pRanking)
        {
            WWWForm form = new WWWForm();
            form.AddField("nome", pRanking.nome);
            form.AddField("tempo", pRanking.tempo);
            form.AddField("dementadores", pRanking.dementadores);
            form.AddField("esqueletos", pRanking.esqueletos);
            WWW www = new WWW(GameTags.ServerHost() + "ranking/add", form);
            yield return www;
            if (www.error == null)
                Debug.Log("ranking inserido!");
            else
                Debug.Log("Erro: " + www.error);
        }

    }

    public class RankingData
    {
        public List<Ranking> data;
    }
}
