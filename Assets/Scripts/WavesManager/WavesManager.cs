using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform[] WavesParents;
    [SerializeField] private List<LookForPlayer> lookForPlayers;
    [SerializeField] private List<List<Transform>> Waves = new List<List<Transform>>();
    public int waveIndex = 0;
    private float timer;
    [SerializeField] private float amountToRaiseEnemiesDis;
    [SerializeField] private float timeToMakeDistanceLonger;
    void Start()
    {
        for (int i = 0; i < WavesParents.Length; i++)
        {
            Waves.Add(new List<Transform>());
            for (int j = 0; j < WavesParents[i].childCount; j++)
            {
                Waves[i].Add(WavesParents[i].GetChild(j));
            }
        }

        GetAllLookForPlayer();
    }

    void MakeDistanceLonger()
    {
        timer += Time.deltaTime;
        if (timer >= timeToMakeDistanceLonger)
        {
            for (int i = 0; i < lookForPlayers.Count; i++)
            {
                lookForPlayers[i].AggroRange += amountToRaiseEnemiesDis;
            }
            timer = 0;
        }
    }
    void GetAllLookForPlayer()
    {
        if(Waves.Count > waveIndex)
        {
            lookForPlayers = new List<LookForPlayer>();
            for (int i = 0; i < Waves[waveIndex].Count; i++)
            {
                lookForPlayers.Add(Waves[waveIndex][i].gameObject.GetComponent<LookForPlayer>());
            }
        }
    }
    public bool CheckCurrentWave()
    {
        var countBodies = 0;
        if (waveIndex < Waves.Count)
        {
            for (int i = 0; i < Waves[waveIndex].Count; i++)
            {
                if (!Waves[waveIndex][i].gameObject.activeInHierarchy)
                {
                    countBodies++;
                }
            }
            if (countBodies >= Waves[waveIndex].Count)
            {
                waveIndex++;
                GetAllLookForPlayer();
                return true;
            }
        }
        return false;
    }
    void Update()
    {
        MakeDistanceLonger();
    }
}
