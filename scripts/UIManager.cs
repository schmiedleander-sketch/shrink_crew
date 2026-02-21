using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI Prefabs")]
    public GameObject shrinkBarPrefab;      // Prefab for shrink level bar
    public GameObject crownIconPrefab;      // Prefab for smallest-win icon
    public GameObject stompCooldownPrefab;  // Prefab for Impostor stomp cooldown

    private Dictionary<PlayerSize, GameObject> shrinkBars = new Dictionary<PlayerSize, GameObject>();
    private Dictionary<PlayerSize, GameObject> crownIcons = new Dictionary<PlayerSize, GameObject>();
    private Dictionary<PlayerSize, GameObject> stompCooldowns = new Dictionary<PlayerSize, GameObject>();

    void Start()
    {
        // Initialize UI for all players
        PlayerSize[] players = FindObjectsOfType<PlayerSize>();
        foreach(var player in players)
        {
            // Shrink Bar
            GameObject bar = Instantiate(shrinkBarPrefab, transform);
            shrinkBars[player] = bar;

            // Crown Icon
            GameObject crown = Instantiate(crownIconPrefab, transform);
            crown.SetActive(false);
            crownIcons[player] = crown;

            // Stomp Cooldown (Impostor Only)
            StompAbility stomp = player.GetComponent<StompAbility>();
            if(stomp != null)
            {
                GameObject cd = Instantiate(stompCooldownPrefab, transform);
                stompCooldowns[player] = cd;
            }
        }
    }

    void Update()
    {
        UpdateShrinkBars();
        UpdateCrowns();
        UpdateStompCooldowns();
    }

    void UpdateShrinkBars()
    {
        foreach(var kvp in shrinkBars)
        {
            PlayerSize player = kvp.Key;
            GameObject bar = kvp.Value;

            // Position bar above player
            bar.transform.position = player.transform.position + Vector3.up * 1.5f;

            // Update fill amount based on size
            Image fill = bar.GetComponentInChildren<Image>();
            fill.fillAmount = (player.currentSize - player.minSize) / (1f - player.minSize);
        }
    }

    void UpdateCrowns()
    {
        // Find smallest alive player(s)
        PlayerSize[] alivePlayers = System.Array.FindAll(FindObjectsOfType<PlayerSize>(), p => p.gameObject.activeSelf);
        if(alivePlayers.Length == 0) return;

        float minSize = Mathf.Min(System.Array.ConvertAll(alivePlayers, p => p.currentSize));
        foreach(var player in alivePlayers)
        {
            GameObject crown = crownIcons[player];
            crown.SetActive(player.currentSize == minSize && player.currentSize <= 0.3f);
            crown.transform.position = player.transform.position + Vector3.up * 2f;
        }
    }

    void UpdateStompCooldowns()
    {
        foreach(var kvp in stompCooldowns)
        {
            PlayerSize player = kvp.Key;
            GameObject cdUI = kvp.Value;
            StompAbility stomp = player.GetComponent<StompAbility>();
            if(stomp == null) continue;

            cdUI.transform.position = player.transform.position + Vector3.up * 1.2f;
            Image fill = cdUI.GetComponentInChildren<Image>();
            fill.fillAmount = Mathf.Clamp01(stomp.cooldownTimer / stomp.stompCooldown);
        }
    }
}