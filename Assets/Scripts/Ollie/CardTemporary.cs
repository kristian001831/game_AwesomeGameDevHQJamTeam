using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardTemporary : MonoBehaviour, IDragHandler, IEndDragHandler
{
    CardManager cardManagerReferrence;
    ResourceManager resourceManagerReferrence;

    [Header("Card Movement")]
    public float maxXPos;

    [SerializeField] mouseOver leftSwipe;
    [SerializeField] mouseOver rightSwipe;

    [Header("Card Rotation")]
    [SerializeField] float maxZRotation;
    [SerializeField] float rotationMultiplier;

    [Header("Circle Variables")]
    [SerializeField] float circleSizeScaleFactor;
    [SerializeField] float maxCircleScaleFactor;


    RectTransform canvas;
    float canvasXOffset;

    bool adjustedCircleSizes;

    private void Start()
    {
        cardManagerReferrence = CardManager.instance;
        resourceManagerReferrence = ResourceManager.instance;
        canvas = CanvasSingleton.instance.GetComponent<RectTransform>();
        canvasXOffset = canvas.position.x;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Rotation of card

        float differenceBtwMouseAndTransform = Input.mousePosition.x - transform.position.x;

        float distanceToMousePointer = (float)(differenceBtwMouseAndTransform / 100f);

        float zRotation = -1 * Mathf.Clamp(distanceToMousePointer * rotationMultiplier, -maxZRotation, maxZRotation);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, zRotation);


        //Movement of the card

        transform.position = new Vector3(Input.mousePosition.x + canvasXOffset, transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x - canvasXOffset, -maxXPos + canvasXOffset, maxXPos + canvasXOffset), transform.position.y, transform.position.z);

        bool isMouseOverLeftSwipe = leftSwipe.isMouseOver;
        bool isMouseOverRightSwipe = rightSwipe.isMouseOver;

        if (isMouseOverLeftSwipe)
        {
            cardManagerReferrence.leftDescriptionText.transform.parent.gameObject.SetActive(true);
            AdjustCircleSizes(true);
        }
        else if (isMouseOverRightSwipe)
        {
            cardManagerReferrence.rightDescriptionText.transform.parent.gameObject.SetActive(true);
            AdjustCircleSizes(false);
        }
        else
        {
            ResetCircleSizes();
        }


        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Reset transform
        transform.position = new Vector3(canvasXOffset, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0);


        //Check if it was swiped and what side it was swiped to
        bool isMouseOverLeftSwipe = leftSwipe.isMouseOver;
        bool isMouseOverRightSwipe = rightSwipe.isMouseOver;

        if (isMouseOverLeftSwipe)
        {
            cardManagerReferrence.LeftCardButtonPressed();
        }
        else if (isMouseOverRightSwipe)
        {
            cardManagerReferrence.RightCardButtonPressed();
        }

        //Reset circles
        ResetCircleSizes();

    }

    void AdjustCircleSizes(bool leftSwipe)
    {
        CardSO currentCard = cardManagerReferrence.GetCurrentCard();


        if (!adjustedCircleSizes)
        {
            if (leftSwipe)
            {
                float foodScale = Mathf.Clamp(Mathf.Abs(currentCard.left_changeInFood) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float waterScale = Mathf.Clamp(Mathf.Abs(currentCard.left_changeInWater) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float energyScale = Mathf.Clamp(Mathf.Abs(currentCard.left_changeInEnergy) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float sanityScale = Mathf.Clamp(Mathf.Abs(currentCard.left_changeInSanity) * circleSizeScaleFactor, 0, maxCircleScaleFactor);

                resourceManagerReferrence.foodCircle.localScale = new Vector3(foodScale, foodScale, foodScale);
                resourceManagerReferrence.waterCircle.localScale = new Vector3(waterScale, waterScale, waterScale);
                resourceManagerReferrence.energyCircle.localScale = new Vector3(energyScale, energyScale, energyScale);
                resourceManagerReferrence.sanityCircle.localScale = new Vector3(sanityScale, sanityScale, sanityScale);
            }
            else
            {
                float foodScale = Mathf.Clamp(Mathf.Abs(currentCard.right_changeInFood) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float waterScale = Mathf.Clamp(Mathf.Abs(currentCard.right_changeInWater) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float energyScale = Mathf.Clamp(Mathf.Abs(currentCard.right_changeInEnergy) * circleSizeScaleFactor, 0, maxCircleScaleFactor);
                float sanityScale = Mathf.Clamp(Mathf.Abs(currentCard.right_changeInSanity) * circleSizeScaleFactor, 0, maxCircleScaleFactor);

                resourceManagerReferrence.foodCircle.localScale = new Vector3(foodScale, foodScale, foodScale);
                resourceManagerReferrence.waterCircle.localScale = new Vector3(waterScale, waterScale, waterScale);
                resourceManagerReferrence.energyCircle.localScale = new Vector3(energyScale, energyScale, energyScale);
                resourceManagerReferrence.sanityCircle.localScale = new Vector3(sanityScale, sanityScale, sanityScale);
            }

            resourceManagerReferrence.foodCircle.gameObject.SetActive(true);
            resourceManagerReferrence.waterCircle.gameObject.SetActive(true);
            resourceManagerReferrence.energyCircle.gameObject.SetActive(true);
            resourceManagerReferrence.sanityCircle.gameObject.SetActive(true);

            adjustedCircleSizes = true;
        }

        

    }

    void ResetCircleSizes()
    {
        cardManagerReferrence.leftDescriptionText.transform.parent.gameObject.SetActive(false);
        cardManagerReferrence.rightDescriptionText.transform.parent.gameObject.SetActive(false);

        resourceManagerReferrence.foodCircle.gameObject.SetActive(false);
        resourceManagerReferrence.waterCircle.gameObject.SetActive(false);
        resourceManagerReferrence.energyCircle.gameObject.SetActive(false);
        resourceManagerReferrence.sanityCircle.gameObject.SetActive(false);

        resourceManagerReferrence.foodCircle.localScale = new Vector3(1, 1, 1);
        resourceManagerReferrence.waterCircle.localScale = new Vector3(1, 1, 1);
        resourceManagerReferrence.energyCircle.localScale = new Vector3(1, 1, 1);
        resourceManagerReferrence.sanityCircle.localScale = new Vector3(1, 1, 1);

        adjustedCircleSizes = false;
    }


}
