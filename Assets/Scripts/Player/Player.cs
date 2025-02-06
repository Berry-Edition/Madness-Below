using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    [Header("Player :")]
    [SerializeField, Tooltip("unused for now")] private float _runSpeed = RUN_SPEED;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _cam;
    [SerializeField] private PlayerItemAttach _itemAttach;
    [FormerlySerializedAs("_pivot")] [SerializeField] private Transform _body;
    [SerializeField] private Animator _playerAnimator;
    
    [Header("*FEATURE   Dash :")]
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.2f; // Time the dash lasts
    [SerializeField] private float _dashCooldown = 1f;  // Time before dash can be used again
    [Space]
    [SerializeField] private bool  _isDashing = false;
    [SerializeField] private float _dashTimeLeft;
    [SerializeField] private float _dashCooldownTimer;
    
    [Header("*FEATURE   Sprint :")]
    [SerializeField] private float _sprintSpeed = 8f;
    [SerializeField] private bool _isSprinting;

    [Header("*FEATURE   Crouch :")]
    [SerializeField] private bool _isCrouching;
    
    [Header("Debugging :")]
    [SerializeField] private Vector2 _movementDirection;
    [SerializeField] private float _rbVelocity;


    public float RbVelocity
    {
        get
        {
            return _rbVelocity;
        }
        set
        {
            _rbVelocity = value;
        }
    }
    
    private float _horizontalInput;
    private float _verticalInput;
    
    private bool _isMoving;

    
    private Vector3 _pointAndClicktarget;
    #region Constants
    public const float RUN_SPEED = 4f;
    
    /// <summary>
    /// <see cref="RUN_SPEED"/> divided by 2
    /// </summary>
    public const float CROUCH_SPEED = 2f;
    #endregion

    public Vector2 GetInput(){
        return new Vector2(_horizontalInput, _verticalInput).normalized;
    }

    public Vector2 GetNormalizedPosition(){
        return _rb.position.normalized;
    }

    public Vector2 GetVelocity(){
        return _rb.velocity;
    }

    private void PlayerAnimations(){
        _isMoving = _isCrouching || Mathf.Approximately(_rbVelocity, 4) || (Vector2)_pointAndClicktarget != Vector2.zero;
        
        if (_isMoving)
        {
            if ((Vector2)_pointAndClicktarget == Vector2.zero)
            {
                _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_HORIZONTAL, GetInput().x);
                _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_VERTICAL,   GetInput().y);
            }
            else
            {
                if (Mathf.Abs(_clickToMoveDirection.x) > Mathf.Abs(_clickToMoveDirection.y))
                {
                    // Mouvement horizontal
                    _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_HORIZONTAL, _clickToMoveDirection.x);
                    _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_VERTICAL, 0);
                    print(_clickToMoveDirection.x > 0 ? "right" : "left");
                }
                else if (Mathf.Abs(_clickToMoveDirection.x) < Mathf.Abs(_clickToMoveDirection.y))
                {
                    // Mouvement vertical
                    _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_VERTICAL, _clickToMoveDirection.y);
                    _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_HORIZONTAL, 0);
                }   
            }
        }
        else
        {
            ResetPlayerAnimation();
        }
    }
    
    private void Update(){
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && _dashCooldownTimer <= 0f && !_isDashing)
        {
            if (!_isSprinting)
                Dash();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else EndSprint();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else EndCrouch();
        
        PlayerAnimations();

        print(_isMoving);
    }
    
    private void OnDrawGizmos(){
        Gizmos.color = Mathf.Abs(_horizontalInput) < 0.1f ? Color.white : Color.yellow;
        Gizmos.DrawRay(transform.position, _horizontalInput >= 0 ? Vector3.right : Vector3.left);

        Gizmos.color = Mathf.Abs(_verticalInput) < 0.1f ? Color.white : Color.red;
        Gizmos.DrawRay(transform.position, _verticalInput >= 0 ? Vector3.up : Vector3.down);

        if ((Vector2)_pointAndClicktarget != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_pointAndClicktarget, 0.2f);
        }
    }
    
    private void FixedUpdate() {
        _movementDirection.Normalize();
        _rb.AddForce(new Vector2(_movementDirection.normalized.x * _runSpeed, _movementDirection.normalized.y * _runSpeed));

        if (_isDashing)
        {
            _rb.velocity = _movementDirection * _dashSpeed;
            _pointAndClicktarget = Vector2.zero;
        }
        else if (_isSprinting)
        {
            _rb.velocity = _movementDirection * _sprintSpeed;
            _pointAndClicktarget = Vector2.zero;
        }
        else if (_isCrouching)
        {
            _rb.velocity = _movementDirection * CROUCH_SPEED;
            _pointAndClicktarget = Vector2.zero;
        }
        else
        {
            // Normal movement
            _rb.velocity = _movementDirection * RUN_SPEED;
            if (_rb.velocity != Vector2.zero)
                _pointAndClicktarget = Vector2.zero;
        }
        
        if (_isDashing)
        {
            _dashTimeLeft -= Time.deltaTime;
            if (_dashTimeLeft <= 0f)
            {
                EndDash();
            }
        }
        
        // Cooldown timer
        if (_dashCooldownTimer > 0f)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }
        
        RbVelocity = _rb.velocity.magnitude;
        ClickToMove();
    }
    
    private Vector2 _clickToMoveDirection = Vector2.zero;
    
    private void ClickToMove(){
        // player's rigidbody velocity as four means the player is moving
        if (!_isMoving && Input.GetMouseButtonUp(0))
        {
            _pointAndClicktarget = _cam.ScreenToWorldPoint(Input.mousePosition);
            _pointAndClicktarget.z = transform.position.z;
        }

        if ((Vector2)_pointAndClicktarget != Vector2.zero)
        {
            if (_rb.velocity.magnitude == 0)
            {
                _clickToMoveDirection = (_pointAndClicktarget - transform.position).normalized;
                
                _rb.velocity = new Vector2(_clickToMoveDirection.x * RUN_SPEED, _clickToMoveDirection.y * RUN_SPEED);
                
                if (Vector2.Distance(transform.position, _pointAndClicktarget) <= 0.5f)
                {
                    _pointAndClicktarget = Vector2.zero;
                }   
            }
        }
    }

    /// <summary>
    /// Set Horizontal & Vertical animation floats to zero
    /// </summary>
    public void ResetPlayerAnimation(){
        _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_HORIZONTAL, 0);
        _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_VERTICAL,   0);
    }
    
    /// <summary>
    /// Horizontal & Vertical animation floats to player inputs
    /// </summary>
    public void UpdatePlayerAnimationToInputs() {
        _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_HORIZONTAL, GetInput().x);
        _playerAnimator.SetFloat(PlayerAnimator.ANIMATOR_VERTICAL,   GetInput().y);
    }
    
    private void MovePlayer(){
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput   = Input.GetAxisRaw("Vertical");
        
        _movementDirection = new Vector2(_horizontalInput, _verticalInput);
        
        #region Player Item Attach Directions
        if (_verticalInput > 0)
            _itemAttach.SetDirection(Vector2.left);
        
        if (_verticalInput < 0)
            _itemAttach.SetDirection(Vector2.right);

        if (_horizontalInput > 0)
            _itemAttach.SetDirection(Vector2.up);
        
        if (_horizontalInput < 0)
            _itemAttach.SetDirection(Vector2.down);
        #endregion
    }
    
    private void Dash(){
        _isDashing = true;
        _dashTimeLeft =      _dashDuration;
        _dashCooldownTimer = _dashCooldown;
    }
    
    private void Sprint(){
        _isSprinting = !_isDashing;
    }

    private void Crouch(){
        _isCrouching = true;
    }

    private void EndCrouch(){
        _isCrouching = false;
    }
    
    private void EndDash()
    {
        _isDashing = false;
        _rb.velocity = Vector2.zero; // force stop the dash
    }

    private void EndSprint(){
        _isSprinting = false;
        _rb.velocity = Vector2.zero; // force stop the sprint
    }
}