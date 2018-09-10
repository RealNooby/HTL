using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombRadius;
	public float bombTimer;
	private void Update()
	{
		bombTimer += Time.deltaTime;
		if(bombTimer > 1)
		{
			Explode();
		}
	}
	public void Explode()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        for (int i = 0; i < colls.Length; i++)
        {
            Destroy(colls[i].gameObject);
            GameManager.instance.score += 3;
        }
		Destroy(gameObject);
    }
}
