using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator deathTransitionimage;

    public void startDeathTransition() {
        deathTransitionimage.SetTrigger("StartDeathTrans");
    }

    public void endDeathTransition() {
        deathTransitionimage.SetTrigger("EndDeathTrans");
    }
}
