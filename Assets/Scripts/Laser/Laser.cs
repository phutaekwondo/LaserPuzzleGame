using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    float m_maxLength = 10;
    float m_lastLength = 0;
    LineRenderer m_lineRenderer;
    Vector3 m_startPosition;
    Vector3 m_startDirection;
    Vector3 m_lastDirection;
    List<Vector3> m_bouncePoints = new List<Vector3>();

    void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_startPosition = transform.position;
        m_startDirection = transform.up;
        m_lastDirection = m_startDirection;
    }

    public void SetLaserStart(Vector3 startPosition, Vector3 direction)
    {
        this.m_startPosition = startPosition;
        this.m_startDirection = direction;
        m_lastDirection = m_startDirection;
    }

    private void FixedUpdate() 
    {
        UpdateLaserPath();
        UpdateLineRenderer();
    }

    private void UpdateLaserPath()
    {
        //clear the bounce points
        m_bouncePoints.Clear();

        m_lastLength = m_maxLength;
        RaycastHit hit;
        int laserInteractableLayer = LayerMask.NameToLayer("LaserInteractable");
        int LaserInteractedLayer = LayerMask.NameToLayer("LaserInteracted");
        List<GameObject> hitObjects = new List<GameObject>();

        bool isHit = Physics.Raycast(m_startPosition, m_lastDirection, out hit, m_lastLength, 1 << laserInteractableLayer);
        while (isHit)
        {
            GameObject hitObject = hit.collider.gameObject;
            LaserInteractable laserInteractable = hitObject.GetComponent<LaserInteractable>(); 
            if (laserInteractable != null)
            {
                hitObject.layer = LaserInteractedLayer;
                hitObjects.Add(hitObject);

                m_bouncePoints.Add(hit.point);

                //if the laser not bounce from the object, then it go straight through. need to make it stop
                if (laserInteractable.IsBounce(m_lastDirection))
                {
                    m_lastDirection = laserInteractable.GetBounceDirection(m_lastDirection);
                }
                else
                {
                    Debug.Log("Laser is going straight through the object");
                }

                m_lastLength -= hit.distance;

                isHit = Physics.Raycast(hit.point, m_lastDirection, out hit, m_lastLength, 1 << laserInteractableLayer);
            }
            else
            {
                isHit = false;
                Debug.LogError("Hit object is not laser interactable");
            }
        }

        //reset the layer of the hit objects
        foreach (GameObject hitObject in hitObjects)
        {
            hitObject.layer = laserInteractableLayer;
        }
    }

    private void UpdateLineRenderer()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(m_startPosition);
        positions.AddRange(m_bouncePoints);
        positions.Add(positions[positions.Count-1] + m_lastDirection * m_lastLength);

        m_lineRenderer.positionCount = positions.Count;
        m_lineRenderer.SetPositions(positions.ToArray());
    }

}
