using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float floatStrength = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPos;
    private Vector3 originalScale;

    void Start()
    {
        startPos = transform.position;
        originalScale = transform.localScale;  // Save the initial scale
    }

    void Update()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Floating effect
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatStrength;
        transform.position += new Vector3(0f, floatOffset * Time.deltaTime, 0f);

        // Flip the enemy based on player position relative to enemy
        if (player.position.x < transform.position.x)
        {
            // Player is to the left — flip enemy to face left
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            // Player is to the right — face right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}
