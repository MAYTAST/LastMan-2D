using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image FillImage;
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private float maxXp;

    [Tooltip("max xp should increase by how much percentage each time")]
    [SerializeField] private float percentIncrease;

    int currentLvl = 0;
    float currentXp;




    private void OnEnable()
    {
        Collector.OnCollect += OnGemsCollectedChanged;
    }

    private void OnDisable()
    {
        Collector.OnCollect -= OnGemsCollectedChanged;
    }



   
   

    private void UpdateExpFill(float fillAmt)
    {

        
        FillImage.fillAmount = fillAmt;
        LevelText.text = currentLvl.ToString();
        
       
    }

    /// <summary>
    /// Will subscribe to the events which want to change the xp
    /// </summary>
    /// <param name="newXp">xp that will be added to the current xp</param>
    public void OnGemsCollectedChanged(float newXp)
    {

        currentXp += newXp;
        float clampedXp = currentXp / maxXp;

        UpdateExpFill(clampedXp);


        if(currentXp == maxXp)
        {
            //After selecting ability from ability panel.

            IncreaseByPercentage(maxXp, percentIncrease);
            currentXp = 0;
            UpdateExpFill(currentXp);
        }


    }


    private float IncreaseByPercentage(float value, float percentaeIncrease)
    {
        float increasedValue = (value * percentaeIncrease) / 100;
        float newValue = value + increasedValue;
        return newValue;

    }

}
