using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;
    private int healAmount = 20; 

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            PerformHeal(); 
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        totalSpinAmount = 0f;

        ActionStart(onActionComplete);
    }

    private void PerformHeal()
    {
        HealthSystem healthSystem = GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            int currentHealth = (int)(healthSystem.GetHealthNormalized() * healthSystem.GetMaxHealth());
            int newHealth = Mathf.Min(currentHealth + healAmount, healthSystem.GetMaxHealth());

            int restoredHealth = newHealth - currentHealth; 
            Debug.Log($"Healing performed: +{restoredHealth} health.");

            healthSystem.SetHealth(newHealth);
        }
        else
        {
            Debug.LogError("HealthSystem component not found on this unit!");
        }
    }

    public override string GetActionName()
    {
        return "Heal";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 1;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 5,
        };
    }
}
