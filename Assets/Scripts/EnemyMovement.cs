using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int wayPointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.wayPoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); //time.deltatime is here to make sure speed is independant from fps

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            getNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void getNextWayPoint()
    {
        if (wayPointIndex >= Waypoints.wayPoints.Length - 1)
        {
            reachedEnd();
            return;
        }

        wayPointIndex++;
        target = Waypoints.wayPoints[wayPointIndex];
    }

    void reachedEnd()
    {
        PlayerStats.lives--;
        WaveSpawner.enemiesAlive--;
        AudioManager.instance.playSound("Take Damage");
        Destroy(gameObject);
    }
}
