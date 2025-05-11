using System.Collections;
using UnityEngine;

public class PokemonCenter : MonoBehaviour
{
    [SerializeField] private GameObject pokeCenterCanvas;
    [SerializeField] private Transform respawnPoint;
    private bool _inDelay;
    private float _interactionDelay = 1f;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player) && !_inDelay)
        {
            Debug.Log("Pokemon Center");
            player.HealAllPokemons();
            pokeCenterCanvas.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void ExitPokemonCenter()
    {
        StartCoroutine(Delay());
        pokeCenterCanvas.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private IEnumerator Delay()
    {
        _inDelay = true;
        yield return new WaitForSeconds(_interactionDelay);
        _inDelay = false;
    }
}
