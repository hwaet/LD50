using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Ice Cream Level Settings")]
public class IceCreamLevel : ScriptableObject
{
    public List<Object> ScoopList;
    public float sunIntensity = 0f;
    public Object cherryPrefab;
    public int beeCount;
    public int beeFrequency;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
