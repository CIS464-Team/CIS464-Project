using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Laser : MonoBehaviour {
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public float maxDistance = 100f;
    public Collider2D[] collidersToIgnore; // Assign in inspector

    // Add this for the glow prefab
    [Header("Laser Lighting")]
    public Color laserColor = Color.blue;
    public float lightIntensity = 1f;
    public float lightOuterRadius = 1.5f;
    public float lightSpacing = 1f;
    private System.Collections.Generic.List<Light2D> segmentLights = new System.Collections.Generic.List<Light2D>();
    
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
        UpdateLaserLights(points);


    
    }
    void UpdateLaserLights(System.Collections.Generic.List<Vector3> points) {
    int lightIndex = 0;
    

    for (int i = 0; i < points.Count - 1; i++) {
        Vector3 start = points[i];
        Vector3 end = points[i + 1];
        float segmentLength = Vector3.Distance(start, end);
        int lightsInSegment = Mathf.Max(1, Mathf.FloorToInt(segmentLength / lightSpacing));

        for (int j = 0; j <= lightsInSegment; j++) {
            float t = (float)j / lightsInSegment;
            Vector3 pos = Vector3.Lerp(start, end, t);
            Light2D l = GetOrCreateLight(lightIndex);
            l.color = laserColor;
            l.transform.position = pos;
            lightIndex++;
        }
    }

    for (int i = lightIndex; i < segmentLights.Count; i++)
        segmentLights[i].gameObject.SetActive(false);
    }

    Light2D GetOrCreateLight(int index) {
    if (index < segmentLights.Count) {
        segmentLights[index].gameObject.SetActive(true);
        return segmentLights[index];
    }

    GameObject go = new GameObject("LaserLight");
    go.transform.SetParent(this.transform);
    Light2D l = go.AddComponent<Light2D>();
    l.lightType = Light2D.LightType.Point;
    l.intensity = lightIntensity;
    l.pointLightOuterRadius = lightOuterRadius;
    segmentLights.Add(l);
    return l;
}
}
