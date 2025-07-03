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
    [Header("Menus")]
    [SerializeField] private GameObject moveUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject teamUI;
    [SerializeField] private GameObject itemOnAllyUI;
    [SerializeField] private GameObject itemOnEnemyUI;
    [Header("Combat Text")]
    [SerializeField] private GameObject moveText;
    [SerializeField] private TextMeshProUGUI textToFill;
    [HideInInspector] public GameObject _currentUI;
        
    //differentes sous-partie du plateau
    [HideInInspector] public Pokemon playerPokemon;
    [Space(10)] [Header("Sous-partie du plateau")] [SerializeField] private TextMeshProUGUI playerName;
    [HideInInspector] public Pokemon enemyPokemon;
    [SerializeField] private TextMeshProUGUI enemyName;
    
    //barres d'Hp
    [Space(10)] [Header("Barres d'Hp")]
    [SerializeField] private GameObject playerHpParent;
    [SerializeField] private RectTransform playerHpBar;
    [SerializeField] private TextMeshProUGUI playerHpNumber;
    [SerializeField] private GameObject enemyHpParent;
    [SerializeField] private RectTransform enemyHpBar; 
    [SerializeField] private TextMeshProUGUI enemyHpNumber;
    
    //boosts
    [Space(10)] [Header("Boosts")]
    [SerializeField] private TextMeshProUGUI playerBoost;
    [SerializeField] private TextMeshProUGUI enemyBoost;
    
    //invetaire du joueur
    private Dictionary<PokeItem, int> _items;
    private List<Pokemon> _playerTeam;
    private List<GameObject> _teamButtons;
    [Space(10)] [Header("Inventaire du joueur")]
    [SerializeField] private Transform teamButtonsParent;
    
    //invetaire de l'ennemie
    private List<Pokemon> _enemyTeam;
    
    //liste des Items possible
    [SerializeField] private Transform itemOnAllyButtonsParent;
    [SerializeField] private Transform itemOnEnemyButtonsParent;
    private List<GameObject> _itemButtons;
    
    //liste des Move possible
    [Space(10)] [Header("Liste des moves")]
    [SerializeField] private Transform moveButtonsParent;
    private List<GameObject> _moveButtons;
    public Move selectedMove;
    
    //prefab a instancier
    [Space(10)] [Header("Prefab Boutton")]
    [SerializeField] private Button button;
    
    //pour finir le tour
    [HideInInspector] public UnityEvent<Pokemon> playerPokemonSwitched;
    [HideInInspector] public UnityEvent<Pokemon> enemyPokemonSwitched;
    [HideInInspector] public bool actionSelected;
    
    public void Initialize(Player player, Enemy enemy, Dictionary<PokeItem, int> items)
    {
        playerPokemonSwitched = new UnityEvent<Pokemon>();
        enemyPokemonSwitched = new UnityEvent<Pokemon>();
        
        _currentUI = mainUI;
        
        enemyName.gameObject.SetActive(true);
        enemyHpParent.gameObject.SetActive(true);
        
        playerName.gameObject.SetActive(true);
        playerHpParent.gameObject.SetActive(true);
        
        _moveButtons = new List<GameObject>();
        UpdateEnemyPokemon(enemy.GetNonKoPokemon());
        UpdatePlayerPokemon(player.GetNonKoPokemon());

        _items = items;
        _itemButtons = new List<GameObject>();
        UpdateInventory();

        _playerTeam = player.GetTeam();
        _teamButtons = new List<GameObject>();
        _enemyTeam = enemy.GetTeam();
        
        UpdateBoost(playerPokemon);
        UpdateBoost(enemyPokemon);
    }
    public void UpdateEnemyPokemon(Pokemon pokemon)
    {
        if (enemyPokemon != null) enemyPokemon.hpChanged.RemoveListener(UpdateEnemyHealth);

        enemyPokemon = pokemon;
        enemyName.text = enemyPokemon.nickname;
        enemyPokemon.hpChanged.AddListener(UpdateEnemyHealth);
        UpdateEnemyHealth(enemyPokemon.HpPourcentage());
        UpdateBoost(enemyPokemon);
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
        UpdateBoost(playerPokemon);
    }

    private void UpdatePlayerHealth(float healthRatio)
    {
        playerHpBar.anchorMax = new(healthRatio, playerHpBar.anchorMax.y);
        playerHpNumber.text = playerPokemon.CurrentHp + "/" + playerPokemon.GetBaseHp();
    }
    
    private void UpdateEnemyHealth(float healthRatio)
    {
        enemyHpBar.anchorMax = new(healthRatio, enemyHpBar.anchorMax.y);
        enemyHpNumber.text = enemyPokemon.CurrentHp + "/" + enemyPokemon.GetBaseHp();
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

    private void SwitchPlayerPokemon(Pokemon newcomer)
    {
        UpdatePlayerPokemon(newcomer);
        playerPokemonSwitched.Invoke(newcomer);
        
        actionSelected = true;
    }
    
    private void SwitchEnemyPokemon(Pokemon newcomer)
    {
        UpdateEnemyPokemon(newcomer);
        enemyPokemonSwitched.Invoke(newcomer);
        
        actionSelected = true;
    }
    
    public void ChoosePokemon()
    {
        CreateTeamUI();
        _currentUI.SetActive(false);
        teamUI.SetActive(true);
        _currentUI = teamUI;
    }
    
    private void CreateTeamUI()
    {
        foreach (GameObject pokeButton in _teamButtons)
        {
            Destroy(pokeButton);
        }
        _teamButtons.Clear();
        
        foreach (Pokemon pokemon in _playerTeam)
        {
            if (pokemon == playerPokemon || pokemon.CurrentHp <= 0) continue;
            Button pokeButton = Instantiate(button, teamButtonsParent);
            pokeButton.GetComponentInChildren<TextMeshProUGUI>().text = pokemon.nickname;
            pokeButton.onClick.AddListener(() => SwitchPlayerPokemon(pokemon));
            _teamButtons.Add(pokeButton.gameObject);
        }
    }

    public void DisplayText(string text)
    {
        _currentUI.SetActive(false);
        moveText.SetActive(true);
        textToFill.text = text;
        _currentUI = moveText;
    }

    public void CloseText()
    {
        moveText.SetActive(false);
        actionSelected = true;
    }

    public void UpdateBoost(Pokemon target)
    {
        if (target == playerPokemon)
        {
            playerBoost.text = playerPokemon.actualBoostAttack + " | " + playerPokemon.actualBoostDefense + " | " + playerPokemon.actualBoostSpeed;
        }
        else
        {
            enemyBoost.text = enemyPokemon.actualBoostAttack + " | " + enemyPokemon.actualBoostDefense + " | " + enemyPokemon.actualBoostSpeed;
        }
    }
}
