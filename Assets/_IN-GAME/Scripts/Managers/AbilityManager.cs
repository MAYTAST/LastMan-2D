using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    RotatingBlade,
    SpeedBoost,
    Bomb,
    PowerBullet,
    ForceField
}
[System.Serializable]
public class Ability
{
    public string name;
    public AbilityType AbilityType;
    public string description;
    public float cooldownTime;
    public float lastUsedTime;
    public int level;

    public Ability(string name, string description, float cooldownTime, int level)
    {
        this.name = name;
        this.description = description;
        this.cooldownTime = cooldownTime;
        this.lastUsedTime = -cooldownTime;
        this.level = level;
    }

    public bool IsReady()
    {
        return Time.time - lastUsedTime >= cooldownTime;
    }

    public virtual void Activate(AbilityManager player)
    {
        // This method should be overridden by subclasses to perform the ability's action
    }
}

public class RotatingBladeAbility : Ability
{
    public float damage;

    public RotatingBladeAbility(int level) : base("Rotating Blade", "A blade that spins around you, dealing damage to enemies.", 10f, level)
    {
        switch (level)
        {
            case 1:
                damage = 5f;
                break;
            case 2:
                damage = 10f;
                break;
            case 3:
                damage = 15f;
                break;
        }
    }

    public override void Activate(AbilityManager player)
    {
        // TODO: Implement rotating blade logic
        Debug.Log("Activating Rotating Blade ability (level " + level + ")");
        player.TakeDamage(damage);
    }
}

public class SpeedBoostAbility : Ability
{
    public float boostDuration;
    public float boostAmount;

    public SpeedBoostAbility(int level) : base("Speed Boost", "Temporarily increases your movement speed.", 15f, level)
    {
        switch (level)
        {
            case 1:
                boostDuration = 3f;
                boostAmount = 5f;
                break;
            case 2:
                boostDuration = 5f;
                boostAmount = 10f;
                break;
            case 3:
                boostDuration = 7f;
                boostAmount = 15f;
                break;
        }
    }

    public override void Activate(AbilityManager player)
    {
        // TODO: Implement speed boost logic
        Debug.Log("Activating Speed Boost ability (level " + level + ")");
        player.BoostSpeed(boostAmount, boostDuration);
    }
}

public class BombAbility : Ability
{
    public float damage;

    public BombAbility(int level) : base("Bomb", "Throws a bomb that explodes on impact, dealing damage to enemies.", 20f, level)
    {
        switch (level)
        {
            case 1:
                damage = 10f;
                break;
            case 2:
                damage = 20f;
                break;
            case 3:
                damage = 30f;
                break;
        }
    }

    public override void Activate(AbilityManager player)
    {
        // TODO: Implement bomb logic
        Debug.Log("Activating Bomb ability (level " + level + ")");
        player.TakeDamage(damage);
    }
}

public class PowerBulletAbility : Ability
{
    public float damage;

    public PowerBulletAbility(int level) : base("Power Bullet", "Fires a powerful bullet that deals increased damage to enemies.", 5f, level)
    {
        switch (level)
        {
            case 1:
                damage = 15f;
                break;
            case 2:
                damage = 25f;
                break;
            case 3:
                damage = 35f;
                break;
        }
    }

    public override void Activate(AbilityManager player)
    {
        // TODO: Implement power bullet logic
        Debug.Log("Activating Power Bullet ability (level " + level + ")");
        player.TakeDamage(damage);
    }
}

public class ForceFieldAbility : Ability
{
    public float duration;

    public ForceFieldAbility(int level) : base("Force Field", "Creates a protective force field around you that absorbs incoming damage.", 30f, level)
    {
        switch (level)
        {
            case 1:
                duration = 5f;
                break;
            case 2:
                duration = 7f;
                break;
            case 3:
                duration = 10f;
                break;
        }
    }

    public override void Activate(AbilityManager player)
    {
        // TODO: Implement force field logic
        Debug.Log("Activating Force Field ability (level " + level + ")");
        player.ActivateShield(duration);
    }
}

public class AbilityManager : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;

    public List<Ability> abilities;

    private void Update()
    {
        // Handle input for activating abilities
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAbility(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateAbility(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateAbility(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateAbility(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateAbility(4);
        }
    }

    private void ActivateAbility(int index)
    {
        if (index < 0 || index >= abilities.Count)
        {
            return;
        }

        Ability ability = abilities[index];

        if (!ability.IsReady())
        {
            return;
        }

        ability.Activate(this);
        ability.lastUsedTime = Time.time;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO: Implement death logic
        Debug.Log("Player has died!");
    }

    public void BoostSpeed(float amount, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(amount, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float amount, float duration)
    {
        speed += amount;

        yield return new WaitForSeconds(duration);

        speed -= amount;
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    private IEnumerator ShieldCoroutine(float duration)
    {
        // TODO: Implement shield logic
        Debug.Log("Activating shield for " + duration + " seconds");

        yield return new WaitForSeconds(duration);

        // TODO: Remove shield effect
        Debug.Log("Shield has expired");
    }
}
