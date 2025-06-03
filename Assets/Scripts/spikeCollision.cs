using UnityEngine;
using UnityEngine.SceneManagement;

public class spikeCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spikes")
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
