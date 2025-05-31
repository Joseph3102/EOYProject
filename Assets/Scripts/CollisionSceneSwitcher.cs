using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSceneSwitcher : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "thing1")
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
