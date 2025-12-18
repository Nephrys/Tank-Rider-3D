using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Mouvement_Tank : MonoBehaviour
{
    [Header("Movement")]
    public float speedx = 10f;
    public float speedz = 10f;
    public float acceleration = 0.6f;
    public InputActionReference moveAction;
    public GameObject explosionPrefab;

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

    [Header("Dev")]
    public bool Death = true;

    float verticalVelocity = 0f;
    bool isGrounded = true;
    bool isDead = false;

    void Update()
    {
        // Don't process input if dead
        if (isDead) return;

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
            GameObject bullet = Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.identity
            );

            // Pass the tank's current Z speed to the bullet
            Bullet_Controller bulletController = bullet.GetComponent<Bullet_Controller>();
            if (bulletController != null)
            {
                bulletController.setInheritedSpeed(speedz);
            }

            fireTimer = fireCooldown;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Death || isDead) return;
        if (collision.collider.CompareTag("enemy") || collision.collider.CompareTag("trou"))
        {
            // Start death sequence (explosion + scene load)
            StartCoroutine(HandleDeath(collision.collider.gameObject));
        }
    }

    private IEnumerator HandleDeath(GameObject enemy)
    {
        // Mark as dead to prevent further input/collisions
        isDead = true;

        Vector3 explosionPosition = transform.position;
        
        // Destroy enemy
        Destroy(enemy);

        // Hide the tank (disable renderer) but keep GameObject alive for coroutine
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        // Disable collider so tank doesn't interact with anything else
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Spawn explosion
        float explosionDuration = 1f; // Default fallback
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                explosionDuration = ps.main.duration + ps.main.startLifetime.constantMax;
                Destroy(explosion, explosionDuration);
            }
            else
            {
                Debug.LogWarning("Explosion prefab has no ParticleSystem component!");
            }
        }
        else
        {
            Debug.LogError("Explosion prefab is not assigned!");
        }
        yield return new WaitForSeconds(explosionDuration);
        SceneManager.LoadScene("GameOverScene");
    }

    void clampTankMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -10f, 10f);
        float clampedZ = Mathf.Clamp(transform.position.z, 0f, 1000f);
        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            transform.position.z
        );
    }
}
