using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float secondsUntilSoftwareUpdate = 60;
    public Popup popupPrefab;
    public static Computer instance;
    private Transform _popupsContainer;

    public static Computer Instance()
    {
        return instance;
    }

    public bool SoftwareUpdateHasHappened()
    {
        return (secondsUntilSoftwareUpdate < 0);
    }

    public void MoveMeToTheFront(Popup popup)
    {
        var children = new List<Transform>();

        // TODO: this could be made to use Linq and forego side effects... but how????

        foreach (Transform popupTransform in _popupsContainer)
        {
            if (popupTransform.GetComponent<Popup>() == popup)
            {
                children.Add(popup.transform);
            }
        }

        foreach (Transform popupTransform in _popupsContainer)
        {
            if (popupTransform.GetComponent<Popup>() != popup)
            {
                children.Add(popup.transform);
            }
        }

        foreach (var child in children)
        {
            child.SetParent(null);
        }

        foreach (var child in children)
        {
            child.SetParent(_popupsContainer, true);
        }
    }

    void Awake()
    {
        instance = this;
        _popupsContainer = transform.Find("PopupsContainer");
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPopup("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        SpawnPopup("Foobar");
    }

    // Update is called once per frame
    void Update()
    {
        secondsUntilSoftwareUpdate -= Time.deltaTime;
    }

    private void SpawnPopup(string content)
    {
        var popup = Instantiate(popupPrefab.gameObject, transform.position, Quaternion.identity);
        popup.transform.SetParent(_popupsContainer, false);
        popup.GetComponent<Popup>().SetText(content);

    }

    [ContextMenu("Spawn More Popups")]
    private void SpawnPopup()
    {
        SpawnPopup("test test test test");
    }
}
