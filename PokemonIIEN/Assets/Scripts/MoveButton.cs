using System.Collections;
using System.Collections.Generic;
using Moves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI powText;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI moveNameText;
    public Button button; //à sérialiser

    [Header("Icons of the type")] 
    [SerializeField] private Sprite piixel;
    [SerializeField] private Sprite filigrane;
    [SerializeField] private Sprite arise;
    [SerializeField] private Sprite lanPartiie;
    [SerializeField] private Sprite siieste;
    [SerializeField] private Sprite bde;
    [SerializeField] private Sprite bar;
    [SerializeField] private Sprite bakaclub;
    [SerializeField] private Sprite itv;
    [SerializeField] private Sprite smashiie;
    
    [Header("Types")] 
    [SerializeField] private Type piixelType;
    [SerializeField] private Type filigraneType;
    [SerializeField] private Type ariseType;
    [SerializeField] private Type lanPartiieType;
    [SerializeField] private Type siiesteType;
    [SerializeField] private Type bdeType;
    [SerializeField] private Type barType;
    [SerializeField] private Type bakaclubType;
    [SerializeField] private Type itvType;
    [SerializeField] private Type smashiieType;

    public void ButtonDisplay(Move move)
    {
        if (move is Attack attack)
        {
            powText.text = attack.BasePower + " POW";
        }
        else
        {
            powText.text = "- -";
        }

        moveNameText.text = move.moveName;
        moveText.text = move.moveDescription;

        if (move.MoveType == piixelType)
            typeIcon.sprite = piixel;
        else if (move.MoveType == filigraneType)
            typeIcon.sprite = filigrane;
        else if (move.MoveType == ariseType)
            typeIcon.sprite = arise;
        else if (move.MoveType == lanPartiieType)
            typeIcon.sprite = lanPartiie;
        else if (move.MoveType == siiesteType)
            typeIcon.sprite = siieste;
        else if (move.MoveType == bdeType)
            typeIcon.sprite = bde;
        else if (move.MoveType == barType)
            typeIcon.sprite = bar;
        else if (move.MoveType == bakaclubType)
            typeIcon.sprite = bakaclub;
        else if (move.MoveType == itvType)
            typeIcon.sprite = itv;
        else if (move.MoveType == smashiieType)
            typeIcon.sprite = smashiie;
    }
}
