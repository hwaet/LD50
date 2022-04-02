using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Popup : MonoBehaviour
{
    private float _minWidth = 0.1f;
    private float _minHeight = 0.1f;
    private Vector3? _moveOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        var rectTransform = GetComponent<RectTransform>();

        var anchorMaxX = Random.Range(0f, 1f);
        var anchorMaxY = Random.Range(0f, 1f);
        
        var anchorMinX = Random.Range(0, anchorMaxX - _minWidth);
        var anchorMinY = Random.Range(0, anchorMaxY - _minHeight);

        rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
        
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
        
        rectTransform.GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveOffset == null)
        {
            return;
        }
        
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            transform.position = mousePos - (Vector3)_moveOffset;
        }
        else
        {
            _moveOffset = null;
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void StartMoving()
    {
        _moveOffset = (Vector3)Mouse.current.position.ReadValue() - transform.position;
        Computer.Instance().MoveMeToTheFront(this);
    }
}
