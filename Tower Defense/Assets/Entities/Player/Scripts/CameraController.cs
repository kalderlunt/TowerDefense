using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Cams Cinemachine")]
    public CinemachineVirtualCamera thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;
    [SerializeField] private Transform cameraFollowTarget;

    [Header("Paramètres de zoom")]
    public float zoomSpeed     = 5f;
    public float minDistance   = 0.1f;
    public float maxDistance   = 4f;
    public float heightOffset  = 1.8f;

    [Header("Rotation 3e personne")]
    public float rotationSpeed = 150f;

    private CinemachineTransposer transposer3P;
    private float currentDistance;
    private bool isFirstPerson = false;
    private Transform target;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        transposer3P = thirdPersonCam.GetCinemachineComponent<CinemachineTransposer>();

        // initialise la distance
        currentDistance = maxDistance;

        // récupère le target
        target = thirdPersonCam.Follow;

        // initialise yaw/pitch à la rotation actuelle du follow target
        Vector3 angles = cameraFollowTarget.rotation.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        SetThirdPerson();
    }

    void Update()
    {
        HandleZoom();
        HandleRotation();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentDistance = Mathf.Clamp(currentDistance - scroll * zoomSpeed, minDistance, maxDistance);
            transposer3P.m_FollowOffset = new Vector3(0f, heightOffset, -currentDistance);

            if (currentDistance <= minDistance + 0.01f && !isFirstPerson)
                SetFirstPerson();
            else if (currentDistance > minDistance + 0.01f && isFirstPerson)
                SetThirdPerson();
        }
    }

    void HandleRotation()
    {
        if (isFirstPerson)
            return;

        if (Input.GetMouseButton(1))
        {
            yaw   += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Clamp l'angle vertical
            pitch = Mathf.Clamp(pitch, -70f, 70f);

            // Appliquer la rotation finale
            cameraFollowTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);

            Debug.Log($"Pitch: {pitch}");  // ← Vérifie si les valeurs changent
            Debug.Log($"Yaw: {yaw}");
        }
    }

    void SetFirstPerson()
    {
        isFirstPerson           = true;
        thirdPersonCam.Priority = 0;
        firstPersonCam.Priority = 10;
    }

    void SetThirdPerson()
    {
        isFirstPerson           = false;
        firstPersonCam.Priority = 0;
        thirdPersonCam.Priority = 10;
    }
}