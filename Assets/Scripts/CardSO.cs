using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardSO : ScriptableObject
{
    [Header("GeneralCardInfo")]
    public Sprite cardSprite;
    public string mainDescription;

    public string leftDescription;
    public string rightDescription;

    [Header("FollowUpCards - (you can leave this blank if it has no follow up cards)")]
    public CardSO[] leftFollowUpCards;
    public CardSO[] rightFollowUpCards;

    [Header("LeftResult")]
    public int left_changeInHunger;
    public int left_changeInThirst;
    public int left_changeInShelter;
    public int left_changeInPanic;

    [Header("RightResult")]
    public int right_changeInHunger;
    public int right_changeInThirst;
    public int right_changeInShelter;
    public int right_changeInPanic;

    [Header("MiniGame - (you can leave this blank if it is not a minigame card)")]
    public bool isMinigameCard;
    public string miniGameSceneString;


    [Header("GameOverCard - (you can leave this blank if it is not a game over card)")]
    public bool isGameOverCard;


}
