using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    private void Update() {
        restartGame();
    }

    private void restartGame() {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
