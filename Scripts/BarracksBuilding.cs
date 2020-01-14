using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksBuilding : Building
{

    Slider healthSlider;

    [SerializeField]
    GameObject unitType;
    [SerializeField]
    private int spawnCost;

    float spawnWait;
    float spawnIntemssion;

    public BarracksBuilding(float health, int team, GameObject unitType) : base(health, team)
    {
        Health = 100;
        MAX_HEALTH = Health;
        Team = team;
        this.unitType = unitType;
    }
    private void Start()
    {
        spawnCost = 100;
        spawnIntemssion = Random.Range(5, 15);
        Health = 100;
        MAX_HEALTH = Health;
    }
    private void Update()
    {
        spawnWait += Time.deltaTime;
        if (spawnWait >= spawnIntemssion)
        {
            Spawn();
            spawnWait = 0;
        }

    }

    public override void Damage(float amount)
    {
        healthSlider = (gameObject.GetComponentInChildren<Canvas>()).GetComponentInChildren<Slider>();
        health -= amount;
        if (healthSlider != null)
        {
            healthSlider.value = health / MAX_HEALTH;
        }

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void Spawn()
    {
        bool canSpawn = false;

        MapManager gM = GameObject.FindGameObjectWithTag("GM").GetComponent<MapManager>();
        if (gameObject.CompareTag("GreenTeam"))
        {
            if (gM.ResourceGreen > spawnCost)
            {
                gM.ResourceGreen -= spawnCost;
                canSpawn = true;
            }
        }
        else if (gameObject.CompareTag("RedTeam"))
        {
            if (gM.ResourceRed > spawnCost)
            {
                gM.ResourceRed -= spawnCost;
                canSpawn = true;
            }
        }
        if (canSpawn)
        {
            Vector3 spawnLoc = transform.position + (transform.forward * 5);


            Instantiate(unitType, spawnLoc, Quaternion.identity);
        }

    }
}