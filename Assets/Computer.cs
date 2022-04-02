using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Computer : MonoBehaviour
{
    public float secondsUntilSoftwareUpdate = 60;
    public Popup popupPrefab;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondsUntilSoftwareUpdate -= Time.deltaTime;
    }

    private void SpawnPopup()
    {
        var popup = Instantiate(popupPrefab.gameObject, transform.position, Quaternion.identity);
        popup.transform.parent = transform;
    }
}
