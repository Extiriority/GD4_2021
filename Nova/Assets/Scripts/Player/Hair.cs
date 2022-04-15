using UnityEngine;

public class Hair : MonoBehaviour
{
    public Vector2 partOffset = Vector2.zero;
    public float lerpSpeed = 20f;

    private Transform[] hairParts;
    private Transform hairAnchor;

    private void Awake() {
        hairAnchor = GetComponent<Transform>();
        hairParts = GetComponentsInChildren<Transform>();
    }

    private void Update() {
        calculateHair();
    }

    private void calculateHair() {
        Transform partsToFollow = hairAnchor;
        foreach (Transform hairPart in hairParts) {
            //exclude hair anchor
            if (hairPart.Equals(hairAnchor)) continue;
            Vector2 targetPosition = (Vector2)partsToFollow.position + partOffset;
            Vector2 newPositionLerp = Vector2.Lerp(hairPart.position, targetPosition, Time.deltaTime * lerpSpeed);
                
            hairPart.position = newPositionLerp;
            partsToFollow = hairPart;
        }
    }
}
