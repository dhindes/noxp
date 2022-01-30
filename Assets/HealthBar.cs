using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image filledPart;
    public Image background;
    public TextMeshProUGUI bountyText;

    public void ShowHealthFraction(float fraction)
    {
        // Scale the filledPart to the fraction provided
        filledPart.rectTransform.localScale = new Vector3(fraction, 1, 1);
        if (fraction < 1)
        {
            filledPart.enabled = true;
            background.enabled = true;
        }
        else
        {
            filledPart.enabled = false;
            background.enabled = false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ask our healthsystem how much health it has, and set our health bar accordingly
    }
}
