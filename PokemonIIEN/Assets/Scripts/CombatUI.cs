using System;
using System.Collections;
using System.Collections.Generic;
using Moves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    //differentes sous-partie du plateau
    [HideInInspector] public Pokemon enemyPokemon;
    [HideInInspector] public Pokemon playerPokemon;
    [HideInInspector] public CombatManager combatManager;

    [SerializeField] public Button moveButton;
    private List<GameObject> _buttons;
    
    public void Something()
    {
        _buttons = new List<GameObject>();
        
        foreach (Move move in playerPokemon.Moves)
        {
            Button button = Instantiate(moveButton, transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = move.moveName;
            button.onClick.AddListener(() => SelectMove(move));
            _buttons.Add(button.gameObject);
            button.gameObject.SetActive(false);
        }
    }

    public void ChooseMove()
    {
        foreach (GameObject button in _buttons)
        {
            button.SetActive(true);
        }
    }

    private void SelectMove(Move move)
    {
        combatManager.selectedMove = move;
        foreach (GameObject button in _buttons)
        {
            button.SetActive(false);
        }
    }
}
