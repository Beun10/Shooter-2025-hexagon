using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private CurrentMove currentMove = CurrentMove.Delay;
    [SerializeField] private float speed;
    public float speedBuff = 1f;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private PlayerController playerController;
    private bool moving;
    private float timer;
    [SerializeField] LegsMovement legsMovement;
    [SerializeField] float movingDelay;
    [SerializeField] float movingDuration;
    [SerializeField] float movingCooldown;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        timer = 0;
        currentMove = CurrentMove.Waiting;
    }
    private void Update()
    {
        Moving();
        if(!moving) (horizontalInput, verticalInput) = playerController.GetInputs();
    }
    private void FixedUpdate()
    {
        if (moving) rb.linearVelocity = new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);
        else rb.linearVelocity = new Vector2(0, 0);
    }

    private void Moving()
    {
        if (currentMove == CurrentMove.Waiting)
        {
            (float horInput, float verInput) = playerController.GetInputs();
            if (horInput != 0 || verInput != 0)
            {
                currentMove = CurrentMove.Delay;
                timer = 0;
            }
        }
        else if (currentMove == CurrentMove.Delay)
        {
            timer += Time.deltaTime;
            if (timer > movingDelay)
            {
                legsMovement.SetDirection();
                currentMove = CurrentMove.CanMove;
                moving = true;
                timer = 0;
            }
        }
        else if (currentMove == CurrentMove.CanMove)
        {
            timer += Time.deltaTime;
            if (timer > movingDuration)
            {
                currentMove = CurrentMove.Cooldown;
                moving = false;
                timer = 0;
            }
        }
        else if (currentMove == CurrentMove.Cooldown)
        {
            timer += Time.deltaTime;
            if (timer > movingCooldown)
            {
                timer = 0;
                currentMove = CurrentMove.Waiting;
            }
        }
    }
    private enum CurrentMove
    {
        Delay,
        Cooldown,
        CanMove,
        Waiting
    }
}
