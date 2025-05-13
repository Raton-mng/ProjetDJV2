using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> musicClips;

    private void Start()
    {
        CombatSingleton.Instance.onCombatStart.AddListener(EnterFight);
        CombatManager.onDefeat.AddListener(Defeat);
        CombatManager.onVictory.AddListener(Victory);
    }

    public void EnterOverWorld()
    {
        musicSource.clip = musicClips[0];
        musicSource.Play();
        musicSource.loop = true;
    }

    public void EnterFight()
    {
        musicSource.clip = musicClips[1];
        musicSource.Play();
        musicSource.loop = true;
    }
    
    public void Defeat()
    {
        musicSource.clip = musicClips[3];
        musicSource.Play();
        musicSource.loop = false;
        StartCoroutine(DefeatCoroutine());
    }

    private IEnumerator DefeatCoroutine()
    {
        yield return new WaitForSeconds(1f);
        EnterOverWorld();
    }
    
    public void Victory()
    {
        musicSource.clip = musicClips[2];
        musicSource.Play();
        musicSource.loop = false;
    }
}

