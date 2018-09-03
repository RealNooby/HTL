using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Enemy : MonoBehaviour
{
    [ReadOnly]
    public int health;
    public int maxHealth;
    public float speed;
    public float knockbackMod;
    public float stunLength;
    private float stunTimer;

    public Color idleColor, hurtColor, deadColor;

    public SpriteRenderer sr;

    public Rigidbody2D rgbd2D;

    public enum EnemyState
    {
        MOVE,
        STUNNED,
        DEAD
    }

    public EnemyState state;

	// Use this for initialization
	public void Init ()
    {
        health = maxHealth;
        state = EnemyState.MOVE;
	}

    private void Move()
    {
        // so if i put this in update it will run 60 times per second at 60 fps
        // but if my game runs at 30 fps
        // enemies will move slower
        // Time.deltatime is the time inbetween frames
        transform.position += Vector3.left * speed * Time.deltaTime;
        rgbd2D.velocity = Vector2.Lerp(rgbd2D.velocity, Vector2.zero, Time.deltaTime / 2);
        rgbd2D.angularVelocity = Mathf.Lerp(rgbd2D.angularVelocity, 0, Time.deltaTime / 2);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime / 2);
    }

    public void Stun(Vector2 direction,float knockback = 1)
    {
        health--;
        if(health <= 0)
        {
            //DIE
            state = EnemyState.DEAD;
            rgbd2D.AddForce(direction * knockback * 0.3f, ForceMode2D.Impulse);
            //rgbd2D.AddForce(Vector2.right * knockback * 0.2f, ForceMode2D.Impulse);
            health = 0;
            //transform.position += Vector3.right * knockback * knockbackMod;
        }
        else
        {
            state = EnemyState.STUNNED;
            //rgbd2D.AddForce(direction * knockback, ForceMode2D.Impulse);
            rgbd2D.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
            //transform.position += Vector3.right * knockback * knockbackMod;
            stunTimer = stunLength;
        }
        
    }

    // Update is called once per frame
    void Update ()
    {
		switch(state)
        {
            case EnemyState.MOVE:
                Move();
                break;
            case EnemyState.STUNNED:
                if(stunTimer > 0)
                {
                    stunTimer -= Time.deltaTime;
                    sr.color = hurtColor; 
                }
                else
                {
                    stunTimer = 0;
                    sr.color = idleColor;
                    state = EnemyState.MOVE;
                }
                break;
            case EnemyState.DEAD:
                sr.color = deadColor;
                break;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyRemover")
        {
            Destroy(this.gameObject);
        }
    }
}
