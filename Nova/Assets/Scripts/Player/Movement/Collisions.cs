using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private Rigidbody2D rb;
    //private float startingGrav;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        //startingGrav = rb.gravityScale;
    }

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Ground Collision")]
    [SerializeField][Range(0, 0.6f)] private float groundRaycastLength;
    [SerializeField] private Vector3 groundRaycastOffset;
    
    [Header("Wall Collision")]
    [SerializeField][Range(0, 0.5f)] public float wallRaycastLength;
    [SerializeField] private Vector3 wallRaycastOffset;
    public bool onWall;
    public bool onRightWall;
    public bool onGround;
    public bool canEdgeCorrect;
    
    /*[Header("Ledge Detection")]
    [SerializeField] public float ledgeXRaycastOffset;
    [SerializeField][Range(-1f, 1f)]  public float ledgeYRaycastOffset;
    private float ledgeXSize = 0.1f;
    private float ledgeYSize = 0.05f;
    
    [SerializeField] public float ledgeWallXRaycastOffset;
    [SerializeField][Range(-1f, 1f)]  public float ledgeWallYRaycastOffset;
    private float ledgeWallXSize = 0.1f;
    private float ledgeWallYSize = 0.05f;
    private bool canLedgeWall;
    private bool canLedgeUp;
    public bool isLedgeClimbing;*/
    
    
    
    
    public void checkCollisions() {
        //Ground Collisions
        onGround = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) ||
                   Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);

        //Corner Detection
        canEdgeCorrect = Physics2D.Raycast(transform.position + outerRaycastOffset, Vector2.up, topRaycastLength, groundLayer) &&
                         !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer) ||
                         Physics2D.Raycast(transform.position - outerRaycastOffset, Vector2.up, topRaycastLength, groundLayer) &&
                         !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer);

        //Wall Collisions
        onWall = Physics2D.Raycast(transform.position - wallRaycastOffset, Vector2.right, wallRaycastLength, groundLayer) ||
                 Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, groundLayer) || 
                 Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, groundLayer) ||
                 Physics2D.Raycast(transform.position - wallRaycastOffset, Vector2.left, wallRaycastLength, groundLayer);
        
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, groundLayer);
        
        //Ledge Detection
        /*canLedgeWall = Physics2D.OverlapBox(new Vector2(transform.position.x + (ledgeWallXRaycastOffset * transform.localScale.x), transform.position.y + ledgeWallYRaycastOffset), new Vector2(ledgeWallXSize, ledgeWallYSize), 0f, groundLayer);
        canLedgeUp = Physics2D.OverlapBox(new Vector2(transform.position.x + (ledgeXRaycastOffset * transform.localScale.x), transform.position.y + ledgeYRaycastOffset), new Vector2(ledgeXSize, ledgeYSize), 0f, groundLayer);

        if (canLedgeWall && !canLedgeUp && !isLedgeClimbing && !onGround)
        {
            isLedgeClimbing = true;
        }

        if (isLedgeClimbing)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0f;
        }*/
    }

    /*public IEnumerator changePos()
    {
        transform.position = new Vector2(transform.position.x + (0.5f * transform.localScale.x),
            transform.position.y + 0.6f);
        
        rb.gravityScale = startingGrav;
        isLedgeClimbing = false;
        yield return new WaitForSeconds(1f);
        
    }*/
    
    [Header("Edge Correction")]
    [SerializeField][Range(0, 1)] private float topRaycastLength;
    [SerializeField] private Vector3 outerRaycastOffset;
    [SerializeField] private Vector3 innerRaycastOffset;
    
    public void edgeCorrect(float yVelocity) {
        //Push player to the right
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength,Vector3.left, topRaycastLength, groundLayer);
        if (hit.collider != null) {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength,
                transform.position - outerRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity);
            return;
        }

        //Push player to the left
        hit = Physics2D.Raycast(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.right, topRaycastLength, groundLayer);
        if (hit.collider == null) return; {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength,
                transform.position + outerRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        //Ground Check
        Gizmos.DrawLine(transform.position + groundRaycastOffset, transform.position + groundRaycastOffset + Vector3.down * groundRaycastLength);
        Gizmos.DrawLine(transform.position - groundRaycastOffset, transform.position - groundRaycastOffset + Vector3.down * groundRaycastLength);

        //Wall Check
        Gizmos.DrawLine(transform.position - wallRaycastOffset, transform.position - wallRaycastOffset  + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position - wallRaycastOffset, transform.position - wallRaycastOffset + Vector3.left * wallRaycastLength);
        Gizmos.color = Color.blue;
        //Corner Check
        Gizmos.DrawLine(transform.position + outerRaycastOffset, transform.position + outerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - outerRaycastOffset, transform.position - outerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastOffset, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength);

        //Corner Distance Check
        Gizmos.DrawLine(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength,
            transform.position - innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength,
            transform.position + innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.right * topRaycastLength);
        
        /*Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (ledgeWallXRaycastOffset * transform.localScale.x), transform.position.y + ledgeWallYRaycastOffset), new Vector2(ledgeWallXSize, ledgeWallYSize));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (ledgeXRaycastOffset * transform.localScale.x), transform.position.y + ledgeYRaycastOffset), new Vector2(ledgeXSize, ledgeYSize));
        */
    }
}
