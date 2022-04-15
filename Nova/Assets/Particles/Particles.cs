using UnityEngine;

public class Particles : MonoBehaviour
{
    private Collisions coll;
    private PlayerMovement player;
    
    private bool groundTouch;
    
    [Header("Player Particles")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    private void Start() {
        coll = GetComponent<Collisions>();
        player = GetComponent<PlayerMovement>();
    }
    private void Update() {
        checkGroundTouch();
    }
    
    private void checkGroundTouch() {
        switch (coll.onGround) {
            case true when !groundTouch:
                jumpParticle.transform.position = transform.position + new Vector3(0f, -0.45f);
                jumpParticle.Play();
                groundTouch = true;
                break;
            case false when groundTouch:
                groundTouch = false;
                break;
        }
    }

    public void jumpEffects(bool wall) {
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;
        
        jumpParticle.transform.position = transform.position + new Vector3(0f, -0.61f);
        particle.Play();
    }

    public void wallParticle(float vertical) {
        var main = slideParticle.main;

        if (player.canWallSlide || player.canWallGrab && vertical < 0) {
            main.startColor = Color.white;
        }
        else {
            main.startColor = Color.clear;
        }
    }
}
