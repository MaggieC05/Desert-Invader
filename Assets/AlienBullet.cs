using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AlienBullet : MonoBehaviour
{
    [Header("Inscribed")]

    static float speed = .1f;

    public Lives health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.y -= speed;
        transform.position = position;
        if(position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
       
        Destroy(gameObject);
        // //health.lives--;
        // Destroy(coll.gameObject);
        //eneManager.LoadScene("EndScreen");
    }
}
