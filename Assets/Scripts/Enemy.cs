using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    
    [HideInInspector]//makes a variable not show up in inspector but still serializable
    public float speed;

    public float startSpeed = 10f; 
    public float startHealth = 100f;

    private float health;
    public int bounty = 50;

    public GameObject deathEffect;
    //AudioClip deathSound;

    [Header("Unity Info")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        health = startHealth;
        speed = startSpeed;    
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health/startHealth;
        if (health <= 0 && !isDead)
        {
            die();
        }
    }

    public void slow(float slowAmount)
    {
        speed = startSpeed * (1f - slowAmount);
    }

    void die()
    {
        isDead = true;
        PlayerStats.money += bounty;
        //if sfx ever get added
        AudioManager.instance.playSound("Enemy Death");

        WaveSpawner.enemiesAlive--;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);
        Destroy(gameObject);

    }
}
