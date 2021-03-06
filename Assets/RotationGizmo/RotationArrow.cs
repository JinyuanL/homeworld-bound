﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationArrow : MonoBehaviour, IPointerClickHandler
{

    public RotationGizmo rotationGizmo;
    public RotationCounter rotationCounter;
    public bool tutorialLevel;

	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseOver()
    {
        if(!rotationGizmo.controlsDisabled)
        {
            Highlighter.Highlight(this.gameObject);
        }
    }

    private void OnMouseExit()
    {
        if(!rotationGizmo.controlsDisabled)
        {
            Highlighter.Unhighlight(this.gameObject);

        }
    }

    // only executes if this gameobject was the first one hit by mouse's raycast
    // so it won't fire if UI element is clicked and object is behind it, yay
    public void OnPointerClick(PointerEventData data)
    {
        if (!rotationGizmo.controlsDisabled)
        {
            Debug.Log("Tutorial Level? " + tutorialLevel);
            Debug.Log("Rotations remaining: " + rotationCounter.getRotationsRemaining());
            // if first time rotating, display Rotation Counter and warning message
            if(tutorialLevel && rotationCounter.getRotationsRemaining() == 10)
            {

                rotationCounter.doIntroRotationsRemaining();

            }
            switch (this.gameObject.name)
            {
                case "XUp":
                    rotationGizmo.incXRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }

                    if (Mathf.Approximately(this.transform.parent.transform.localEulerAngles.y, 180f))
                    {
                         StartCoroutine(rotationGizmo.Rotate(90f, 0f, 0f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("XDown");
                    }
                    else
                    {
                        StartCoroutine(rotationGizmo.Rotate(-90f, 0f, 0f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("XUp");
                    }
                    break;

                case "XDown":
                    //if (!CheckBattery())
                    //    break;
                    rotationGizmo.incXRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }

                     if (Mathf.Approximately(this.transform.parent.transform.localEulerAngles.y, 180f))
                     {
                        StartCoroutine(rotationGizmo.Rotate(-90f, 0f, 0f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("XUp");
                     }
                    else
                    {
                        StartCoroutine(rotationGizmo.Rotate(90f, 0f, 0f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("XDown");
                    }
                    break;

                case "YLeft":
                    //   if (!CheckBattery())
                    //      break;
                    rotationGizmo.incYRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }

                    StartCoroutine(rotationGizmo.Rotate(0f, 90f, 0f));
                    rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("YRight");
                    break;

                case "YRight":
                    //   if (!CheckBattery())
                    //        break;
                    rotationGizmo.incYRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }

                    StartCoroutine(rotationGizmo.Rotate(0f, -90f, 0f));
                    rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("YLeft");
                    break;

                case "ZUp":
                    //   if (!CheckBattery())
                    //       break;
                    rotationGizmo.incZRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }

                    if (Mathf.Approximately(this.transform.parent.transform.localEulerAngles.y, 270f))
                    {
                         StartCoroutine(rotationGizmo.Rotate(0f, 0f, 90f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("ZLeft");
                     }
                     else
                     {
                        StartCoroutine(rotationGizmo.Rotate(0f, 0f, -90f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("ZRight");
                    }
                    break;

                case "ZDown":
                    //   if (!CheckBattery())
                    //       break;
                    rotationGizmo.incZRots();
                    if (rotationGizmo.limitRotations && !rotationGizmo.isRotating())
                    {
                        rotationCounter.decrementRotations();
                    }
                    SimpleData.WriteDataPoint("Rotate_Object", rotationGizmo.getObjectToRotate().name, "", "", "", "Z");
                      if (Mathf.Approximately(this.transform.parent.transform.localEulerAngles.y, 270f))
                      {
                         StartCoroutine(rotationGizmo.Rotate(0f, 0f, -90f));
                         rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("ZRight");
                     }
                     else
                     {
                        StartCoroutine(rotationGizmo.Rotate(0f, 0f, 90f));
                        rotationGizmo.getObjectToRotate().GetComponent<OrientationTracker>().adjustFaceDirections("ZLeft");
                     }
                    break;

                default:
                    break;
            }
        }
    }

        // Update is called once per frame
        void Update () {
		
	}
}
