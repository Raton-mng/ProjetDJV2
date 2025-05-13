using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Moves;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    //differents menus
    [SerializeField] private GameObject moveUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject itemOnAllyUI;
    [SerializeField] private GameObject itemOnEnemyUI;
    private GameObject _currentUI;
        
    //differentes sous-partie du plateau
    [HideInInspector] public Pokemon playerPokemon;
    [SerializeField] private TextMeshProUGUI playerName;
    [HideInInspector] public Pokemon enemyPokemon;
    [SerializeField] private TextMeshProUGUI enemyName;
    
    //barres d'Hp
    [SerializeField] private GameObject playerHpParent;
    [SerializeField] private RectTransform playerHpBar;
    [SerializeField] private GameObject enemyHpParent;
    [SerializeField] private RectTransform enemyHpBar; 
    
    //invetaire du joueur
    private Dictionary<PokeItem, int> _items;
    
    //liste des Items possible
    [SerializeField] private Transform itemOnAllyButtonsParent;
    [SerializeField] private Transform itemOnEnemyButtonsParent;
    private List<GameObject> _itemButtons;
    
    //liste des Move possible
    [SerializeField] private Transform moveButtonsParent;
    private List<GameObject> _moveButtons;
    public Move selectedMove;
    
    //prefab a instancier
    [SerializeField] private Button button;
    
    //pour finir le tour
    public bool actionSelected;
    
    public void Initialize(Pokemon playerPoke, Pokemon enemyPoke, Dictionary<PokeItem, int> items)
    {
        _currentUI = mainUI;
        
        enemyName.gameObject.SetActive(true);
        enemyHpParent.gameObject.SetActive(true);
        
        playerName.gameObject.SetActive(true);
        playerHpParent.gameObject.SetActive(true);
        
        _moveButtons = new List<GameObject>();
        UpdateEnemyPokemon(enemyPoke);
        UpdatePlayerPokemon(playerPoke);

        _items = items;
        _itemButtons = new List<GameObject>();
        UpdateInventory();
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
        
        foreach (GameObject moveButton in _moveButtons)
        {
            Destroy(moveButton);
        }
        _moveButtons.Clear();
        
        foreach (Move move in playerPokemon.Moves)
        {
            Button moveButton = Instantiate(button, moveButtonsParent);
            moveButton.GetComponentInChildren<TextMeshProUGUI>().text = move.moveName;
            moveButton.onClick.AddListener(() => SelectMove(move));
            _moveButtons.Add(moveButton.gameObject);
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

    public void ChooseAction()
    {
        if (_currentUI == null) return; //fix de la fin de combat après capture pour pas se casser la tête
        
        _currentUI.SetActive(false);
        mainUI.SetActive(true);
        _currentUI = mainUI;
    }

    public void ChooseMove()
    {
        _currentUI.SetActive(false);
        moveUI.SetActive(true);
        _currentUI = moveUI;
    }

    private void SelectMove(Move move)
    {
        selectedMove = move;
        actionSelected = true;
    }

    public void ChooseItem()
    {
        _currentUI.SetActive(false);
        itemUI.SetActive(true);
        _currentUI = itemUI;
    }
    
    public void ChooseItemOnAlly()
    {
        _currentUI.SetActive(false);
        itemOnAllyUI.SetActive(true);
        _currentUI = itemOnAllyUI;
    }
    
    public void ChooseItemOnEnemy()
    { 
        _currentUI.SetActive(false);
        itemOnEnemyUI.SetActive(true);
        _currentUI = itemOnEnemyUI;
    }

    private void UpdateInventory()
    {
        foreach (GameObject itemButton in _itemButtons)
        {
            Destroy(itemButton);
        }
        _itemButtons.Clear();
        
        foreach (var item in _items)
        {
            if (item.Key is ItemOnAlly ioa)
            {
                Button itemButton = Instantiate(button, itemOnAllyButtonsParent);
                itemButton.GetComponentInChildren<TextMeshProUGUI>().text = ioa.itemName;
                itemButton.onClick.AddListener(() => SelectItem(ioa.UseOnAlly, playerPokemon, ioa));
                _itemButtons.Add(itemButton.gameObject);
            }
            else if (item.Key is ItemOnEnemy ioe)
            {
                Button itemButton = Instantiate(button, itemOnEnemyButtonsParent);
                itemButton.GetComponentInChildren<TextMeshProUGUI>().text = ioe.itemName;
                itemButton.onClick.AddListener(() => SelectItem(ioe.UseOnEnemy, enemyPokemon, ioe));
                _itemButtons.Add(itemButton.gameObject);
            }
        }
    }
    
    private void SelectItem(Func<Pokemon, bool> useItem, Pokemon target, PokeItem item)
    {
        bool useWorked = useItem(target);
        if (useWorked)
        {
            _items[item] -= 1;
            if (_items[item] <= 0)
            {
                _items.Remove(item);
                UpdateInventory();
            }
            actionSelected = true;
        }
    }
}
