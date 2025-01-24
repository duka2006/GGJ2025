using Unity.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    CharacterController charCtrl;
    [SerializeField] float moveSpeed;

    float gravity = 1f;
    Vector2 directionInputs;
    Vector3 moveDirection;

    bool isGrounded;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        CheckGround();
        directionInputs = new Vector2(Input.GetAxis("Vertical") * moveSpeed, Input.GetAxis("Horizontal") * moveSpeed);
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * directionInputs.x) + (transform.TransformDirection(Vector3.right) * directionInputs.y);
        moveDirection.y = moveDirectionY;

        if (!isGrounded)
        {
            moveDirection.y -= gravity;
        }
        else
        {
            Debug.Log(directionInputs);
            charCtrl.Move(moveDirection * Time.deltaTime);
        }
    }
    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 1f, out hit))
        {
            if (hit.collider.gameObject.layer == 3)
            {
                isGrounded = true;
            }
            else isGrounded = false;
        }
        else isGrounded = false;
        
    }
}
