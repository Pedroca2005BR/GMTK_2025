using UnityEngine;

//Mudei o nome da classe para melhor representar sua função
public class LV_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField]private LayerMask interactableLayer;

    /* private Animator animator; */
    private Rigidbody2D rb;

    private Vector2 moveDirection;
    private bool withItem = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /* animator = GetComponent<Animator>(); */
    }

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            TryInteract();
        }
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        /* animator.SetFloat("Speed", moveDirection.sqrMagnitude); */
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void TryInteract()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Button"))
            {
                hitCollider.GetComponent<LV_Button>().PressButton();
                break;
            }
            if (hitCollider.CompareTag("Item"))
            {
                if (!withItem)
                {
                    withItem = true;
                    hitCollider.GetComponent<LV_Item>().TakeItem();
                }
            }
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
