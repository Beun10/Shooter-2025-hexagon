using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
    public (float, float) GetInputs()
    {
        return (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
