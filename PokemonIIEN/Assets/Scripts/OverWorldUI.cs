using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverWorldUI : MonoBehaviour
{
    public static OverWorldUI Instance;
    
    [SerializeField] private Player player;

    [SerializeField] private List<GameObject> pokemonsUI;
    [SerializeField] private List<TextMeshProUGUI> pokemonsName;
    [SerializeField] private List<RectTransform> pokemonsHp;
    [SerializeField] private RectTransform cursor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateTeam();
    }

    public void UpdateTeam()
    {
        //on part du principe que l'UI a forcement prevu plus de pokemon que le joueur peut avoir
        List<Pokemon> list = player.GetTeam();
        
        int pokemonIndex;
        for (pokemonIndex = 0; pokemonIndex < list.Count; pokemonIndex++)
        {
            Pokemon pokemon = list[pokemonIndex];
            
            pokemonsUI[pokemonIndex].SetActive(true);
            pokemonsName[pokemonIndex].text = pokemon.nickname;
            pokemonsHp[pokemonIndex].anchorMax = new(pokemon.HpPourcentage(), pokemonsHp[pokemonIndex].anchorMax.y);
        }

        for (int i = pokemonIndex; i < pokemonsUI.Count; i++)
        {
            pokemonsUI[i].SetActive(false);
        }
        
        //Update cursor
        Pokemon mainPokemon = player.GetNonKoPokemon();
        for (int i = 0; i < list.Count; i++)
        {
            if (mainPokemon == list[i]) {
                cursor.anchoredPosition3D = new Vector3(cursor.anchoredPosition3D.x, 440 - 125 * i, cursor.anchoredPosition3D.z); //à rendre meilleur
                break;
            }
        }
    }
}
