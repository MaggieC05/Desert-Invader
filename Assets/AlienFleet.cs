using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AlienFleet : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject alienPrefab;

    public AlienCounter alienCounter;

    public List<GameObject> aliens = new List<GameObject> { };

    float speed = .1f;
    float leftBound = -16;
    float rightBound = 16;
    float lowerBound = -4.6f;
    float downTime = .2f;
    bool movingDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createAlienFleet(); //creates the alien fleet at the start of the game
        alienCounter.score = aliens.Count; //sets the score to the number of aliens in the fleet

    }

    // Update is called once per frame
    void Update()
    {
        //alienCounter.score = aliens.Count;
    }

    void FixedUpdate()
    {
        float left = 0, right = 0;
        //move all aliens
        foreach (GameObject alien in aliens)
        {
            Vector3 position = alien.transform.position;
            if (!movingDown)
            {
                position.x += speed;
            }
            else
            {
                position.y += -Mathf.Abs(speed);

            }

            alien.transform.position = position;

            if (position.x > right)
            {
                right = position.x;
            }
            if (position.x < left)
            {
                left = position.x;
            }

            if (position.y < lowerBound)
            {
                SceneManager.LoadScene("EndScreen");
            }

            if (speed > 0 && right >= rightBound || speed < 0 && left <= leftBound)
            {
                speed = -speed;
                movingDown = true;
                Invoke("startSidewaysMotion", downTime);
            }
        }

        // if (speed > 0 && right >= rightBound || speed < 0 && left <= leftBound)
        // {
        //     speed = -speed;
        //     movingDown = true;
        //     Invoke("startSidewaysMotion", downTime);
        // }
    }

    void startSidewaysMotion()
    {
        movingDown = false;
    }

    //Double loop to create the alien fleet in a grid pattern
    void createAlienFleet()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject alien = Instantiate<GameObject>(alienPrefab);
                alien.transform.position = new Vector3(2 * i - 12, 10 - 2 * j, 0);
                aliens.Add(alien);
                alien.GetComponent<Alien>().fleet = this;
            }
        }
    }

    //Function to remove an alien from the fleet and update the score and speed
    public void removeAlien(GameObject alien)
    {
        aliens.Remove(alien); //remove the alien from the list of aliens
        alienCounter.score= aliens.Count; //update the score to the number of remaining aliens
        if (aliens.Count == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            speed *= 1.02f; //increase speed by 2% for each alien destroyed
        }

    }
}
