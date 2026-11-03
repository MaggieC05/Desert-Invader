using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject dartPrefab;

    public static float shotCooldown = .05f;
    public float lastShot = 0;
    public float speed = 10f;

    [SerializeField] private Lives lives;

    public Vector2 screenBounds;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Making sure the spaceship stays within the screen bounds
    void Start()
    {
       if (lives == null)
       {
           lives = FindAnyObjectByType<Lives>();
       }

       screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)); 
    }

    // Update is called once per frame
    //Spaceship movement and shooting logic
    void Update()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Vector3 position = transform.position;
            position.x += speed * Time.deltaTime;
            transform.position = position;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Vector3 position = transform.position;
            position.x -= speed * Time.deltaTime;
            transform.position = position;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time - lastShot > shotCooldown)
        {
            Vector3 position = transform.position;
            GameObject bullet = Instantiate<GameObject>(dartPrefab);
            bullet.transform.position = position;
            lastShot = Time.time;
        }

        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);
        Vector2 currentPosition = transform.position;
        currentPosition.x = clampedX;
        transform.position = currentPosition;
    }


    void OnCollisionEnter(Collision coll)
    {
        if (lives == null)
        {
            Debug.LogError("No active Lives component was found in the scene.", this);
            return;
        }

        lives.lives--;
        if (lives.lives <= 0)
        {
            Destroy(coll.gameObject);
            SceneManager.LoadScene("EndScreen");
        }
    }

    // void OnDestroy()
    // {
    //     SceneManager.LoadScene("EndScreen");
    // }

}
