using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float maxRayDistance = 100.0f;
    private bool hasLineRenderer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (lineRenderer = GetComponent<LineRenderer>())
        {
            hasLineRenderer = true;
        } else
        {
            Debug.LogError("Error: The GameObject has no LineRenderer component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLineRenderer)
        {
            CalculateRay();
        }
    }

    private void CalculateRay()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, maxRayDistance))
            {
                Debug.Log("Hit object: from " + transform.position + " to " + hit.point);
                RenderLaser(hit.point);
            }
            else
            {
                Debug.Log("Hit sky: from " + transform.position + " to " + hit.point);
                RenderLaser(new Vector3(transform.position.x+maxRayDistance, transform.position.y, transform.position.z));
            }
        }
        else
        {
            RemoveLaserRender();
        }
    }

    public void RenderLaser(Vector3 LaserEndPosition)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, LaserEndPosition);
    }

    public void RemoveLaserRender()
    {
        lineRenderer.positionCount = 1;
    }

}
