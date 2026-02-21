using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SmallestWinManager : MonoBehaviour
{
    public float smallestThreshold = 0.3f;

    public void CheckSmallestWin()
    {
        List<PlayerSize> alivePlayers = new List<PlayerSize>(FindObjectsOfType<PlayerSize>().Where(p => p.gameObject.activeSelf));
        if(alivePlayers.Count == 0) return;

        float minSize = alivePlayers.Min(p => p.currentSize);
        var winners = alivePlayers.Where(p => p.currentSize == minSize && minSize <= smallestThreshold).ToList();

        foreach(var player in winners)
        {
            Debug.Log($"{player.name} wins as the smallest!");
            // Anim.Play("TinyCrownAnim");
        }
    }
}