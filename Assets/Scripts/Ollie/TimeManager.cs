using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton

    public static TimeManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are 2 time managers please remove one of them");
        }
    }

    #endregion

    int timeOfDay = 0; //0 mean start of day, 1 means half way thorugh day, 2 means evening, 3 means night then loop back to zero 
    int amtDaysSurvived;
    [SerializeField] int daysToSurviveBeforeEscape;

    [Header("Lighting")]
    [SerializeField] Material skyBoxMorning;
    [SerializeField] Material skyBoxMidDay;
    [SerializeField] Material skyBoxEvening;
    [SerializeField] Material skyBoxNight;

    private void Start()
    {
        RenderSettings.skybox = skyBoxNight;
    }

    public bool IncreaseTimeOfDay()
    {
        if(timeOfDay == 3)
        {
            //This means that we have completed one day
            timeOfDay = 0;
            amtDaysSurvived += 1;
            ResourceManager.instance.energy = 80; // its a new day so the user would have slept and hence gained back their energy

            if(amtDaysSurvived >= daysToSurviveBeforeEscape)
            {
                //Then the user wins the game
                return true;
            }

        }
        else
        {
            timeOfDay += 1;
        }

        return false;
    }

}
