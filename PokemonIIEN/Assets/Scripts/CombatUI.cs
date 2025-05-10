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
    [HideInInspector] public Pokemon playerPokemon;
    [SerializeField] private TextMeshProUGUI playerName;
    [HideInInspector] public Pokemon enemyPokemon;
    [SerializeField] private TextMeshProUGUI enemyName;

    [SerializeField] private Transform buttonsParent;
    [SerializeField] private GameObject playerHpParent;
    [SerializeField] private RectTransform playerHpBar;
    [SerializeField] private GameObject enemyHpParent;
    [SerializeField] private RectTransform enemyHpBar;
    
    //prefab a instancier
    [SerializeField] private Button moveButton;
    private List<GameObject> _buttons;
    public Move selectedMove;

    public void Initialize(Pokemon playerPoke, Pokemon enemyPoke)
    {
        _buttons = new List<GameObject>();
        
        enemyName.gameObject.SetActive(true);
        enemyHpParent.gameObject.SetActive(true);
        
        playerName.gameObject.SetActive(true);
        playerHpParent.gameObject.SetActive(true);
        
        UpdateEnemyPokemon(enemyPoke);
        UpdatePlayerPokemon(playerPoke);
    }
    public void UpdateEnemyPokemon(Pokemon pokemon)
    {
        if (enemyPokemon != null) enemyPokemon.hpChanged.RemoveListener(UpdateEnemyHealth);

        enemyPokemon = pokemon;
        enemyName.text = enemyPokemon.nickname;
        enemyPokemon.hpChanged.AddListener(UpdateEnemyHealth);
        UpdateEnemyHealth(enemyPokemon.HpPourcentage());
    }
    
    public void UpdatePlayerPokemon(Pokemon pokemon)
    {
        if (playerPokemon != null) playerPokemon.hpChanged.RemoveListener(UpdatePlayerHealth);
        
        playerPokemon = pokemon;
        playerName.text = pokemon.nickname;
        playerPokemon.hpChanged.AddListener(UpdatePlayerHealth);
        UpdatePlayerHealth(playerPokemon.HpPourcentage());
        
        foreach (GameObject button in _buttons)
        {
            Destroy(button);
        }
        _buttons.Clear();
        
        foreach (Move move in playerPokemon.Moves)
        {
            Button button = Instantiate(moveButton, buttonsParent);
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
        selectedMove = move;
        foreach (GameObject button in _buttons)
        {
            button.SetActive(false);
        }
    }

    private void UpdatePlayerHealth(float healthRatio)
    {
        playerHpBar.anchorMax = new(healthRatio, playerHpBar.anchorMax.y);
    }
    
    private void UpdateEnemyHealth(float healthRatio)
    {
        enemyHpBar.anchorMax = new(healthRatio, playerHpBar.anchorMax.y);
    }
}
