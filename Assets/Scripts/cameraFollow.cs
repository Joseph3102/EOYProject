using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Optional: Drag your player here. If left empty, it will auto-find by tag 'Player' or name 'Player'.")]
    public Transform player;

    private float offsetY;
    private float offsetZ;

    void Awake()
    {
        // 1) If not assigned in Inspector, try to find by tag
        if (player == null)
        {
            var byTag = GameObject.FindWithTag("Player");
            if (byTag != null)
            {
                player = byTag.transform;
                Debug.Log("CameraFollow: Found player by tag.");
            }
        }

        // 2) If still null, try to find by name
        if (player == null)
        {
            var byName = GameObject.Find("Player");
            if (byName != null)
            {
                player = byName.transform;
                Debug.Log("CameraFollow: Found player by name.");
            }
        }

        // 3) If still not found, bail out
        if (player == null)
        {
            Debug.LogError("CameraFollow: Player not found! Make sure your player GameObject is tagged 'Player' or named 'Player'.");
            enabled = false;
            return;
        }

        // Capture the initial Y and Z so we keep them fixed
        offsetY = transform.position.y;
        offsetZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Only follow on X-axis; Y & Z stay as they were at start
        transform.position = new Vector3(player.position.x, offsetY, offsetZ);
    }
}
