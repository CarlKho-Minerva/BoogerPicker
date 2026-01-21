using UnityEngine;
using UnityEngine.SceneManagement; // Needed to restart
using UnityEngine.UI; // Needed for Text

public class GameManager : MonoBehaviour
{
    public GameObject winText; // Drag your UI Text here
    public AudioSource winSound; // Optional: Drag a "Ta-da!" sound here

    void OnTriggerEnter(Collider other)
    {
        // If the Booger hits this invisible wall...
        if (other.gameObject.name == "Booger")
        {
            WinGame();
        }
    }

    void WinGame()
    {
        // 1. Show the text
        if (winText != null) winText.SetActive(true);

        // 2. Play sound (if you have one)
        if (winSound != null) winSound.Play();

        // 3. Restart the game after 3 seconds
        Invoke("RestartScene", 3f);
    }

    void RestartScene()
    {
        // Reloads the current scene (Reset everything)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}