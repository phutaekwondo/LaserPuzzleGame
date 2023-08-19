using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    float m_maxLength = 20;
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
        //reset the straight laser
        m_bouncePoints.Clear();
        m_lastLength = m_maxLength;
        m_lastDirection = m_startDirection;

        RaycastHit hit;
        int laserInteractedLayer = LayerMask.NameToLayer("LaserInteracted");
        List<int> hitLayers = new List<int>();
        List<GameObject> hitObjects = new List<GameObject>();

        bool isHit = Physics.Raycast(m_startPosition, m_lastDirection, out hit, m_lastLength, ~(1<<laserInteractedLayer));
        while (isHit)
        {
            GameObject hitObject = hit.collider.gameObject;
            hitObjects.Add(hitObject);
            hitLayers.Add(hitObject.layer);
            //avoid re-hitting the same object, which would cause an infinite loop
            hitObject.layer = laserInteractedLayer;

            LaserInteractable laserInteractable = hitObject.GetComponent<LaserInteractable>(); 

            if (laserInteractable != null && laserInteractable.IsBounce(m_lastDirection))
            {
                m_bouncePoints.Add(hit.point);

                m_lastDirection = laserInteractable.GetBounceDirection(m_lastDirection);
                m_lastLength -= hit.distance;

                isHit = Physics.Raycast(hit.point, m_lastDirection, out hit, m_lastLength, ~(1<<laserInteractedLayer));
            }
            else
            {
                isHit = false;

                //stop the laser
                m_lastLength = hit.distance;
            }
        }

        //reset the layer of the hit objects
        for (int i = 0; i < hitObjects.Count; i++)
        {
            hitObjects[i].layer = hitLayers[i];
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
