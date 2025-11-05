using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform Center;
    public float RotationSpeed = 10.0f;
    public float Radius = 3.0f;

    public bool IsLeftHandSide = true;

    private float _startY = 0;

    private void Start()
    {
        if (!Center)
            Center = transform.parent.Find("Center");

        _startY = Center.localPosition.y;
    }

    private void Update()
    {
        MoveOnCircle();
        OscillateCenter();
    }

    private void OscillateCenter()
    {
        Center.position = new Vector3(Center.position.x, Center.parent.transform.position.y + _startY + IsLeftHandSide.ToIntMinusPlusOne() * Mathf.Sin(RotationSpeed * Time.time), Center.position.z);
    }

    private void MoveOnCircle()
    {
        float x = Radius * Mathf.Cos(RotationSpeed * Time.time);
        float z = Radius * Mathf.Sin(RotationSpeed * Time.time);

        transform.position = Center.position + new Vector3(x, 0, z);
    }
}
