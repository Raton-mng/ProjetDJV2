using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class MainTests
{
    private Player _character;
    private Player _characterPrefab;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Tests/Scenes/VseauTest");

        yield return null;

        if (!_characterPrefab)
        {
            var loadHandle = Addressables.LoadAssetAsync<GameObject>("Assets/Player/Player.prefab");
            yield return loadHandle;
            _characterPrefab = loadHandle.Result.GetComponent<Player>();
        }

        _character = GameObject.Instantiate(_characterPrefab);
    }
    
    [UnityTest]
    public IEnumerator CharacterControllerMove()
    {
        var speed = 17f;
        float timer = 0;
        
        while (timer < 1)
        {
            _character.Move(Vector2.up, speed);
            timer += Time.deltaTime;
            yield return null;
        }

        Assert.That(_character.transform.position, Is.EqualTo(Vector3.forward * speed).Using(new Vector3EqualityComparer(0.5f)));
    }
}
