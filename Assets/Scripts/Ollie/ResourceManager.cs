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

        hunger = startHunger;
        thirst = startThirst;
        shelter = startShelter;
        panic = startPanic;
    }

    #endregion

    [Header("UI Referrences")]
    [SerializeField] TextMeshProUGUI hungerText;
    [SerializeField] TextMeshProUGUI thirstText;
    [SerializeField] TextMeshProUGUI shelterText;
    [SerializeField] TextMeshProUGUI panicText;

    [Header("Resources")]
    [SerializeField] int startHunger;
    [SerializeField] int startThirst;
    [SerializeField] int startShelter;
    [SerializeField] int startPanic;

    public int hunger;
    public int thirst;
    public int shelter;
    public int panic;

    //returns true if game over, else returns false
    public void UpdateResourcesDisplay()
    {
        EnsureResourcesAreWithinCorrectRange();        

        //Update UI
        hungerText.text = "Hunger: " + hunger.ToString(); 
        thirstText.text = "Thirst: " + thirst.ToString(); 
        shelterText.text = "Shelter: " + shelter.ToString(); 
        panicText.text = "Panic: " + panic.ToString();        
    }

    void EnsureResourcesAreWithinCorrectRange()
    {
        if (hunger < 0)
        {
            hunger = 0;
        }
        else if(hunger > 100)
        {
            hunger = 100;
        }


        if (thirst < 0)
        {
            thirst = 0;
        }
        else if (thirst > 100)
        {
            thirst = 100;
        }


        if (shelter < 0)
        {
            shelter = 0;
        }
        else if (shelter > 100)
        {
            shelter = 100;
        }


        if (panic < 0)
        {
            panic = 0;
        }
        else if (panic > 100)
        {
            panic = 100;
        }


    }

    public bool CheckIfGameOver()
    {
        if (hunger == 0)
        {
            return true;
        }
        else if (thirst == 0)
        {
            return true;
        }
        else if (shelter == 0)
        {
            return true;
        }
        else if (panic == 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
