using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement pm;

    [SerializeField] float originalSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravity;
    [SerializeField] float strength;
    float speed;
    CapsuleCollider charCtrl;
    Rigidbody rb;
    bool isGrounded;
    LayerMask ground = 3;
    [SerializeField] float throwForce = 10f;

    public PickUp currentPickUp;
    public bool hasItem;
    [SerializeField] float checkRadius = 2f;
    [SerializeField] GameObject slot;
    [SerializeField] GameObject arm;

    [SerializeField] Animator anim;



    private void Awake()
    {
        pm = this;
        speed = originalSpeed;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        if(currentPickUp != null)
        {
            if (Input.GetMouseButton(0))
            {
                ThrowItem();
            }
        }  
    }
    void PickUpItem(GameObject pickUp)
    {
        currentPickUp = pickUp.GetComponent<PickUp>();
        //currentPickUp.GetComponent<BoxCollider>().enabled = false;
        currentPickUp.GetComponent<SphereCollider>().enabled = false;
        anim.SetBool("IsHolding", true);
        currentPickUp.GetComponent <Rigidbody>().isKinematic = true;
        currentPickUp.transform.position = slot.transform.position;
        currentPickUp.transform.parent = slot.transform;
        currentPickUp.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward, Vector3.up);
        speed = originalSpeed / pickUp.GetComponent<PickUp>().weight * strength;
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (horizontalInput == 0 && verticalInput == 0)
        {
           
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
        if (!isGrounded)
        {
            rb.velocity += new Vector3(0, -gravity * Time.deltaTime, 0);
        }

        if (currentPickUp != null)
        {
            slot.transform.position = new Vector3(slot.transform.position.x, arm.transform.position.y, slot.transform.position.z) ;
        }
    }
    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, charCtrl.height / 2f , ground))
        {
            isGrounded = true;
        }
        else isGrounded = false;

    }
    public void ThrowItem()
    {
        anim.SetBool("IsHolding", false);
        Rigidbody rigid = currentPickUp?.GetComponent<Rigidbody>();
        currentPickUp.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Collider());
        rigid.AddForce((transform.forward + transform.up) / currentPickUp.GetComponent<PickUp>().weight * throwForce, ForceMode.Impulse);
        currentPickUp.GetComponent<SphereCollider>().enabled = false;
        currentPickUp = null;
    }
    private void OnEnable()
    {
        PickUp.pickUpEvent += PickUpItem;
    }
    private void OnDisable()
    {
        PickUp.pickUpEvent += PickUpItem;
    }
    private IEnumerator Collider()
    {
        GameObject GAS = currentPickUp.gameObject;
        yield return new WaitForSeconds(0.2f);
        GAS.GetComponent<SphereCollider>().enabled = true;
    }
}