using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;

    [SerializeField] private int health = 100;
    private int healthMax;

    private ScoreManager scoreManager;

    private void Awake()
    {
        healthMax = health;

        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    public void SetHealth(int newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, healthMax); 
        OnDamaged?.Invoke(this, EventArgs.Empty); 
    }

    public int GetMaxHealth()
    {
        return healthMax;
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);

        Unit unit = GetComponent<Unit>();
        if (unit != null && unit.IsEnemy())
        {
            scoreManager?.AddScore(1);
            Debug.Log("Enemy killed! Score increased.");
        }
    }
}
