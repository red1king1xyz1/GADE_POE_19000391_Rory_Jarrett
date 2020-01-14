using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected float health;
    protected float MAX_HEALTH;
    protected float speed;
    protected float attDamage;
    protected float attRange;
    protected int team;
    protected bool onGround;
    protected GameObject closest;

    public Unit(float health, float speed, float attDamage, float attRange, int team)
    {
        MAX_HEALTH = health;
        Health = health;
        Speed = speed;
        AttDamage = attDamage;
        AttRange = attRange;
        Team = team;
    }

    public float AttRange { get => attRange; set => attRange = value; }

    protected int Team { get => team; set => team = value; }

    protected float AttDamage
    {
        get => attDamage; set
        {
            if (value <= 0)
            {
                attDamage = 0;
            }
            else
            {
                attDamage = value;
            }
        }
    }

    protected float Speed { get => speed; set => speed = value; }

    protected float Health { get => health; set => health = value; }

    public abstract void Move();

    public abstract void Attack(GameObject target);

    public abstract void Damage(float amount);

    // checks if unit is on ground
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            onGround = false;

        }
    }


}
