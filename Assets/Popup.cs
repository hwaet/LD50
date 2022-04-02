using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private float minWidth = 0.1f;
    private float minHeight = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        var rectTransform = GetComponent<RectTransform>();

        var anchorMaxX = Random.Range(0f, 1f);
        var anchorMaxY = Random.Range(0f, 1f);
        
        var anchorMinX = Random.Range(0, anchorMaxX - minWidth);
        var anchorMinY = Random.Range(0, anchorMaxY - minHeight);

        rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
        
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
        
        rectTransform.GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        Destroy(gameObject);
    }
    
}
