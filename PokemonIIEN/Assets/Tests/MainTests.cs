using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class MainTests
{
    private Player _characterPrefab;
    private Player _character;
    private Trainer _trainerPrefab;
    private Trainer _trainer;
    private PokemonCenter _pokemonCenterPrefab;
    private PokemonCenter _pokemonCenter;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadScene("Tests/Scenes/RatonTest");

        yield return null;

        if (!_characterPrefab)
        {
            var loadHandle = Addressables.LoadAssetAsync<GameObject>("Assets/Player/Player.prefab");
            yield return loadHandle;
            _characterPrefab = loadHandle.Result.GetComponent<Player>();
        }

        if (!_trainerPrefab)
        {
            var loadHandle = Addressables.LoadAssetAsync<GameObject>("Assets/Enemies/Enemy1.prefab");
            yield return loadHandle;
            _trainerPrefab = loadHandle.Result.GetComponent<Trainer>();
        }

        if (!_pokemonCenterPrefab)
        {
            var loadHandle = Addressables.LoadAssetAsync<GameObject>("Assets/Buildings/PokemonCenter.prefab");
            yield return loadHandle;
            _pokemonCenterPrefab = loadHandle.Result.GetComponent<PokemonCenter>();       
        }

        _character = GameObject.Instantiate(_characterPrefab);
        _character.inTest = true;
    }
    
    [UnityTest]
    public IEnumerator CharacterControllerMove()
    {
        float timer = 0f;

        while (timer < 1f)
        {
            _character.Move(Vector2.up, 20);
            timer += Time.deltaTime;
            yield return null;
        }

        Assert.That(_character.transform.position, Is.EqualTo(new Vector3(19, 1, -6)).Using(new Vector3EqualityComparer(0.5f)));
    }

    [UnityTest]
    public IEnumerator CheckFightStart()
    {
        _trainer = GameObject.Instantiate(_trainerPrefab, _character.transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.1f);// Real Time to bypass Time.TimeScale = 0
        GameObject combatUI = GameObject.Find("CombatUI(Clone)");
        Time.timeScale = 1;//Since Combat set it to 0, revert it for the next test
        Assert.That(combatUI.activeSelf, Is.True);
    }
    
    [UnityTest]
    public IEnumerator CheckPokemonCenter()
    {
        _pokemonCenter = GameObject.Instantiate(_pokemonCenterPrefab, _character.transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.1f);
        
        GameObject pokemonCenterUI = GameObject.Find("PokeCenterUI");
        Time.timeScale = 1;
        Assert.That(pokemonCenterUI.activeSelf, Is.True);
        Assert.That(_character.respawnPoint, Is.EqualTo(_pokemonCenter.respawnPoint));
        
        _character.Respawn();
        yield return null;
        Assert.That(_character.transform.position, Is.EqualTo(_pokemonCenter.respawnPoint.position));
    }
}
