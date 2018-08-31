using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy enemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets")]
    public GameObject bulletPrefabs;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Use this for initialization
    void Start() {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    //function to get turret to point towards closest target within a range
    void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }


	// Update is called once per frame
	void Update () {
        //target locking on every frame
		if (target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }
        lockOnTarget();

        if (useLaser)
        {
            laser();
        }
        else
        { 
            if(fireCountdown <= 0)
            {
                shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void lockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookAtRotation, Time.deltaTime * turnSpeed).eulerAngles; //convert into euler angles because we only want y rotation
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void laser()
    {
        enemy.takeDamage(damageOverTime * Time.deltaTime);
        enemy.slow(slowAmount);
        //line renderer object has an array of points this is setting the only 2 points to go from turret to target
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();//.play not .enabled for particle system
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);//turrets pos
        lineRenderer.SetPosition(1, target.position);//enemy target pos

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.seek(target);
        }
    }

    //display the range in a red sphere at range turret can fire
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
