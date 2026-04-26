using UnityEngine;

[ExecuteAlways]
public class LaserGoal : MonoBehaviour
{
    // Assign these in the Inspector
    public Sprite lasergoal_0;
    public Sprite lasergoal_1;

    private SpriteRenderer spriteRenderer;
    public bool isHit = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && lasergoal_0 != null)
        {
            spriteRenderer.sprite = lasergoal_0;
        }
    }

    public void Activate()
    {
        if (!isHit)
        {
            isHit = true;
            if (spriteRenderer != null && lasergoal_1 != null)
                spriteRenderer.sprite = lasergoal_1;
        }
    }
}
