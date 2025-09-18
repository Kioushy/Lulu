using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 10;

    private Animator animator;

    // Update is called once per frame
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void StopRotation()
    {
        Debug.Log("Animator nul");
        if (animator != null)
        {
            Debug.Log("isRotating True & RotationSpeed 0 ");
            animator.SetBool("isRotating", true);
            rotationSpeed = 0;
        }
    }
}
