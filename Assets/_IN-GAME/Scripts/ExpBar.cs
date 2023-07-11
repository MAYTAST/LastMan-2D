using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image FillImage;
    [SerializeField] private TMP_Text LevelText;

    public int gemsCollected;
    public int level;


    private void Start()
    {
        UpdateExpFill();
    }
   

    private void UpdateExpFill()
    {

        level = gemsCollected / 30;

        FillImage.fillAmount = (float)gemsCollected / 30;
        LevelText.text = "Level" + level;
        
       
    }

    public void OnGemsCollectedChanged()
    {
        UpdateExpFill();
    }
    
}
