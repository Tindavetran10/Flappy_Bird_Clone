using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float detectionDistance = 10f; // Increased detection distance
    [SerializeField] private LayerMask wallLayer;

    public void EnableAI() => enabled = true;

    public void DisableAI() => enabled = false;

    private void Update()
    {
        if (player.gm.isStarted && !player.gm.isGameOver)
        {
            KeepPlayerInMiddle();
            DetectAndDodgeWalls();
        }
    }

    private void KeepPlayerInMiddle()
    {
        if (Camera.main != null)
        {
            var screenMiddleY = Camera.main.orthographicSize / 2;
            var playerY = player.transform.position.y;

            if (playerY < screenMiddleY - 2.5f)
                player.force = player.jumpForce;
        }
    }

    private void DetectAndDodgeWalls()
    {
        var topDetectionRect = player.GetTopDetectionRect();
        var bottomDetectionRect = player.GetBottomDetectionRect();
        var walls = FindObjectsOfType<Wall>();

        foreach (var wall in walls)
        {
            if (Vector3.Distance(player.transform.position, wall.transform.position) < detectionDistance)
            {
                if (bottomDetectionRect.Overlaps(wall.bottomRect))
                {
                    player.force = player.jumpForce;
                }
                else if (topDetectionRect.Overlaps(wall.topRect))
                {
                    player.force = -player.jumpForce;
                }
            }
        }
    }
}