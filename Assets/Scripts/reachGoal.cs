using UnityEngine;
using UnityEngine.SceneManagement;

public class reachGoal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Flagpole")
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
