using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float panSpeed = 5f;
    [SerializeField] ShipCursor _cursor;
    Camera _camera;
    Ship _selection;

    private void OnEnable()
    {
        Ship.OnShipDestroy += OnShipDestroyed;
    }
    private void OnDisable()
    {
        Ship.OnShipDestroy -= OnShipDestroyed;
    }

    void OnShipDestroyed(Ship.ShipInfo info)
    {
        if (info.Team == Ship.ShipTeam.Mercenary)
        {
            //Todo: Shake Camera
            Debug.Log("Ally Ship Destroyed");
        }
    }

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
    //todo show show target and path via lines.
    private void Action()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Ship _actionTarget = hit.collider.GetComponentInParent<Ship>();

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

    //todo show ship health
    private void Select()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _selection = hit.collider.GetComponentInParent<Ship>();
        }

        if (_selection)
        {
            _cursor.SetTarget(_selection.transform, _selection.Team != Ship.ShipTeam.Mercenary);
        }
        else
        {
            _cursor.ClearTarget();
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
