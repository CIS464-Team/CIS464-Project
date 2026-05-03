using UnityEngine;
using System;
using System.Collections;

public class Statue : MonoBehaviour
{
    public float pushDist = 1f;
    public int statueID;
    public float pushSpeed;
    public LayerMask ObjLayer;
    

    private bool isMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isMoving) return;
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
    
    private Vector2 GetPushDir(Transform player)
    {
        Vector2 dir = (transform.position - player.position).normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) + 0.1f) {
        return new Vector2(Mathf.Sign(dir.x), 0);
        }
        else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x) + 0.1f){
        return new Vector2(0, Mathf.Sign(dir.y));}
        else{
        return new Vector2(Mathf.Sign(dir.x), 0); }
}



    private IEnumerator Slide(Vector2 target)
    {
        isMoving = true;

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
        isMoving = false;
    }

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Reset()
    {
        StopAllCoroutines();
        isMoving = false;
        StartCoroutine(Slide(startPos));
    }

}