using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Popup : MonoBehaviour
{
    public float minWidth = 0.1f;
    public float minHeight = 0.1f;
    private Vector3? _moveOffset;
    public Text _text;
    private bool _hasBeenScaledAndPositioned = false;

    void Awake()
    {
        if (_text==null) this._text = this.transform.Find("Text").GetComponent<Text>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO: Clearly wrong. This script shouldn't have to hard-code the resolution. All magic numbers here refer to pixel measures
        //this._text.GetComponent<LayoutElement>().preferredWidth = Mathf.Clamp(this._text.text.Length * 2, 100, 500);
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!_hasBeenScaledAndPositioned)
		{
			/*
             * since we are only able to set the preferred width in Start(), it takes at least one tick to resize
             * so we use this lossy JIT evaluator
             */

			ScaleAndPosition();
		}

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

    public void SetText(string textContent)
    {
        this._text.text = textContent;
    }

    /*
     * This whole approach seems wrong, but here's what we have.
     *
     * The problem is that we need to have a popup size which can properly contain the contents of the inner text.
     * Since the inner text is non-deterministic, its dimensions are unknown. Additionally, the way that the text
     * size is measured (pixel dimensions) is different than how this popup is resized (float range of axes)
     *
     * So, we measure the size of the text's rectangle (done before this method is called), compare that to the
     * supposed pixel measure of this game, and then convert to a float for randomized positioning/sizing.
     *
     * Not fun.
     */
    [ContextMenu("Scale and position")]
    private void ScaleAndPosition()
    {
        var rectForText = this._text.GetComponent<RectTransform>().rect;

        if (rectForText.width == 0 || rectForText.height == 0)
        {
            return;
        }

        RectTransform rect = Computer.Instance().GetComponent<RectTransform>();
        Vector2 screenSize = rect.sizeDelta;

        // TODO: Clearly wrong. This script shouldn't have to hard-code the resolution.
        float targetResolutionWidth = screenSize.x;
        float targetResolutionHeight = screenSize.y;

        // The magic number at the end is for predictable bloat for the panel itself. Very fudgy.
        float widthFactor = (rectForText.width / targetResolutionWidth) + 0.1f;
        float heightFactor = (rectForText.height / targetResolutionHeight) + 0.1f;
        
        var rectTransform = GetComponent<RectTransform>();
        
        var anchorRandomX = Random.Range(0f, 1f - widthFactor);
        var anchorRandomY = Random.Range(0f, 1f - heightFactor);

        var anchorMaxX = 1f - anchorRandomX;
        var anchorMaxY = anchorRandomY + heightFactor;
        
        var anchorMinX = 1f - (widthFactor + anchorRandomX);
        var anchorMinY = anchorMaxY - heightFactor;

        rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
        
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
        
        //rectTransform.GetComponent<Image>().color = Color.green;
        
        _hasBeenScaledAndPositioned = true;
	}
    
}
