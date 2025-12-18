using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PointerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        var tempMousePosition = Input.mousePosition;
        tempMousePosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
        tempMousePosition.z = 0;
        float angle = Mathf.Atan2(tempMousePosition.y- transform.position.y, tempMousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
    }
}
