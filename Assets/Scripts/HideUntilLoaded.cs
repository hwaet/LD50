using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUntilLoaded : MonoBehaviour
{
    CanvasGroup canvasGroup;

	public void Start()
	{
        canvasGroup = GetComponent<CanvasGroup>();
    }

	public void HideGUI()
	{
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
	}

	// Start is called before the first frame update
	void OnEnable()
    {
        SceneWrangler.sceneLoadGUI += ShowGUI;
        SceneWrangler.sceneLoadEnd += HideGUI;

    }

    // Update is called once per frame
    void OnDisable()
    {
        SceneWrangler.sceneLoadGUI -= ShowGUI;
        SceneWrangler.sceneLoadEnd -= HideGUI;
    }

    public void ShowGUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
