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
    public IEnumerator Setup()
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

        Assert.That(_character.transform.position, Is.EqualTo(new Vector3(-57, 1, -10)).Using(new Vector3EqualityComparer(0.5f)));
    }
}
