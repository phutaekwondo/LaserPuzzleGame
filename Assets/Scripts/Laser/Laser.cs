using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    float m_length = 10;
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
        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(m_startPosition, m_startDirection, out hit, m_length))
        {
            GameObject hitObject = hit.collider.gameObject;
            LaserInteractable laserInteractable = hitObject.GetComponent<LaserInteractable>();
            if (laserInteractable != null)
            {
                m_bouncePoints.Add(hit.point);
                m_lastDirection = laserInteractable.BounceDirection(m_startDirection);
            }
        }
    }

    private void UpdateLineRenderer()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(m_startPosition);
        positions.AddRange(m_bouncePoints);
        positions.Add(positions[positions.Count-1] + m_lastDirection * m_length);

        m_lineRenderer.positionCount = positions.Count;
        m_lineRenderer.SetPositions(positions.ToArray());
    }

}
