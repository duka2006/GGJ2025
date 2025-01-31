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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            PickUp.pickUpEvent -= PickUpItem;
            if (Input.GetMouseButton(0))
            {
                ThrowItem();
            }
        }  
        if (currentPickUp == null)
        {
            PickUp.pickUpEvent += PickUpItem;
        }

        Debug.Log(anim.GetBool("IsRunning"));
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
    void PickUpItem(GameObject pickUp)
    {
        hasItem = true;
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

        Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

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
    public void ThrowItem()
    {
        anim.SetBool("IsHolding", false);
        Rigidbody rigid = currentPickUp?.GetComponent<Rigidbody>();
        currentPickUp.GetComponent<Rigidbody>().isKinematic = false;
        currentPickUp.transform.parent = null;
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
        PickUp.pickUpEvent -= PickUpItem;
    }
    private IEnumerator Collider()
    {
        GameObject GAS = currentPickUp.gameObject;
        yield return new WaitForSeconds(0.2f);
        GAS.GetComponent<SphereCollider>().enabled = true;
    }
}