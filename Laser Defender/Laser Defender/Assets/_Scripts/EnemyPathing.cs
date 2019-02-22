using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    private WaveConfig waveConfig;
    private int waypointIndex = 0;
    private List<Transform> waypoints;
    private float moveSpeed;


    private void Start()
    {
        waypoints = waveConfig.GetWayPoins();
        moveSpeed = waveConfig.GetMoveSpeed;
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
