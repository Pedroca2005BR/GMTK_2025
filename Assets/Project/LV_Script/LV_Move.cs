using UnityEngine;

//Mudei o nome da classe para melhor representar sua função
[RequireComponent (typeof(PlayerRecordable))]
public class LV_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField]private LayerMask interactableLayer;

    /* private Animator animator; */
    private Rigidbody2D rb;
    private bool isEnabled = true;

    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /* animator = GetComponent<Animator>(); */
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
