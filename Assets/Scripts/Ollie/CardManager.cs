using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    #region Singleton 

    public static CardManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are two card managers, please remove one of them");
        }
    }


    #endregion

    public string mainMenuSceneString;
    ResourceManager resourceManager;
    TimeManager timeManager;

    public TextMeshProUGUI mainDescriptionText;
    public TextMeshProUGUI leftDescriptionText;
    public TextMeshProUGUI rightDescriptionText;

    [Header("Cards")]
    public CardSO[] cards;

    CardSO currentlySelectedCard = null;
    List<CardSO> usedCards;

    [Header("GameOverCards")]
    public CardSO foodGameOverCard;
    public CardSO waterGameOverCard;
    public CardSO energyGameOver;
    public CardSO sanityGameOver;

    [Header("WinCards")]
    public CardSO allCardsCompletedCard;
    public CardSO survivedAllDaysCard;

    bool choseLeft = false;
    bool choseRight = false;


    private void Start()
    {
        resourceManager = ResourceManager.instance;
        timeManager = TimeManager.instance;
        usedCards = new List<CardSO>();

        if(cards.Length == 0)
        {
            Debug.LogError("You have not added any cards to the array!");
        }
        else
        {
            ChooseNextCard();
        }
    }


    void ChooseNextCard()
    {
        resourceManager.UpdateResourcesDisplay();
        bool gameOver = resourceManager.CheckIfGameOver();
        bool hasSurvivedAllDays = timeManager.IncreaseTimeOfDay();
        leftDescriptionText.gameObject.SetActive(false);
        rightDescriptionText.gameObject.SetActive(false);

        if (gameOver)
        {
            //make currently selected card a gameOverCard

            if (resourceManager.food == 0)
            {
                currentlySelectedCard = foodGameOverCard;
            }
            else if(resourceManager.water == 0)
            {
                currentlySelectedCard = waterGameOverCard;
            }
            else if (resourceManager.energy == 0)
            {
                currentlySelectedCard = energyGameOver;
            }
            else if (resourceManager.sanity == 100)
            {
                currentlySelectedCard = sanityGameOver;
            }

        }
        else
        {
            if (hasSurvivedAllDays)
            {
                currentlySelectedCard = survivedAllDaysCard;
            }
            else
            {
                if (currentlySelectedCard == null)
                {
                    //Picks out random card from all the cards
                    ChooseNonFollowUpCard();
                }
                else
                {
                    if (choseLeft && currentlySelectedCard.leftFollowUpCards.Length > 0)
                    {
                        ChooseFollowUpCard(true);
                    }
                    else if (choseRight && currentlySelectedCard.rightFollowUpCards.Length > 0)
                    {
                        ChooseFollowUpCard(false);
                    }
                    else
                    {
                        //Picks out random card from all the cards
                        ChooseNonFollowUpCard();
                    }
                }
            }
            
        }

        choseLeft = false;
        choseRight = false;

        UpdateCardDisplay();

    }

    void ChooseFollowUpCard(bool left)
    {
        //if left = true then we choose from the left follow up cards
        //if left = false then we choose from the right follow up cards

        if (left)
        {
            int randFollowUpCardIndex = Random.Range(0, currentlySelectedCard.leftFollowUpCards.Length);
            currentlySelectedCard = currentlySelectedCard.leftFollowUpCards[randFollowUpCardIndex];
        }
        else
        {
            int randFollowUpCardIndex = Random.Range(0, currentlySelectedCard.rightFollowUpCards.Length);
            currentlySelectedCard = currentlySelectedCard.rightFollowUpCards[randFollowUpCardIndex];
        }
        
    }

    void ChooseNonFollowUpCard()
    {
        int randCardIndex = Random.Range(0, cards.Length);
        CardSO nextCard = cards[randCardIndex];

        if (currentlySelectedCard != null)
        {

            //Loop to ensure that the same card cannot appear again
            if(cards.Length == usedCards.Count)
            {
                //This means that we have looked at all of the cards and therefore the player can win
                nextCard = allCardsCompletedCard;                
            }
            else
            {
                int numTimeInWhileLoop = 0;

                while (nextCard == currentlySelectedCard || usedCards.Contains(nextCard))
                {
                    randCardIndex = Random.Range(0, cards.Length);
                    nextCard = cards[randCardIndex];

                    numTimeInWhileLoop += 1;
                    if(numTimeInWhileLoop > 100) //This is just to prevent crashes
                    {
                        Debug.LogError("THIS WOULD HAVE CRASHED, PLEASE CHECK ALL CODE IS WORKING CORRECTLY");
                        break;
                    }

                }
            }            
        }

        currentlySelectedCard = nextCard;
    }

    void UpdateCardDisplay()
    {
        mainDescriptionText.text = currentlySelectedCard.mainDescription;
        leftDescriptionText.text = currentlySelectedCard.leftDescription;
        rightDescriptionText.text = currentlySelectedCard.rightDescription;
    }

    public void LeftCardButtonPressed()
    {
        if (currentlySelectedCard.isGameOverCard)
        {
            SceneManager.LoadScene(mainMenuSceneString);
        }
        else
        {
            resourceManager.food += currentlySelectedCard.left_changeInFood;
            resourceManager.water += currentlySelectedCard.left_changeInWater;
            resourceManager.energy += currentlySelectedCard.left_changeInEnergy;
            resourceManager.sanity += currentlySelectedCard.left_changeInSanity;

            if (!currentlySelectedCard.isAFollowUpCard)
            {
                usedCards.Add(currentlySelectedCard);
            }

            choseLeft = true;
            ChooseNextCard();
        }        
    }

    public void RightCardButtonPressed()
    {
        if (currentlySelectedCard.isGameOverCard)
        {
            SceneManager.LoadScene(mainMenuSceneString);
        }
        else
        {
            resourceManager.food += currentlySelectedCard.right_changeInFood;
            resourceManager.water += currentlySelectedCard.right_changeInWater;
            resourceManager.energy += currentlySelectedCard.right_changeInEnergy;
            resourceManager.sanity += currentlySelectedCard.right_changeInSanity;

            //Need to ensure that if this is a follow on card that we do not add it to the list
            if (!currentlySelectedCard.isAFollowUpCard)
            {
                usedCards.Add(currentlySelectedCard);
            }            

            choseRight = true;
            ChooseNextCard();
        }        
    }
    
}
