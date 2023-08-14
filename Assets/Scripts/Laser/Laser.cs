using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    float m_length = 10;
    float m_maxLength = 10;
    LineRenderer m_lineRenderer;
    Vector3 m_startPosition;
    Vector3 m_direction;

    void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_startPosition = transform.position;
        m_direction = transform.up;
    }

    public void SetLaserStart(Vector3 startPosition, Vector3 direction)
    {
        this.m_startPosition = startPosition;
        this.m_direction = direction;
        UpdateLaserStart();
    }

    private void FixedUpdate() 
    {
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(m_startPosition, m_direction, out hit, m_length))
        {
            m_length = hit.distance;
        }
        else
        {
            m_length = m_maxLength;
        }
    }

    private void UpdateLaserStart()
    {
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.SetPosition(0, m_startPosition);
        m_lineRenderer.SetPosition(1, m_startPosition + m_direction * m_length);
    }

}
