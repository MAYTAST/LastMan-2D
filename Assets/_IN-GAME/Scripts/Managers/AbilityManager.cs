using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    RotatingBlade,
    SpeedBoost,
    Bomb,
    ForceField
}
[System.Serializable]
public class Ability
{
    public AbilityType AbilityType;
    public int Currentlevel;
    public GameObject[] LevelsGameObject;
}
public class AbilityManager : MonoBehaviour
{
    public PlayerControlller playerControlller;
    public List<Ability> abilities;
    public float SpeedUpgradeValue=0.5f;
    private Ability CurrentAblity;
    private float PlayerSpeed;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //  ActivateAbility(AbilityType.RotatingBlade);
            // ActivateAbility(AbilityType.ForceField);
            ActivateAbility(AbilityType.SpeedBoost);
        }
    
    }
    private void Start()
    {
        PlayerSpeed = playerControlller.moveSpeed;
    }
    public void  AblitySelected(AbilityType abilityType,int AbilityCurrentLevel)
    {
        foreach (Ability item in abilities)
        {
            if (abilityType==item.AbilityType)
            {

            }
        }
    }
    public void ActivateAbility(AbilityType _abilityType)
    {
        AssingCurrentAblity(_abilityType);
        if (_abilityType == AbilityType.SpeedBoost)
        {
            ActiveSpeedBoost(CurrentAblity.Currentlevel);
            CurrentAblity.Currentlevel++;
            return;
        }
        CheckCurrentAblityLevel();
        CurrentAblity.Currentlevel++;
    }

    private void CheckCurrentAblityLevel()
    {
        switch (CurrentAblity.Currentlevel)
        {
            case 0:
                SetCurrentLevelAblityActive(0);
                break;
            case 1:
                SetCurrentLevelAblityActive(1);
                break;
            case 2:
                SetCurrentLevelAblityActive(2);
                break;
            default:
                break;
        }
    }

    public void SetCurrentLevelAblityActive(int AblityLevel)
    {
        for (int i = 0; i < CurrentAblity.LevelsGameObject.Length; i++)
        {
            if (i == AblityLevel)
            {
                CurrentAblity.LevelsGameObject[i].SetActive(true);
                break;
            }
            else
            {
                CurrentAblity.LevelsGameObject[i].SetActive(false);
            }
        }
    }
    private void AssingCurrentAblity(AbilityType abilityType)
    {
        foreach (Ability item in abilities)
        {
            if (item.AbilityType == abilityType)
            {
                CurrentAblity = item;
                break;
            }
        }
    }
    void ActiveSpeedBoost(int Currentlevel)
    {
        if (Currentlevel < 3)
        {
            playerControlller.moveSpeed += SpeedUpgradeValue;
        }
    }
}
