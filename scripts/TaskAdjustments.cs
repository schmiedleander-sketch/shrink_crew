using UnityEngine;

public class TaskAdjustments : MonoBehaviour
{
    [Header("Task Settings")]
    public float baseTaskTime = 5f;
    public float currentTaskTime;

    [Header("Interaction")]
    public float baseInteractionRange = 2f;
    public float currentInteractionRange;

    private PlayerSize playerSize;

    void Start()
    {
        playerSize = GetComponent<PlayerSize>();
        UpdateTaskModifiers();
    }

    void Update()
    {
        UpdateTaskModifiers();
    }

    private void UpdateTaskModifiers()
    {
        if (playerSize == null) return;

        float size = playerSize.currentSize;

        // Smaller players complete tasks faster
        float speedBonus = 1f - size;
        currentTaskTime = baseTaskTime - (speedBonus * 2f);

        // Smaller players have shorter reach
        currentInteractionRange = baseInteractionRange * size;

        currentTaskTime = Mathf.Clamp(currentTaskTime, 1f, baseTaskTime);
    }

    public void PerformTask()
    {
        Debug.Log($"Task will take {currentTaskTime} seconds.");
        // Hook this into your task system timer
    }

    public bool CanInteract(Vector3 taskPosition)
    {
        float distance = Vector3.Distance(transform.position, taskPosition);
        return distance <= currentInteractionRange;
    }
}