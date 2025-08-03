using UnityEngine;

//Mudei o nome da classe para melhor representar sua função
[RequireComponent(typeof(PlayerRecordable))]
public class LV_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField]private LayerMask interactableLayer;

    private bool isEnabled = true;

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveDirection = Vector2.zero;
    private bool withItem = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isEnabled)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                TryInteract();
            }
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
        }

        animator.SetBool("isWalking", moveDirection != Vector2.zero);
        animator.SetFloat("walkDirection", moveDirection.sqrMagnitude);
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
            if (hitCollider.CompareTag("Text"))
            {
                hitCollider.GetComponent<TextInterface>().ShowText();
                break;
            }
        }
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
