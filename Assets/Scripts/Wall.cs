using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField] private float speed;
    
    [Header("Wall's bounds")]
    public float pipeWidth = 1f;
    public float pipeHeight = 10f;
    public float gapHeight = 4f;
    
    public Rect topRect;
    public Rect middleRect;
    public Rect bottomRect;
    
    public bool hasScored;
    
    // Start is called before the first frame update
    private void OnValidate() => UpdateRects();
    
    public void UpdateRects()
    {
        var position = transform.position;
        var topPipeBottom = position.y + gapHeight / 2;
        var bottomPipeTop = position.y - gapHeight / 2;
        

        topRect = new Rect(position.x - pipeWidth / 2, topPipeBottom, pipeWidth, pipeHeight);
        middleRect = new Rect(position.x - pipeWidth / 2, bottomPipeTop, pipeWidth, gapHeight);
        bottomRect = new Rect(position.x - pipeWidth / 2, bottomPipeTop - pipeHeight, pipeWidth, pipeHeight);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(bottomRect.x + bottomRect.width / 2, bottomRect.y + bottomRect.height / 2, 0), 
            new Vector3(bottomRect.width, bottomRect.height, 0));
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(middleRect.x + middleRect.width / 2, middleRect.y + middleRect.height / 2, 0),
            new Vector3(middleRect.width, middleRect.height, 0));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(topRect.x + topRect.width / 2, topRect.y + topRect.height / 2, 0), 
            new Vector3(topRect.width, topRect.height, 0));
    }
}
