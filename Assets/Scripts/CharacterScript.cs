using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private float speed = 10f;
    private float playerVelocityY; // вертикальна компонента швидкості
    private float gravityValue = -9.80f;
    private float jumpHeight = 1.5f;
    private bool groundedPlayer;
  
    private CharacterController _characterController;
    private Animator _animator;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        playerVelocityY = 0f;

    }
    void Update()
    {
        int animatorState = 0;
        if (_characterController.isGrounded)
        {
            groundedPlayer = true;
        }
        // groundedPlayer = _characterController.isGrounded;
        if (groundedPlayer && playerVelocityY < 0)
        {
            playerVelocityY = 0f;
        }

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        if (Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f; // /= Mathf.Sqrt(2f);
            dy *= 0.707f; // /= Mathf.Sqrt(2f);
        }

        if (dy != 0 && groundedPlayer) // перевага "вперед" (якщо дiагональ, то анiмацiя)
        {
            animatorState = 1;
        }
        else if (dx != 0 && groundedPlayer)
        {
            animatorState = 2;
        }
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animatorState = 4;
            _animator.SetInteger("State", animatorState);
            groundedPlayer = false;
            playerVelocityY += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Camera.main.transform.forward - має нахил, иому для .Move має піднімальний ефект
        Vector3 horizontalForward = Camera.main.transform.forward;
        horizontalForward.y = 0;
        horizontalForward = horizontalForward.normalized;

        float ds = Time.deltaTime / 5.0f;
        if (Input.GetKey(KeyCode.LeftShift) && (dx != 0 || dy != 0))
        {

            if (GameState.CharacterStamina > ds)
            {
                dx *= 2.5f;
                dy *= 2.5f;
                GameState.CharacterStamina -= ds;
            }

        }
        else
        {
            if (GameState.CharacterStamina < 1 - ds)
            {
                GameState.CharacterStamina += ds;
            }
            else
            {
                GameState.CharacterStamina = 1f;
            }
        }
        playerVelocityY += gravityValue * Time.deltaTime;

        _characterController.Move(Time.deltaTime *
            (speed * (dx * Camera.main.transform.right + dy * horizontalForward) +
            playerVelocityY * Vector3.up));
        // повертаємо персонаж у напрямку погляду камери
        this.transform.forward = horizontalForward;
        // задаємо стан аніматору
        if (groundedPlayer)
        {
            _animator.SetInteger("State", animatorState);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = true;
            _animator.SetInteger("State", 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = false;
        }
    }
    public void OnJumpStart()
    {
        _animator.SetInteger("State", 3);
    }
}
