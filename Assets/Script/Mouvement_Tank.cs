using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvement_Tank : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject explosionPrefab;


    [Header("Movement")]
    public float speed = 5f;
    public InputActionReference moveAction;

    [Header("Jump")]
    public InputActionReference jumpAction;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float gravity = 15f;

    [Header("Pitch")]
    [SerializeField] float maxPitchAngle = 20f;
    [SerializeField] float pitchSpeed = 6f;

    [Header("Fire")]
    public InputActionReference fireAction;
    public GameObject bulletPrefab;

    float verticalVelocity = 0f;
    bool isGrounded = true;

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 movement = new Vector3(
            input.x * speed,
            verticalVelocity,
            1f * speed
        );

        transform.position += movement * Time.deltaTime;


        if (transform.position.y <= 0f)
        {
            transform.position = new Vector3(
                transform.position.x,
                0f,
                transform.position.z
            );

            verticalVelocity = 0f;
            isGrounded = true;
        }
        float pitch = Mathf.Clamp(
            verticalVelocity * maxPitchAngle,
            maxPitchAngle,
            -maxPitchAngle
        );

        Quaternion targetRotation = Quaternion.Euler(
            pitch,
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            pitchSpeed * Time.deltaTime
        );

        // ---------- TIR ----------
        if (fireAction.action.WasPressedThisFrame())
        {
            Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy") || collision.collider.CompareTag("water") )
        {
            Destroy(gameObject);
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

    }

}
