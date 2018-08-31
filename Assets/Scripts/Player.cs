using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform muzzleTransform;
	// Use this for initialization
	void Start ()
    {
		//now to make a skill for removing dead enemies (AoE)
	}
	void RotationLoop()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this is the direction
        Vector2 dir = (Vector2)transform.position - mousePos;
        float _angle = Vector2.SignedAngle(Vector2.left, dir.normalized);
        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }
    void SpawnBullets(int numOfBullets = 1)
    {
        for(int i = 0; i < numOfBullets; i++)
        {
            Bullet _bullet = Instantiate(bulletPrefab, muzzleTransform.position, transform.rotation);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        RotationLoop();
        if(Input.GetMouseButtonDown(0))
        {
            //Shoot
            SpawnBullets();
        }
	}
}
