using UnityEngine;

[ExecuteAlways]
public class Laser : MonoBehaviour {
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public float maxDistance = 100f;
    public Collider2D[] collidersToIgnore; // Assign in inspector

    void Update() {
        ShootLaser();
    }

    void ShootLaser() {
        Vector2 currentPos = firePoint.position;
        Vector2 currentDir = firePoint.right;
        int maxBounces = 50;
        var points = new System.Collections.Generic.List<Vector3>();
        points.Add(currentPos);

        for (int i = 0; i < maxBounces; i++) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(currentPos, currentDir, maxDistance);
            RaycastHit2D? validHit = null;
            foreach (var hit in hits) {
                if (hit.collider == null) continue;
                bool shouldIgnore = false;
                foreach (var ignoreCol in collidersToIgnore) {
                    if (hit.collider == ignoreCol) {
                        shouldIgnore = true;
                        break;
                    }
                }
                if(hit.collider.tag == "LaserIgnore") {
                    shouldIgnore = true;
                }
                if(hit.collider.tag == "MainCamera")
                {
                    shouldIgnore = true;
                }
                if (!shouldIgnore) {
                    validHit = hit;
                    break;
                }
            }

            if (validHit.HasValue) {
                var hit = validHit.Value;
                points.Add(hit.point);
                if(hit.collider.tag == "Goal")
                {
                    hit.collider.GetComponent<LaserGoal>()?.Activate();
                    break;  
                } else if (hit.collider.tag == "Reflective") {
                    currentDir = Vector2.Reflect(currentDir, hit.normal);
                    currentPos = hit.point + currentDir * 0.01f;
                } else {
                    break;
                }
            } else {
                points.Add(currentPos + currentDir * maxDistance);
                break;
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
