using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerBody;
    private Transform cameraTransform;

    [Header("Paramètres de vitesse")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;

    private Vector3 moveDirection = Vector3.zero;
    private bool isMoving = false;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!isMoving) return;

        // Appliquer le déplacement en direction de la caméra
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // On ignore la hauteur pour rester à plat
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Calcul de la direction finale dans l’espace caméra
        Vector3 worldDirection = camRight * moveDirection.x + camForward * moveDirection.z;

        rb.linearVelocity = worldDirection * walkSpeed;

        if (moveDirection == Vector3.zero)
            isMoving = false;

        if (worldDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(worldDirection);
            playerBody.transform.rotation = Quaternion.Slerp(playerBody.transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    public void Move(Vector2 inputValue)
    {
        isMoving = true;
        inputValue = Vector2.ClampMagnitude(inputValue, 1f);
        moveDirection = new Vector3(inputValue.x, 0, inputValue.y);
    }
}