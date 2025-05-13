using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private InputAction _pauseAction;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingsUI;

    public void SwitchPanel()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        //Debug.Log(settingsUI.activeSelf);
        settingsUI.SetActive(!settingsUI.activeSelf);
        //Debug.Log(settingsUI.activeSelf);
    }

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
        if (pauseUI.activeSelf || settingsUI.activeSelf)
        {
            Unpause();
            return;
        }
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1;
    }
}
