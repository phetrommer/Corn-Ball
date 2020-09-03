using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelDisplay : MonoBehaviour
{
    private int level = 1;
    public TMP_Text levelText;

    void Update()
    {
        levelText.text = "LEVEL: " + level;
    }
}
