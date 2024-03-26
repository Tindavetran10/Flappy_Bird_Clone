using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is used to move and control the wall forward the player
/// the MoveWall method is used to move the walls
/// the CheckPos method is used to check the actual position of the wall and destroy it
/// </summary>
public class Wall : MonoBehaviour
{

    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWall();

        CheckPos();
    }

    void MoveWall()
    {
        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
    }

    void CheckPos()
    {
        if (transform.position.x <= -6f)
        {
            Destroy(this.gameObject);
        }
    }
}
