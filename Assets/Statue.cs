using UnityEngine;

public class Statue : MonoBehaviour
{
    public float pushDist = 1f;
    public int statueID;
    private Rigidbody2D rb;

void Awake(){
    rb = GetComponent<Rigidbody2D>();
}

    public void Push(Vector2 direction)
    {

       Vector2 newPos = rb.position + direction * pushDist;

        // Check if destination is clear (excluding self)
        Collider2D hit = Physics2D.OverlapCircle(newPos, 0.2f);
        bool blocked = hit != null && hit.gameObject != this.gameObject && !hit.isTrigger;

        Debug.Log($"Push called | newPos={newPos} | blocked by: {hit?.name ?? "nothing"}");

        if (!blocked)
        {
            rb.MovePosition(newPos);  // correct way to move kinematic rb
            PuzzleManager.instance.CheckPuzzle();
        }
    }
}