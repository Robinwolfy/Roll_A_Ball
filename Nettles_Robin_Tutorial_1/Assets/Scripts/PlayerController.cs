using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool canInput;
    private bool win;
    public float speed;
    private int count;
    private int badCount;
    public int lives;
    public int goal;
    public int goal2;
    public float delay;
    private float nextStage;
    public Text countText;
    public Text winText;
    public Text lifeText;
    public Text restartText;
    public Transform lvl2;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        badCount = 0;
        lives = 3;
        SetCountText();
        winText.text = "";
        restartText.text = "";
        canInput = true;
        win = false;
    }

    void FixedUpdate() //called before physics
    {
        //turns input into variables
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Creates Vector3

        if (canInput)
        {
            rb.AddForce(movement * speed);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(win && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Minigame");
        }
        SetCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Pick up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("bad"))
        {
            other.gameObject.SetActive(false);
            lives--;
            badCount++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        lifeText.text = "Lives: " + lives.ToString();
        if (count >= goal && !win)
        {
            winText.text = "Stage 1 Clear";
            canInput = false;
            Win();
        }
        else if (count >= goal && win)
        {
            winText.text = "You Win!";
            canInput = false;
            win = true;
            restartText.text = "Press Space to Restart";
        }
        else if (lives <= 0)
        {
            winText.text = "YOU LOSE!";
            canInput = false;
            Lose();
        }

    }
    void Win()
    {
        nextStage += Time.deltaTime;
        if (nextStage >= delay)
        {
            this.transform.position = lvl2.position;
            goal = goal + goal2;
            winText.text = "";
            win = true;
            canInput = true;
        }
    }
    void Lose()
    {
        nextStage += Time.deltaTime;
        if (nextStage >= delay)
        {
            this.gameObject.SetActive(false);
        }

    }
}
