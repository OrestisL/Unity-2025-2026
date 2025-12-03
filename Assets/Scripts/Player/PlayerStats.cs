using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Attack = 30;
    public float Defense = 15;
    public float Speed = 10;
    public float AttackSpeed = 50;

    public float KickStrength => Attack * 15;
    public float PunchStrength => Attack * 12;
}
