using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ControlsUIBehaviour : MonoBehaviour
{
    //Text
    [SerializeField]
    private TMP_Text speedometerKM;
    [SerializeField]
    private TMP_Text speedometerKnots;
    float speed;

    [SerializeField]
    private Rigidbody _rb;




    [SerializeField]
    private Material speedLevel01Material;
    [SerializeField]
    private Material speedLevel02Material;
    [SerializeField]
    private Material speedLevel03Material;
    [SerializeField]
    private Material speedLevel04Material;

    [SerializeField]
    private Color offColour;

    private Color speedLevel01Colour;
    private Color speedLevel02Colour;
    private Color speedLevel03Colour;
    private Color speedLevel04Colour;

    // I can probably make this a lot more efficient and neater
    void Start()
    {
        // Store the colours
        speedLevel01Colour = speedLevel01Material.color;
        speedLevel02Colour = speedLevel02Material.color;
        speedLevel03Colour = speedLevel03Material.color;
        speedLevel04Colour = speedLevel04Material.color;

        // Set all colours to off
        UpdateSpeedLevel(-1f);
    }

    private void FixedUpdate()
    {
        // It's physics, so it's FixedUpdate
        //CalculateSpeed();
    
    }


    public void CalculateSpeed(float speed)
    {
        // Times 3.6 to convert from m/s to km/h
        speed = speed * 3.6f;
        speedometerKM.text = speed.ToString("F0") + " km/h";
        // Times 0.539957 to convert from km/h to knots
        speedometerKnots.text = (speed * 0.539957f).ToString("F0") + " knots";
    }

    public void UpdateSpeedLevel(float leverValue)
    {
        // Value between -1 0 and 1
        // normalised to 0 and 1
        // formula: (value - min) / (max - min)
        // 0 = no speed
        // 1 = max speed
        float normalisedLeverValue = (leverValue + 1) / 2;

        Debug.Log(leverValue);

        // there are 4 levels

        switch (normalisedLeverValue)
        {
            case > 0.75f:
                // 4th level
                //speedLevel04Material.color = speedLevel04Colour;
                speedLevel04Material.EnableKeyword("_EMISSION");
                break;
            case > 0.5f:
                // 3rd level
                //speedLevel03Material.color = speedLevel03Colour;
                speedLevel04Material.DisableKeyword("_EMISSION");
                speedLevel03Material.EnableKeyword("_EMISSION");


                // Turn off 4th level
                //speedLevel04Material.color = offColour;
                break;
            case > 0.25f:
                // 2nd level
                //speedLevel02Material.color = speedLevel02Colour;
                speedLevel03Material.DisableKeyword("_EMISSION");
                speedLevel02Material.EnableKeyword("_EMISSION");

                // Turn off 3rd level
                //speedLevel03Material.color = offColour;
                break;
            case > 0f:
                // 1st level
                //speedLevel01Material.color = speedLevel01Colour;
                speedLevel02Material.DisableKeyword("_EMISSION");
                speedLevel01Material.EnableKeyword("_EMISSION");

                // Turn off 2nd level
                //speedLevel02Material.color = offColour;
                break;
            default:
                // turn off all levels
                //speedLevel01Material.color = offColour;
                //speedLevel02Material.color = offColour;
                //speedLevel03Material.color = offColour;
                //speedLevel04Material.color = offColour;

                // turn off emission
                speedLevel01Material.DisableKeyword("_EMISSION");
                speedLevel02Material.DisableKeyword("_EMISSION");
                speedLevel03Material.DisableKeyword("_EMISSION");
                speedLevel04Material.DisableKeyword("_EMISSION");


                break;
        }
    }

    private void OnDisable()
    {
        // Reset all colours to original
        speedLevel01Material.color = speedLevel01Colour;
        speedLevel02Material.color = speedLevel02Colour;
        speedLevel03Material.color = speedLevel03Colour;
        speedLevel04Material.color = speedLevel04Colour;

        // turn on emission
        speedLevel01Material.EnableKeyword("_EMISSION");
        speedLevel02Material.EnableKeyword("_EMISSION");
        speedLevel03Material.EnableKeyword("_EMISSION");
        speedLevel04Material.EnableKeyword("_EMISSION");

        
    }



}
