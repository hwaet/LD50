using UnityEngine;

public class Computer : MonoBehaviour
{
    public float secondsUntilSoftwareUpdate = 60;
    public Popup popupPrefab;
    public static Computer instance;

    public static Computer Instance()
    {
        return instance;
    }

    public bool SoftwareUpdateHasHappened()
    {
        return (secondsUntilSoftwareUpdate < 0);
    }

    void Awake()
    {
        instance = this;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        SpawnPopup();
        SpawnPopup();
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
