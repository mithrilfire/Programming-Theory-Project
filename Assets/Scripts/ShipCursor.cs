using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCursor : MonoBehaviour
{
    [SerializeField] MeshRenderer _renderer;
    [SerializeField] Vector3 _offset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] Color _allyColor;
    [SerializeField] Color _enemyColor;
    Transform _target;
    private void Update()
    {
        if (_target == null && _renderer.enabled)
        {
            _renderer.enabled = false;
        }

        if (_target == null)
        {
            return;
        }

        transform.position = _target.position + _offset;
    }

    public void SetTarget(Transform target, bool isTargetEnemy)
    {
        _target = target;
        _renderer.enabled = true;

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        if (isTargetEnemy)
        {
            mpb.SetColor("_BaseColor", _enemyColor);
        }
        else
        {
            mpb.SetColor("_BaseColor", _allyColor);
        }
        gameObject.GetComponent<MeshRenderer>().SetPropertyBlock(mpb);
    }

    public void ClearTarget()
    {
        _target = null;
        _renderer.enabled = false;
    }

    public void SetOffset(Vector3 value)
    {
        _offset = value;
    }
}
