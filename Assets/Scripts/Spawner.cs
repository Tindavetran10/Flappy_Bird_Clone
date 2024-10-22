using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameManager gm;

    public float time;
    private bool _canIncrease = true;
    
    public float spawnInterval = 2f;
    public float wallSpeed = 2f;
    public float destroyXPosition = -10f;

    private float _timer;
    private readonly List<Wall> _walls = new();
    
    private void Update()
    {
        CheckIncrease();
        
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            SpawnPipe();
            _timer = 0f;
        }

        MovePipes();
    }

    private void CheckIncrease()
    {
        switch (gm.score)
        {
            case 20 when _canIncrease:
                _canIncrease = false;
                IncreaseTime();
                break;
            case 40 when _canIncrease:
                _canIncrease = false;
                IncreaseTime();
                break;
            case 60 when _canIncrease:
                _canIncrease = false;
                IncreaseTime();
                break;
            case 80 when _canIncrease:
                _canIncrease = false;
                IncreaseTime();
                break;
            case 100 when _canIncrease:
                _canIncrease = false;
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
        _canIncrease = true;
        StopCoroutine(ResetIncrease());
    }

    
    private void SpawnPipe()
    {
        var randomY = Random.Range(-2f, 2f);
        var spawnPosition = new Vector3(10f, randomY, 0f);
        var wallObject = Instantiate(wallPrefab, spawnPosition, Quaternion.identity, transform);
        var wall = wallObject.GetComponent<Wall>();
        wall.UpdateRects();
        _walls.Add(wall);
    }
    
    private void MovePipes()
    {
        for (var i = _walls.Count - 1; i >= 0; i--)
        {
            var pipe = _walls[i];
            pipe.transform.position += Vector3.left * (wallSpeed * Time.deltaTime);
            pipe.UpdateRects();

            if (pipe.transform.position.x < destroyXPosition)
            {
                Destroy(pipe.gameObject);
                _walls.RemoveAt(i);
            }
        }
    }

    public bool CheckWallCollision(Rect playerRect) => 
        _walls.Any(pipe => playerRect.Overlaps(pipe.topRect) || playerRect.Overlaps(pipe.bottomRect));
    
    public bool GapPassed(Rect playerRect)
    {
        foreach (var pipe in _walls.Where(pipe => 
                     !pipe.hasScored && playerRect.Overlaps(pipe.middleRect)))
        {
            pipe.hasScored = true;
            return true;
        }

        return false;
    }
}
