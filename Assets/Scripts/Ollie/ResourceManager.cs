using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    #region Singleton

    public static ResourceManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are two resourceManager scripts and this will cause a lot of problems, please remove one of them");
        }

        food = startFood;
        water = startWater;
        energy = startEnergy;
        sanity = startSanity;
    }

    #endregion

    [Header("UI Referrences")]
    [SerializeField] TextMeshProUGUI foodText;
    [SerializeField] TextMeshProUGUI waterText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI sanityText;

    [Header("Resources")]
    [SerializeField] int startFood;
    [SerializeField] int startWater;
    [SerializeField] int startEnergy;
    [SerializeField] int startSanity;

    public int food;
    public int water;
    public int energy;
    public int sanity;

    //returns true if game over, else returns false
    public void UpdateResourcesDisplay()
    {
        EnsureResourcesAreWithinCorrectRange();        

        //Update UI
        foodText.text = "Food: " + food.ToString(); 
        waterText.text = "Water: " + water.ToString(); 
        energyText.text = "Energy: " + energy.ToString(); 
        sanityText.text = "Sanity: " + sanity.ToString();        
    }

    void EnsureResourcesAreWithinCorrectRange()
    {
        if (food < 0)
        {
            food = 0;
        }
        else if(food > 100)
        {
            food = 100;
        }


        if (water < 0)
        {
            water = 0;
        }
        else if (water > 100)
        {
            water = 100;
        }


        if (energy < 0)
        {
            energy = 0;
        }
        else if (energy > 100)
        {
            energy = 100;
        }


        if (sanity < 0)
        {
            sanity = 0;
        }
        else if (sanity > 100)
        {
            sanity = 100;
        }


    }

    public bool CheckIfGameOver()
    {
        if (food == 0)
        {
            return true;
        }
        else if (water == 0)
        {
            return true;
        }
        else if (energy == 0)
        {
            return true;
        }
        else if (sanity == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
