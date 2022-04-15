using UnityEngine;

public class Finish : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject.Find("Luna").SendMessage("finish");
    }
}
