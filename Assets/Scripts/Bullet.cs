using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public int bulletDamage = 50;
    public float bulletSpeed = 70f;
    public float damageRadius = 0f;

    public GameObject impactEffect;

    public void seek(Transform tempTarget)
    {
        target = tempTarget;
    }
	
	// Update is called once per frame
	void Update () {
		if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            hitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World); //Space.World neccesary or else you get weird sphereical movement
        transform.LookAt(target);
	}

    void hitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if(damageRadius > 0)
        {
            explode();
        }
        else
        {
            damage(target);
        }
        Destroy(gameObject);
    }

    void damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.takeDamage(bulletDamage);
        }
    }

    void explode()
    {
        Collider[] objectsInExplosion = Physics.OverlapSphere(transform.position, damageRadius);
        foreach(Collider collider in objectsInExplosion)
        {
            if(collider.tag == "Enemy")
            {
                damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
