using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveVector;
    public float moveMultiplier=0f;
    public double moveDuration;
    public Camera referenceCam;
    public Rigidbody rigidBody;
    public Vector2 screenPos;
    public AnimationCurve speedCurve;
    public RectTransform renderTexRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Matrix4x4 m = referenceCam.cameraToWorldMatrix;
        moveMultiplier = speedCurve.Evaluate(((float)moveDuration));
        
        rigidBody.MovePosition(this.transform.position + new Vector3(moveVector.x, moveVector.y,0)*Time.fixedDeltaTime);
        screenPos = Mouse.current.position.ReadValue();
        
    }

    private void LateUpdate()
	{
        this.transform.position = this.transform.position + (new Vector3(moveVector.x, moveVector.y, 0) * Time.fixedDeltaTime);

    }

	public void OnMove(InputAction.CallbackContext ctx)
	{
        moveVector = ctx.ReadValue<Vector2>();
        moveDuration = ctx.duration;
        if (ctx.performed) Debug.Log("Move");
        if (ctx.started) Debug.Log("MoveStarted");
        if (ctx.canceled)
        {
            Debug.Log("MoveCanceled");
            moveVector = Vector2.zero;
        }

    }

    public void OnFire(InputAction.CallbackContext ctx)
	{
        //convert click on render texture to a point in the render texture space, then fire a ray through that to a collider, from the render tex's camera
        RectTransformUtility.ScreenPointToLocalPointInRectangle(renderTexRectTransform, screenPos, null, out Vector2 localClick);
		localClick.x = (renderTexRectTransform.rect.xMin * -1) - (localClick.x * -1);
		localClick.y = (renderTexRectTransform.rect.yMin * -1) - (localClick.y * -1);
		Vector2 viewportClick = new Vector2(localClick.x / renderTexRectTransform.rect.size.x, localClick.y / renderTexRectTransform.rect.size.y);

        RaycastHit hit = new RaycastHit();
        Ray ray = referenceCam.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));
        if (Physics.Raycast(ray, out hit, 9999f) == true)
		{
			if (ctx.performed) Debug.Log("fire" + hit.collider);
            Scoop scoop = hit.collider.gameObject.GetComponent<Scoop>();

            scoop?.RegisterHit();
		}

	}
}
