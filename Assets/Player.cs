using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float timer = 0.4f;
    float time = 0;
    public GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
    public void OnJump(InputAction.CallbackContext cxt)
    {
        if (cxt.started && time>timer)
        {
            rb.linearVelocityY = speed;
            time = 0;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        manager.EndGame();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score")) manager.UpdateText();
    }
}
