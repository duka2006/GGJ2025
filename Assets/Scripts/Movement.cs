using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    CharacterController charCtrl;
    [SerializeField] float moveSpeed;

    [SerializeField] float gravity = 5f;
    Vector2 directionInputs;
    Vector3 moveDirection;

    bool isGrounded;

    LayerMask ground = 3;
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        CheckGround();
        directionInputs = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = ((transform.TransformDirection(Vector3.forward) * directionInputs.x) + (transform.TransformDirection(Vector3.right) * directionInputs.y)).normalized;
        moveDirection.y = moveDirectionY;

        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            Mathf.Clamp(moveDirection.y, -gravity, 0);
        }
        charCtrl.Move(moveDirection * Time.deltaTime * moveSpeed);
        
    }
    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, charCtrl.height / 2f + .5f, ground))
        {
            isGrounded = true;
        }
        else isGrounded = false;

    }
}
