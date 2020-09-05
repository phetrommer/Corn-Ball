using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text roundText;

    private void Start()
    {
        roundText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        roundText.text = "ROUND: " + GameManager.Instance.currentRound.ToString();
    }
}
