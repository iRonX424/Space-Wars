using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int wayPointIndex = 0;
    
    void Start()
    {
        waypoints = waveConfig.getWaypoints();
        this.transform.position = waypoints[wayPointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void setWave(WaveConfig wave)
    {
        waveConfig = wave;
    }

    void Move()
    {
        if(wayPointIndex<=waypoints.Count-1)
        {
            var targetPos = waypoints[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.getMoveSpeed() * Time.deltaTime;

            this.transform.position = Vector2.MoveTowards(this.transform.position,targetPos,movementThisFrame);

            if (this.transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
