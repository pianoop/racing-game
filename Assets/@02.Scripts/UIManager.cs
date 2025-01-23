using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI GasText;
    public List<GameObject> GameOverPanels;

    public void SetRemainGas(float value)
    {
        GasText.text = "Gas: " + ((int)value).ToString();
    }

    public void GameOver()
    {
        foreach (var gameOverPanel in GameOverPanels)
        {
            gameOverPanel.SetActive(true);
        }
    }
    
}
