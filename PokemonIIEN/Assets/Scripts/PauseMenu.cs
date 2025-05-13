using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private InputAction _pauseAction;

    [SerializeField] private GameObject pauseUI;

    private void Start()
    {
        _pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (_pauseAction.triggered) Pause();
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
