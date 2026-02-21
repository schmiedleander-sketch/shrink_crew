using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    public float currentSize = 1f; // 1 = normal size
    public float minSize = 0.3f;

    public void Shrink(float percent)
    {
        currentSize -= percent;
        if(currentSize < minSize) currentSize = minSize;

        transform.localScale = Vector3.one * currentSize;
        // Play shrink animation
        // Anim.Play("ShrinkAnim");
    }

    public bool IsSmallest(float threshold)
    {
        return currentSize <= threshold;
    }
}