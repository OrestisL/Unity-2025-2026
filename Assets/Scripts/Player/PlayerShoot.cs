using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour
{
    public GameObject PetPrefab;

    public InputAction Click;
    public InputAction MousePosition;

    private Transform _player;
    private int _maxPets = 2;
    private int _currentPets = 2;

    private GameObject[] _pets;
    private GameObject _currentPet;
    private Vector3 _target;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            // something's wrong
            Debug.LogError($"Not player found in scene {SceneManager.GetActiveScene().name}");
        }

        _player = player.transform;

        _pets = new GameObject[_maxPets];

        for (int i = 0; i < _maxPets; i++)
        {
            GameObject pet = Instantiate(PetPrefab);
            pet.transform.SetParent(_player, false);
            pet.GetComponent<PetMovement>().IsLeftHandSide = i % 2 == 0;
            _pets[i] = pet;
        }

        Click = InputSystem.actions.FindAction("Attack");
        Click.started += Shoot;

        MousePosition = InputSystem.actions.FindAction("Point");

        var X = InputSystem.actions.FindAction("X");
        X.started += (_) => { Debug.Log("asdasdas"); };
    }


    private void Update()
    {
        if (_currentPet == null)
            return;

        _currentPet.transform.position = Vector3.MoveTowards(_currentPet.transform.position, _target, Time.deltaTime);

        if (_currentPets == 0)
        {
            // regenerate
        }
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Camera cam = Camera.main;
        Vector2 mousePos = MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            if (_pets.Any(p => p != null)) 
            {
                _currentPet = _pets.First(p => p != null);
                int idx = _pets.ToList().IndexOf(_currentPet);
                _pets[idx] = null;
                _currentPet.transform.parent = null;

                _currentPet.GetComponent<PetMovement>().enabled = false;
                _target = hit.point;

                Destroy(_currentPet, 10.0f);
                _currentPets--;
            }
        }
    }



}
