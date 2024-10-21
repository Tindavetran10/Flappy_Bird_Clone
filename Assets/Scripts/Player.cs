using UnityEngine;

/// <summary>
/// this script is used to control our player
/// and manage his collisions
/// at the start of the scrit we get player's rigidbody2D component
/// in the Jump method we get the input and push up the player
/// adding a force to his rb
/// 
/// we also check the collisions with the walls and triggers with the scores
/// if the player hit the wall its gameover
/// else if the player trigger the score we will increase the value score in gm(Game Manager)
/// </summary>


public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] public GameManager gm;
    //[SerializeField] AudioSource dieAudio, pointAudio, flyAudio;

    [SerializeField] public float force;
    bool _alive = true;


    // Start is called before the first frame update
    private void Start() => rb = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    private void Update() => Jump();

    public void Jump()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {

            //flyAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, 1 * force);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score" && _alive)
        {
            //pointAudio.Play();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gm.score += 1;

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && _alive)
        {
            _alive = false;
            //dieAudio.Play();
            gm.isGameOver = true;
            enabled = false;
        }
    }
}
