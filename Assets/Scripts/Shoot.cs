using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public InputAction Click;
    public InputAction MousePosition;
    public GameObject BulletPrefab;
    public float Cooldown = 0.5f;
    private float _cooldown;
    private bool _isShooting;
    private void Start()
    {
        _cooldown = Cooldown;

        Click = InputSystem.actions.FindAction("Attack");
        Click.started += (ctx) =>
        {
            _isShooting = true;
        };
        Click.canceled += (ctx) =>
        {
            _isShooting = false;
        };

        MousePosition = InputSystem.actions.FindAction("Point");
    }

    private void Update()
    {
        if (_isShooting)
            ShootBullet();

        _cooldown -= Time.deltaTime;
    }

    private void ShootBullet()
    {
        if (_cooldown > 0)
            return;

        Camera cam = Camera.main;
        Vector2 mousePos = MousePosition.ReadValue<Vector2>();
        // create an instance of the prefab
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.position = cam.ScreenToWorldPoint(mousePos);
        // get the rigidbody of the prefab
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // get the mouse position, convert to a point 
        // in 3d space and create a vector from that point
        // that is parallel to the camera's direction
        Ray ray = cam.ScreenPointToRay(mousePos);

        rb.AddForce(1000 * ray.direction, ForceMode.Acceleration);

        // destroy the instance of the prefab after some time
        Destroy(bullet, 5);

        _cooldown = Cooldown;
    }

}
