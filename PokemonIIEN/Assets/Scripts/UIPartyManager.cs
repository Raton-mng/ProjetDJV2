using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIPartyManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RectTransform cursor;
    private int currentCursorNumber;

    [SerializeField] private List<GameObject> pokemonButtonsGo; //jsp pk j'arrive pas à recuperer les go donc solutionn temporaire

    private void OnEnable()
    {
        //part du principe que les bouttons sont bien ordonnés
        List<Pokemon> list = player.GetTeam();
        
        int pokemonIndex;
        for (pokemonIndex = 0; pokemonIndex < list.Count; pokemonIndex++)
        {
            int i = pokemonIndex;
            GameObject buttonGo = pokemonButtonsGo[pokemonIndex];
            buttonGo.SetActive(true);
            Button button =  buttonGo.GetComponent<Button>();
            button.onClick.AddListener(() => MoveCursor(i));
        }

        for (int i = pokemonIndex; i < pokemonButtonsGo.Count; i++)
        {
            pokemonButtonsGo[i].SetActive(false);
        }
        
        MoveCursor(0);
    }

    public void MoveCursor(int number)
    {
        if (number >= player.GetTeam().Count) return;
        
        currentCursorNumber = number;
        cursor.anchoredPosition3D = new Vector3(cursor.anchoredPosition3D.x, 440 - 125 * number, cursor.anchoredPosition3D.z); //à rendre meilleur
    }

    public void SelectNewMainPokemon()
    {
        List<Pokemon> team = player.GetTeam();
        Pokemon newMainPokemon = team[currentCursorNumber];
        team.Remove(newMainPokemon);
        team.Insert(0,newMainPokemon);
        
        OverWorldUI.Instance.UpdateTeam();
        
        currentCursorNumber = 0;
        cursor.anchoredPosition3D = new Vector3(cursor.anchoredPosition3D.x, 440 - 125 * currentCursorNumber, cursor.anchoredPosition3D.z); //à rendre meilleur
    }
}
