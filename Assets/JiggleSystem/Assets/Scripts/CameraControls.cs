using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    Vector2 MouseLook;
    Vector2 SmoothV;

    [SerializeField]
    private float MouseSensitivity = 2f;
    [SerializeField]
    private float Smoothing = 2f;

    Transform PlayerTransform;

	// Use this for initialization
	void Start () {
        PlayerTransform = transform.parent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, 
            new Vector2(MouseSensitivity * Smoothing, MouseSensitivity * Smoothing));
        SmoothV.x = Mathf.Lerp(SmoothV.x, md.x, 1f / Smoothing);
        SmoothV.y = Mathf.Lerp(SmoothV.y, md.y, 1f / Smoothing);
        MouseLook += SmoothV;

        MouseLook.y = Mathf.Clamp(MouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
        PlayerTransform.localRotation = Quaternion.AngleAxis(MouseLook.x, PlayerTransform.up);
	}
}
