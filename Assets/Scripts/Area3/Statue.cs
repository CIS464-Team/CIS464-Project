using UnityEngine;
using System;
using System.Collections;

public class Statue : MonoBehaviour
{
    public float pushDist = 1f;
    public int statueID;
    public float pushSpeed;
    public LayerMask ObjLayer;
    public float snapRadius;
    private bool isMoving = false;
    public bool IsMoving => isMoving;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When player's collider hit statue push in the appropriate direction
        if(isMoving) return;
        //make sure only player tag can push it
        if(!collision.gameObject.CompareTag("Player")) return;


        Vector2 pushDir = GetPushDir(collision.transform);
        Vector2 targetPos = (Vector2)transform.position + (pushDir * pushDist);
        RaycastHit2D hit = Physics2D.Raycast(
        transform.position,
        pushDir,
        pushDist,
        ObjLayer
    );

    

    if (hit.collider == null) StartCoroutine(Slide(targetPos));
    
    }
    
    //calculate push direction
    private Vector2 GetPushDir(Transform player)
    {
        Vector2 dir = (transform.position - player.position).normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) + 0.1f)
        {
            return new Vector2(Mathf.Sign(dir.x), 0);
        }
        else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x) + 0.1f)
        {
            return new Vector2(0, Mathf.Sign(dir.y));}
        else{
            return new Vector2(Mathf.Sign(dir.x), 0); 
        }
}



    private IEnumerator Slide(Vector2 target)
    {
        isMoving = true;

        //while statue isnt in target position keep sliding
        while((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                pushSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = target;

        //When statues gets close, snap to slot position
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, snapRadius);
        foreach(var col in nearby)
        {
            statueSlot slot = col.GetComponent<statueSlot>();
            if(slot != null)
            {
                transform.position = slot.transform.position;
                soundManager.Instance.PlaySFX("PlasticBox2");
                slot.RegisterStatue(this);
                
                break;
            }
        }

        isMoving = false;
    }

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    //Reset if player makes mistake or loses statues
    public void Reset()
    {
        StopAllCoroutines();
        isMoving = false;
        StartCoroutine(Slide(startPos));
    }

}