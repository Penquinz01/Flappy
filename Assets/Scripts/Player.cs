using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float timer = 0.4f;
    float time = 0;
    public GameManager manager;
    private Controls _controls;
    private bool started = false;

    private void Awake()
    {
        _controls = new Controls();
        AssignControls();
        if (manager.started)
        {
            StartGame();
        }
        else
        {
            rb.gravityScale = 0;
        }
    }

    private void Start()
    {
        
        Debug.Log(started);
    }

    private void OnEnable()
    {
        AssignControls();
    }

    private void OnDisable()
    {
        UnAssignControls();
    }

    void AssignControls()
    {
        _controls.Enable();
        _controls.Player.Enable();
        _controls.Player.Jump.started += ctx => OnJump(ctx);
    }

    void UnAssignControls()
    {
        _controls.Player.Jump.started -= ctx => OnJump(ctx);
        _controls.Player.Disable();
        _controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Mathf.Abs(transform.position.y) > 5f)
        {
            manager.EndGame();
        }

        if (manager.started && !started)
        {
            StartGame();
        }
        
    }

    void StartGame()
    {
        started = true;
        rb.gravityScale = 1;
    }
    public void OnJump(InputAction.CallbackContext cxt)
    {
        if (!started)
        {
            return;
        }
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
