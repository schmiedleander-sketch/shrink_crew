using UnityEngine;
using System.Collections.Generic;

public class ShrinkManager : MonoBehaviour
{
    public float shrinkInterval = 30f; // seconds
    public float shrinkPercent = 0.1f; // 10% shrink each interval

    private float timer = 0f;
    private List<PlayerSize> players;

    void Start()
    {
        players = new List<PlayerSize>(FindObjectsOfType<PlayerSize>());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= shrinkInterval)
        {
            ShrinkPlayers();
            timer = 0f;
        }
    }

    void ShrinkPlayers()
    {
        foreach(var player in players)
        {
            player.Shrink(shrinkPercent);
        }
    }
}