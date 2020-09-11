using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text roundText;
    public TMP_Text pointsText;

    void Update()
    {
        roundText.text = "ROUND: " + GameManager.Instance.currentRound.ToString();
        pointsText.text = "POINTS: " + GameManager.Instance.currentPoints.ToString();
    }
}
