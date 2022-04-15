using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float panSpeed = 5f;
    Camera _camera;
    Ship _selection;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Select();
        }
        if (Input.GetMouseButtonDown(1) && _selection != null)
        {
            Action();
        }
    }

    private void Action()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Ship _actionTarget = hit.collider.GetComponent<Ship>();

            if (_actionTarget != null)
            {
                _selection.GoTo(_actionTarget);
            }
            else
            {
                _selection.GoTo(hit.point);
            }
        }
    }

    private void Select()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _selection = hit.collider.GetComponent<Ship>();
        }
    }

    private void Move()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float scaledSpeed = panSpeed * Time.deltaTime;
        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.position += movementVector * scaledSpeed;
    }
}