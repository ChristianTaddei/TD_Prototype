using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoardPlayTest
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Level");
    }

    [UnityTest]
    public IEnumerator BoardPlayTestWithEnumeratorPasses()
    {
        GameObject terrain = GameObject.Find("Terrain");

        Assert.AreNotEqual(null, terrain);

        yield return null;
    }
}
