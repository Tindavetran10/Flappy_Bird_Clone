using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    [Test]
    public void PlayerJump_AddsForce()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        Player playerScript = playerObject.AddComponent<Player>();

        Rigidbody2D mockRigidbody = playerObject.AddComponent<Rigidbody2D>();
        playerScript.rb = mockRigidbody;

        // Act
        playerScript.Jump();

        // Assert
        Assert.AreEqual(new Vector2(0, playerScript.force), mockRigidbody.velocity);
    }

    [UnityTest]
    public IEnumerator PlayerScoreCollision_IncreasesScore()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        Player playerScript = playerObject.AddComponent<Player>();

        GameManager mockGameManager = new GameObject().AddComponent<GameManager>();
        playerScript.gm = mockGameManager;

        GameObject scoreObject = new GameObject();
        scoreObject.tag = "Score";
        BoxCollider2D scoreCollider = scoreObject.AddComponent<BoxCollider2D>();
        scoreObject.AddComponent<AudioSource>(); // Assuming there's an AudioSource for the score sound.

        // Act
        playerScript.OnTriggerEnter2D(scoreCollider);

        // Yield to wait for the next frame (since OnTriggerEnter2D is typically called in FixedUpdate)
        yield return null;

        // Assert
        Assert.AreEqual(1, mockGameManager.score);
    }

    [UnityTest]
    public IEnumerator PlayerWallCollision_SetsGameOver()
    {
        // Arrange
        GameObject playerObject = new GameObject();
        Player playerScript = playerObject.AddComponent<Player>();

        GameManager mockGameManager = new GameObject().AddComponent<GameManager>();
        playerScript.gm = mockGameManager;

        GameObject wallObject = new GameObject();
        wallObject.tag = "Wall";
        BoxCollider2D wallCollider = wallObject.AddComponent<BoxCollider2D>();
        wallObject.AddComponent<AudioSource>(); // Assuming there's an AudioSource for the game over sound.

        // Act
        // Simulate a collision by moving the player towards the wall
        playerObject.transform.position = new Vector3(0, 0, 0);
        wallObject.transform.position = new Vector3(1, 0, 0);
        playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);

        // Wait for physics to simulate (1 frame)
        yield return null;

        // Assert
        Assert.IsTrue(mockGameManager.isGameOver);
    }
}
