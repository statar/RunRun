using UnityEngine;
public class StudentMovementController : MonoBehaviour
{
    [HideInInspector]
    public bool stopPlayerMovement;
    public static bool startTimerFinish;
    
    private Joystick _joystick;
    [SerializeField] private float forwardSpeed = 2.4f;
    [SerializeField] private float movementSpeed = 1.7f;
    private float _targetY;
    private bool _updateMovement;
    private bool _isJumping;
    private bool _isMovementStartedTimer;
    private bool _stopPos;
   
    
    private void Awake()
    {
        _isMovementStartedTimer = false;
        startTimerFinish = false;
        _targetY = 0.24f;
        _joystick = FindObjectOfType<Joystick>();
    }
    private void FixedUpdate()
    {
        if (_updateMovement)
            StudentMovement();
        else 
            JoystickGameStartMovement();
    }
    
    private void JoystickGameStartMovement()
    {
        if (startTimerFinish)
        {
            _updateMovement = true;
            GameManager.instance.StartGame();
        }
        
        if (((_joystick.Horizontal > 0.3) || (_joystick.Horizontal < -0.3)) && !_isMovementStartedTimer)
        {
            CanvasController.instance.StartPanelToTimer();
            _isMovementStartedTimer = true;
        }
        
    }
    private void StudentMovement()
    {
        if (stopPlayerMovement)
        {
            if (_stopPos || !(transform.position.y > 0.3f)) return;
            transform.position = new Vector3(transform.position.x, 0.24f, transform.position.z);
            _stopPos = true;
            return;
        }
        var pPos = transform.position;
        
        var targetX = pPos.x + _joystick.Horizontal * movementSpeed;
        if (targetX <= -4.5f)
            targetX = -4.5f;
        else if (targetX >= 4.5f)
            targetX = 4.5f;

        var targetZ = pPos.z + forwardSpeed;
        var direction = new Vector3(x: targetX, _targetY, targetZ);
        StudentJump();
        transform.position = Vector3.Lerp(pPos, direction ,5f * Time.deltaTime);
        StudentMovementRotatian();
    }

    private void StudentJump()
    {
        var positionYPos = transform.position.y;
        if (!_isJumping &&_joystick.Vertical > 0.5f)
        {
            _targetY = 4.5f;
            _isJumping = true;
        }

        if (_isJumping && positionYPos > 4f)
            _targetY = 0.24f;
        
        if (_isJumping && positionYPos < 0.26f)
            _isJumping = false;
        
        /*
        switch (_isJumping)
        {
            case false when _joystick.Vertical > 0.5f:
                _targetY = 4f;
                _isJumping = true;
                break;
            case true when pPos.y > 3.7f:
                _targetY = 0.24f;
                break;
            case true when pPos.y < 0.26f:
                _isJumping = false;
                break;
        }
        */
    }

    private void StudentMovementRotatian()
    {
        var direction = gameObject.transform.position + Vector3.right * (_joystick.Horizontal * 30f);
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 6f * Time.deltaTime);
    }

    public void ResetMovement()
    {
        _updateMovement = false;
        startTimerFinish = false;
        stopPlayerMovement = false;
        _isMovementStartedTimer = false;
        transform.position = new Vector3(0f, 0.245f, 0f);
    }
}
