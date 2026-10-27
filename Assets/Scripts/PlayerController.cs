using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public GameObject loseTextObject;

    public GameObject restartButton;
    public GameObject startMenuButton;
    public GameObject gameOverOverlay;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        loseTextObject.SetActive(false);

        restartButton.SetActive(false);
        startMenuButton.SetActive(false);
        gameOverOverlay.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (transform.position.y < -2)
        {
            Vector3 pos = transform.position;
            pos.y = -2;
            transform.position = pos;

            Lose();
        }
    }

    void SetCountText()
    {
        countText.text = "Items collected: \n" + count.ToString() + " / 12";
        if (count >= 12)
        {
            Win();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            FindObjectOfType<AudioManager>().PlayItemPickup();

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Lose();
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            Win();
        }
    }

    void Win()
    {
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));

        FindObjectOfType<AudioManager>().PlayGameOver();

        winTextObject.gameObject.SetActive(true);
        restartButton.SetActive(true);
        startMenuButton.SetActive(true);
        gameOverOverlay.SetActive(true);

        Time.timeScale = 0;
    }

    void Lose()
    {
        FindObjectOfType<AudioManager>().PlayGameOver();

        loseTextObject.gameObject.SetActive(true);
        restartButton.SetActive(true);
        startMenuButton.SetActive(true);
        gameOverOverlay.SetActive(true);

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
