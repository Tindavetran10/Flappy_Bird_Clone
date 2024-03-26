using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is used to manage the walls spawning
/// at the start of the scrit we call the coroutine to spawn the walls
/// every "time" the coroutine will create a random y value for our walls
/// and after that it will be spawn
/// 
/// the CheckIncrease method is used to check if is possible increase the time value 
/// if it is possible the IncreaseTime method will increase it
/// </summary>


public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject wall;
    [SerializeField] float x, minY, maxY, rndY;
    [SerializeField] Vector2 startPos;

    [SerializeField] GameManager gm;

    public float time;
    bool canIncrease = true;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Spawning());

    }

    // Update is called once per frame
    void Update()
    {

        CheckIncrease();

    }
    
    void CheckIncrease()
    {
        if (gm.score == 20 && canIncrease == true)
        {
            canIncrease = false;
            IncreaseTime();
        }
        else if (gm.score == 40 && canIncrease == true)
        {
            canIncrease = false;
            IncreaseTime();
        }
        else if (gm.score == 60 && canIncrease == true)
        {
            canIncrease = false;
            IncreaseTime();
        }
        else if (gm.score == 80 && canIncrease == true)
        {
            canIncrease = false;
            IncreaseTime();
        }
        else if (gm.score == 100 && canIncrease == true)
        {
            canIncrease = false;
            IncreaseTime();
        }
    }

    void IncreaseTime()
    {
        if (time >= 1.2f)
        {
            time -= 0.2f;
            StartCoroutine(ResetIncrease());
        }
        
    }

    IEnumerator ResetIncrease()
    {
        yield return new WaitForSeconds(2f);
        canIncrease = true;
        StopCoroutine(ResetIncrease());
    }

    IEnumerator Spawning()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(time);
            rndY = Random.Range(minY, maxY);
            startPos = new Vector2(x, rndY);

            GameObject go = Instantiate(wall, startPos, Quaternion.identity);
        }
    }
}
