using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : Unit
{
    Slider healthSlider;
    public List<GameObject> inRadius = new List<GameObject>();
    float attIntermission;

    public Wizard(float health, float speed, float attDamage, float attRange, int team) : base(health, speed, attDamage, attRange, team)
    {
        Health = health;
        MAX_HEALTH = health;
        Speed = speed;
        AttDamage = attDamage;
        AttRange = attRange;
        Team = team;
    }

    private void Start()
    {
        Health = 20;
        MAX_HEALTH = health;
        Speed = 5;
        AttDamage = 4;
        AttRange = 10;
        Team = team;

        //Initialising the health Slider

        healthSlider = (gameObject.GetComponentInChildren<Canvas>()).GetComponentInChildren<Slider>();
        healthSlider.value = 1;
    }

    private void Update()
    {
        OutOfBounds();
        Move();
    }

    public override void Move()
    {
        //Choosing enemies
        List<GameObject> enemies = new List<GameObject>();

        if (!gameObject.CompareTag("GreenTeam"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("GreenTeam"))
            {
                if (go.GetComponent<Building>() == null)
                {
                    enemies.Add(go);
                }

            }
        }

        if (!gameObject.CompareTag("RedTeam"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("RedTeam"))
            {
                if (go.GetComponent<Building>() == null)
                {
                    enemies.Add(go);
                }
            }
        }

        closest = null;
        float closestDistance = float.MaxValue;


        foreach (GameObject go in enemies) //Moving towards Enemies
        {
            if (Vector3.Distance(gameObject.transform.position, go.transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(gameObject.transform.position, go.transform.position);
                closest = go;
            }
        }
        if (closest != null)
        {
            transform.LookAt(closest.transform.position);
            attIntermission += Time.deltaTime;
            if (Health <= MAX_HEALTH * 0.50f)
            {
                transform.Translate(Vector3.forward * -Speed * Time.deltaTime);
            }
            else if (closestDistance < 3)
            {
                transform.Translate(Vector3.forward * -Speed * Time.deltaTime);
            }
            else if (closestDistance > AttRange && onGround)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            }
            if (inRadius.Count > 0)
            {
                if (attIntermission >= 3) //Cooldown on attack so he doesnt destroy everything
                {
                    AreaBlast();
                }

            }
        }
    }


    public override void Attack(GameObject target)
    {
        Unit u = target.GetComponent<Unit>();
        if (u != null)
        {
            u.Damage(AttDamage);
        }
    }



    public void AreaBlast()//Creating attack on all enemies within range
    {
        foreach (GameObject go in inRadius)
        {
            if (go != null)
            {
                Attack(go);
            }


        }
        attIntermission = 0;
    }

    public override void Damage(float amount)//Updating Health
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

    private void OutOfBounds()
    {
        if (gameObject.transform.position.y < -1)
        {
            Death();
        }
    }
}

