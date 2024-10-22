using UnityEngine;

public class Player : MonoBehaviour
{
    //public Rigidbody2D rb;

    [SerializeField] public GameManager gm;
    [SerializeField] public AudioSource dieAudio, pointAudio, flyAudio;

    public float force;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    private const bool Alive = true;

    [Header("Player's bounds")]
    public float width = 0.5f;
    public float height = 0.5f;

    [SerializeField] private float detectionGap = 0.1f;
    [SerializeField] private float detectionXOffset = 0.0f;
    [SerializeField] private float topDetectionHeight = 0.5f; // Height of the top detection rect
    [SerializeField] private float bottomDetectionHeight = 0.5f; // Height of the bottom detection rect

    
    // Update is called once per frame
    private void Update()
    {
        if (gm.isStarted && Alive)
        {
            // Calculate gravity
            force += gravity * Time.deltaTime;
            
            // Apply gravity to the player
            transform.position += new Vector3(0, force * Time.deltaTime, 0);
        }
        
        var aiController = GetComponent<AIController>();
        if (!gm.isGameOver && (aiController == null || !aiController.enabled))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Mathf.Approximately(force, jumpForce))
        {
            flyAudio.Play();
            force = jumpForce;
        }
    }
    

    public Rect GetBounds()
    {
        var position = transform.position;
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
        return new Rect(position.x - size.x / 2, position.y - size.y / 2, size.x, size.y);
    }

    public Rect GetTopDetectionRect()
    {
        var position = transform.position;
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
        return new Rect(position.x - size.x + detectionXOffset, position.y + detectionGap, size.x * 2, topDetectionHeight);
    }

    public Rect GetBottomDetectionRect()
    {
        var position = transform.position;
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
        return new Rect(position.x - size.x + detectionXOffset, position.y - size.y / 2 - detectionGap, size.x * 2, bottomDetectionHeight);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<SpriteRenderer>().bounds.size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(GetTopDetectionRect().x + GetTopDetectionRect().width / 2, GetTopDetectionRect().y + GetTopDetectionRect().height / 2, 0), new Vector3(GetTopDetectionRect().width, GetTopDetectionRect().height, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(GetBottomDetectionRect().x + GetBottomDetectionRect().width / 2, GetBottomDetectionRect().y + GetBottomDetectionRect().height / 2, 0), new Vector3(GetBottomDetectionRect().width, GetBottomDetectionRect().height, 0));
    }
}
