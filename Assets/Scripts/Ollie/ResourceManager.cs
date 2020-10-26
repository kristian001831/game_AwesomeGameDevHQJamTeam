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



    public void UpdateResourcesDisplay()
    {
        hungerText.text = "Hunger: " + hunger.ToString(); 
        thirstText.text = "Thirst: " + thirst.ToString(); 
        shelterText.text = "Shelter: " + shelter.ToString(); 
        panicText.text = "Panic: " + panic.ToString(); 
    }

}
