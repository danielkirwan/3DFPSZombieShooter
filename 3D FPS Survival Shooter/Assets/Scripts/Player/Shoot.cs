using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bloodSplatter;
    [SerializeField] private LayerMask layer;
    // Update is called once per frame
    void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Health health = hit.collider.GetComponent<Health>();
                if(health == null)
                {
                    Debug.Log("Object doesn't have health component");
                }
                if(health != null)
                {
                    health.TakeDamage(20);
                    Instantiate(_bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }
}
