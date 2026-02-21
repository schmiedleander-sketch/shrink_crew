using UnityEngine;

public class StompAbility : MonoBehaviour
{
    public float stompRange = 1.5f;
    public float stompCooldown = 15f;
    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.E) && cooldownTimer >= stompCooldown)
        {
            AttemptStomp();
        }
    }

    void AttemptStomp()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, stompRange);
        foreach(var hit in hits)
        {
            PlayerSize target = hit.GetComponent<PlayerSize>();
            if(target != null && target.currentSize <= 0.5f)
            {
                // Eliminates the player
                Debug.Log($"Stomped {target.name}");
                // Anim.Play("StompAnim");
                target.gameObject.SetActive(false);
                cooldownTimer = 0f;
                break;
            }
        }
    }
}