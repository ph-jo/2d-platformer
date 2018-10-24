using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBreadScript : MonoBehaviour {
    public GameObject SpawnPoint;
    private Rigidbody2D rb2d;
    private GameController gc;
    private VampireBossScript boss;
    private bool hit = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        boss = FindObjectOfType<VampireBossScript>();
        Destroy(gameObject, 12f);
        int spawnArea = Mathf.CeilToInt(Random.Range(0, 4));
        rb2d = GetComponent<Rigidbody2D>();
        float spawnRangeY = Random.Range(-7, 8);
        float spawnRangeX = Random.Range(-14.5f, 16.5f);
     
        Vector3 spawnPos = SpawnPoint.transform.position;
        print(spawnPos);
        Vector3 batScale = transform.localScale;
        Quaternion batRotation = transform.localRotation;
        switch (spawnArea)
        {
            case 0:
                spawnPos.x += 20;
                spawnPos.y += spawnRangeY;
                rb2d.velocity = new Vector2(-5, 0);
                break;
            case 1:
                spawnPos.x -= 18;
                spawnPos.y += spawnRangeY;
                batScale.x *= -1;
                transform.localScale = batScale;
                rb2d.velocity = new Vector2(5, 0);
                break;
            case 2:
                spawnPos.y -= 10;
                spawnPos.x += spawnRangeX;
                transform.Rotate(0, 0, -90);
                rb2d.velocity = new Vector2(0, 5);
                break;
            case 3:
                spawnPos.y += 10;
                spawnPos.x += spawnRangeX;
                transform.Rotate(0, 0, 90);
                rb2d.velocity = new Vector2(0, -5);
                break;
        }
        transform.position = spawnPos;
    }

    private void FixedUpdate()
    {
        if (boss == null || (transform.position.x > boss.transform.position.x && transform.position.y > boss.transform.position.y)) Destroy(gameObject);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            print("bread hit");
            rb2d.velocity = new Vector2((boss.transform.position.x - transform.position.x) * 2f, (boss.transform.position.y - transform.position.y) * 2f);
            boss.TakeDamage();
        }

    }
}
