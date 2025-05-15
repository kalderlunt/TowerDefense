using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Cams Cinemachine")]
    public CinemachineVirtualCamera thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;
    [SerializeField] private Transform cameraFollowTarget;

    [Header("Paramètres de zoom")]
    public float zoomSpeed     = 5f;    // vitesse du zoom
    public float minDistance   = 0.1f;  // distance mini → switch 1ère personne
    public float maxDistance   = 4f;    // distance maxi (3e personne)
    public float heightOffset  = 1.8f;  // hauteur de la caméra

    [Header("Rotation 3e personne")]
    public float rotationSpeed = 150f;  // vitesse de rotation orbite

    private CinemachineTransposer transposer3P;
    private float currentDistance;
    private bool isFirstPerson = false;
    private Transform target;  // le pivot sur lequel orbiter

    void Start()
    {
        // récupère le Transposer du 3P pour modifier le FollowOffset
        transposer3P = thirdPersonCam.GetCinemachineComponent<CinemachineTransposer>();

        Debug.Log(transposer3P);

        // initialise la distance au paramètre de l’éditeur
        currentDistance = maxDistance;

        // repère le target (Follow) de la thirdPersonCam
        target = thirdPersonCam.Follow;

        // assure qu’on démarre bien en 3e personne
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
            // met à jour la distance
            currentDistance = Mathf.Clamp(currentDistance - scroll * zoomSpeed, minDistance, maxDistance);
            // applique au Transposer
            transposer3P.m_FollowOffset = new Vector3(0f, heightOffset, -currentDistance);

            // bascule de mode si on atteint l’une des extrémités
            if (currentDistance <= minDistance + 0.01f && !isFirstPerson)
                SetFirstPerson();
            else if (currentDistance > minDistance + 0.01f && isFirstPerson)
                SetThirdPerson();
        }
    }

    void HandleRotation()
    {
        if (isFirstPerson)
            return;  // le POV de la 1ère personne gère déjà la rotation

        // en 3e personne, on orbite autour du target **uniquement** si clic droit enfoncé
        if (Input.GetMouseButton(1))
        {

            Debug.Log("Input.GetMouseButton(1)");

            float yaw   = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float pitch = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;


            // Appliquer la rotation sur le Follow Target
            cameraFollowTarget.Rotate(Vector3.up, yaw, Space.World);
            cameraFollowTarget.Rotate(Vector3.right, pitch, Space.Self);

            // // rotation horizontale
            // thirdPersonCam.transform.RotateAround(
            //     target.position,
            //     Vector3.up,
            //     yaw
            // );
            // // rotation verticale
            // thirdPersonCam.transform.RotateAround(
            //     target.position,
            //     thirdPersonCam.transform.right,
            //     pitch
            // );
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