using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameManager gm;

    public float time;
    private bool canIncrease = true;
    
    public float spawnInterval = 2f;
    public float wallSpeed = 2f;
    public float destroyXPosition = -10f;

    private float timer;
    private readonly List<Wall> walls = new();
    
    private void Update()
    {
        CheckIncrease();
        
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }

        MovePipes();
    }

    private void CheckIncrease()
    {
        switch (gm.score)
        {
            case 20 when canIncrease:
                canIncrease = false;
                IncreaseTime();
                break;
            case 40 when canIncrease:
                canIncrease = false;
                IncreaseTime();
                break;
            case 60 when canIncrease:
                canIncrease = false;
                IncreaseTime();
                break;
            case 80 when canIncrease:
                canIncrease = false;
                IncreaseTime();
                break;
            case 100 when canIncrease:
                canIncrease = false;
                IncreaseTime();
                break;
        }
    }

    private void IncreaseTime()
    {
        if (time >= 1.2f)
        {
            time -= 0.2f;
            StartCoroutine(ResetIncrease());
        }
    }

    private IEnumerator ResetIncrease()
    {
        yield return new WaitForSeconds(2f);
        canIncrease = true;
        StopCoroutine(ResetIncrease());
    }

    
    private void SpawnPipe()
    {
        var randomY = Random.Range(-2f, 2f);
        var spawnPosition = new Vector3(10f, randomY, 0f);
        var wallObject = Instantiate(wallPrefab, spawnPosition, Quaternion.identity, transform);
        var wall = wallObject.GetComponent<Wall>();
        wall.UpdateRects();
        walls.Add(wall);
    }
    
    private void MovePipes()
    {
        for (var i = walls.Count - 1; i >= 0; i--)
        {
            var pipe = walls[i];
            pipe.transform.position += Vector3.left * (wallSpeed * Time.deltaTime);
            pipe.UpdateRects();

            if (pipe.transform.position.x < destroyXPosition)
            {
                Destroy(pipe.gameObject);
                walls.RemoveAt(i);
            }
        }
    }

    public bool CheckWallCollision(Rect playerRect) => 
        walls.Any(pipe => playerRect.Overlaps(pipe.topRect) || playerRect.Overlaps(pipe.bottomRect));
    
    public bool GapPassed(Rect playerRect)
    {
        foreach (var pipe in walls.Where(pipe => 
                     !pipe.hasScored && playerRect.Overlaps(pipe.middleRect)))
        {
            pipe.hasScored = true;
            return true;
        }

        return false;
    }
}
