using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftClickToJiggleSampleScript : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject FPSPlayer;
    float switchViewCooldown = 0f;
    public Text PressEText;
    
    void Update () {
        //if clicked on left mouse  
        if (Input.GetMouseButtonDown(0))
        {
            //sending a raycast from the mouse position to the world
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the raycast hit a collider
            if (Physics.Raycast(ray,out hit))
            {
                //using the 'JiggleSystem' to jiggle all the materials at the 
                //hit position of the raycast, in radius of 1f units.
                JiggleSystem.
                    JiggleMaterials(
                    1f,             //radius in which all materials will jiggle
                    hit.point);     //center jiggle position
            }
        }

        SwitchBtwnTopCamAndFPS();
	}

    //this method switches between camera modes
    private void SwitchBtwnTopCamAndFPS()
    {
        if (Input.GetKey(KeyCode.E) && switchViewCooldown <= 0f)
        {
            MainCamera.SetActive(!MainCamera.activeSelf);
            FPSPlayer.SetActive(!FPSPlayer.activeSelf);

            PressEText.text =
                MainCamera.activeSelf ?
                "Press 'E' to switch to FPS controls" :
                "Press 'E' to switch to Top view controls";

            if (MainCamera.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            switchViewCooldown = 1.5f;
        }
        if (switchViewCooldown > 0f)
            switchViewCooldown -= Time.deltaTime;
    }
}
