using Cinemachine;
using UnityEngine;

public class PlayerInspect : MonoBehaviour
{
    public static bool isInspecting;

    [Header("Camera Settings")]
    public Camera stillCamera;
    public CinemachineBrain followCamera;
    
    [Header("Scene Settings")]
    [SerializeField] private bool inspectAvailable;
    public Animator inspectButton;

    private void Update()
    {
        if (Input.GetKey(KeyCode.I) && !isInspecting)
        {
            Debug.Log("Player attempted to perform inspect");
            if (inspectAvailable)
            {
                Debug.Log("Inspect initiated.");
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            }
            else
            {
                Debug.Log("No inspect available right now.");
            }
        }
    }

    public void BeginInspect()
    {
        stillCamera.transform.SetPositionAndRotation(followCamera.transform.position, followCamera.transform.rotation);
        followCamera.gameObject.SetActive(false);
        stillCamera.gameObject.SetActive(true);
        isInspecting = true;
    }

    public void EndInspect()
    {
        stillCamera.gameObject.SetActive(false);
        followCamera.gameObject.SetActive(true);
        isInspecting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InspectRegion"))
        {
            inspectButton.SetBool("isEnabled", true);
            inspectAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InspectRegion"))
        {
            inspectButton.SetBool("isEnabled", false);
            inspectAvailable = false;
        }
    }
    
}
