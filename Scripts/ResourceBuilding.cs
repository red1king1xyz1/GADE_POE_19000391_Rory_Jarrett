using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBuilding : Building
{

    Slider healthSlider;
    private int resRemaining;
    private int resRate;

    float resWait;
    float resIntemssion;

    private object gM;


    public ResourceBuilding(float health, int team, GameObject unitType) : base(health, team)
    {
        Health = 100;
        MAX_HEALTH = Health;
        Team = team;

    }

    void Start()
    {
        resRemaining = Random.Range(500, 2000);
        resRate = Random.Range(10, 50);
        resIntemssion = 2.0f;

    }

    void Update()
    {
        resWait += Time.deltaTime;
        if (resWait >= resIntemssion)
        {
            Dig();
            resWait = 0;
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

    private void Dig()
    {
        MapManager gM = GameObject.FindGameObjectWithTag("GM").GetComponent<MapManager>();

        if (gameObject.CompareTag("RedTeam"))
        {
            if (resRemaining <= 0)
            {
                Destroy(gameObject);
            }
            else if (resRemaining < resRate)
            {
                gM.ResourceRed += resRemaining;
                resRemaining = 0;
            }
            else
            {
                resRemaining -= resRate;
                gM.ResourceRed += resRate;
            }

        }
        if (gameObject.CompareTag("GreenTeam"))
        {
            if (resRemaining <= 0)
            {
                Destroy(gameObject);
            }
            else if (resRemaining < resRate)
            {
                gM.ResourceGreen += resRemaining;
                resRemaining = 0;
            }
            else
            {
                resRemaining -= resRate;
                gM.ResourceGreen += resRate;
            }

        }
    }
}
