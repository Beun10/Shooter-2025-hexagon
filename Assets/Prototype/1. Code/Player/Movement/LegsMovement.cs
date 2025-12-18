using UnityEngine;

public class LegsMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Animation movingAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        (horizontalInput, verticalInput) =  controller.GetInputs();
        Anim();
    }

     public void SetDirection()
    {
        if (horizontalInput > 0) //->
        {
            if (verticalInput > 0) transform.rotation = Quaternion.Euler(0, 0, 45); //up
            else if (verticalInput < 0) transform.rotation = Quaternion.Euler(0, 0, -45); //down
            else transform.rotation = Quaternion.Euler(0, 0, 0); //right
        }
        else if (horizontalInput < 0) //<-
        {
            if (verticalInput > 0) transform.rotation = Quaternion.Euler(0, 0, 135); //up
            else if (verticalInput < 0) transform.rotation = Quaternion.Euler(0, 0, -135); //down
            else transform.rotation = Quaternion.Euler(0, 0, 180); //left
        }
        else if (verticalInput > 0) transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (verticalInput < 0) transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    private void Anim()
    {
        if (horizontalInput != 0 || verticalInput != 0) movingAnim.Play();
    }
}
