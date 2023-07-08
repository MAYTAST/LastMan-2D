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
    public AbilityType AbilityType;
    public float cooldownTime;
    public int Currentlevel;
    public GameObject[] LevelsGameObject;
}
public class AbilityManager : MonoBehaviour
{
    public PlayerControlller playerControlller;
    public List<Ability> abilities;

    private void Update()
    {
    
    }
    public void  AblitySelected(AbilityType abilityType,int AbilityCurrentLevel)
    {
        foreach (Ability item in abilities)
        {
           
        }
    }
    public void BoostSpeed(int level)
    {
    
    }
    public void ActivateShield(int level)
    {
    
    }
}
