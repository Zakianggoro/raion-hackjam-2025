using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void Update()
    {
        // Test collision detection at mouse position
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            
            Debug.Log($"Mouse clicked at world position: {mousePos}");
            
            // Test what's at this position
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null)
            {
                Debug.Log($"Found collider: {hit.gameObject.name} with tag: {hit.tag}");
                Debug.Log($"Collider bounds: {hit.bounds}");
                Debug.Log($"Is Trigger: {hit.isTrigger}");
            }
            else
            {
                Debug.Log("No collider found at mouse position");
            }
            
            // Test all colliders at this point
            Collider2D[] allHits = Physics2D.OverlapPointAll(mousePos);
            Debug.Log($"Total colliders at point: {allHits.Length}");
            for (int i = 0; i < allHits.Length; i++)
            {
                Debug.Log($"  [{i}] {allHits[i].gameObject.name} - Tag: {allHits[i].tag} - Trigger: {allHits[i].isTrigger}");
            }
        }
    }
}