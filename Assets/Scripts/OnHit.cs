using UnityEngine;

public class OnHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 100, ForceMode.Acceleration);

    }
}
