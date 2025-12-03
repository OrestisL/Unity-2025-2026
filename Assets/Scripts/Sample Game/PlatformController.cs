using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public List<string> Tags = new List<string>();
    public int AmountOfObjectsToWin = 3;
    private int _objectsOn;
    private Color _startingColor;
    private void Start()
    {
        _startingColor = GetComponent<Renderer>().material.color;
    }

    private void FixedUpdate()
    {
        Collider[] objects = Physics.OverlapBox(transform.position, transform.localScale);
        foreach (Collider obj in objects) 
        {
            if (!Tags.Contains(obj.tag)) continue;
            _objectsOn++;
        }

        if (_objectsOn >= AmountOfObjectsToWin)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<Renderer>().material.color = _startingColor;
        }
        _objectsOn = 0;
    }

}
