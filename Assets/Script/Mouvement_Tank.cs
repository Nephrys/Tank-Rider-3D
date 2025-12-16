using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvement_Tank : MonoBehaviour
{
    [Header("Movement")]
    public float speedx = 10f;
    public float speedz = 10f;
    public float acceleration = 0.6f;
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
    public float fireCooldown = 0.5f;
    float fireTimer = 0f;
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
        speedz += acceleration * Time.deltaTime;

        Vector3 movement = new Vector3(
            input.x * speedx,
            verticalVelocity,
            1f * speedz
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
        clampTankMovement();

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
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }

        if (fireAction.action.WasPressedThisFrame() && fireTimer <= 0f)
        {
            Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.identity
            );
            fireTimer = fireCooldown;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }

    void clampTankMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -10f, 10f);
        float clampedZ = Mathf.Clamp(transform.position.z, 0f, 100f);
        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            clampedZ
        );
    }
}
