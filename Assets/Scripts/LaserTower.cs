using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    Laser m_laser;
    [SerializeField] GameObject m_laserPrefab;
    [SerializeField] Transform m_laserSpawnPoint;
    Vector3 m_startLaserPosition;
    Vector3 m_laserDirection;

    private void Start() 
    {
        GameObject laserObject = Instantiate(m_laserPrefab, m_laserSpawnPoint.position, Quaternion.identity);
        laserObject.transform.position = Vector3.zero;
        m_laser = laserObject.GetComponent<Laser>();
    }

    private void Update() 
    {
        m_startLaserPosition = m_laserSpawnPoint.position;
        m_laserDirection = this.transform.forward;
        m_laser.SetLaserStart(m_startLaserPosition, m_laserDirection);
        
    }
}
