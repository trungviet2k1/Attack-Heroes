using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 move = moveSpeed * Time.deltaTime * direction;
            transform.position += move;
        }
    }
}