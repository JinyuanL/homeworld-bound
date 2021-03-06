﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour {

    public GameObject eventSystem;
    public Camera mainCam;

    public Button fuseButton;
	public Button b1p1Button;
    public Button b1p2Button;
    public Button b1p3Button;
    public Button controlsButton;
    public CanvasGroup bottomPanel;
    public Button goToNextTutorial;

    public GameObject rotationGizmo;
	private RotationGizmo rotationScript;
	public Highlighter highlighter;

    public GameObject xUp;
    public GameObject xDown;
    public GameObject yUp;
    public GameObject yDown;
    public GameObject zUp;
    public GameObject zDown;

    public GameObject bb1Start;
    public GameObject bb1;
    public GameObject bb1_b1p1_a1;
    public GameObject bb1_b1p2_a1;
    public GameObject bb1_b1p2_a2;
    public GameObject bb1_b1p3_a1;


    public Text shapesWrong;
	public Text rotationWrong;
	public Text congrats;

    public GameObject finishedImage;

    private SelectPart selectPart;
    private FuseEvent fuseEvent;
    private CameraControls cameraControls;
    public ScrollingText scrollingText;
    private GameObject b1p1;
    private GameObject b1p2;
    private Vector3 baseStartPosition;
    private Button[] partButtons;

    private Tooltip[] allTooltips;

    private const float MOVEMENT_SPEED = 100f;
    private const float SHOW_IMAGE_DURATION = 2f;
    private float step;

    private bool tooltipsEnabled;
    private GameObject selectedObj;

	void Awake() {
	}

	// Use this for initialization
	void Start () {


    }

    void OnEnable()
    {
        tooltipsEnabled = false;

        rotationScript = rotationGizmo.GetComponent<RotationGizmo>();
        fuseEvent = eventSystem.GetComponent<FuseEvent>();
        selectPart = eventSystem.GetComponent<SelectPart>();
        cameraControls = mainCam.GetComponent<CameraControls>();
        baseStartPosition = new Vector3(-100, 30, 100);

        fuseEvent.setIsFirstLevel(true);

        partButtons = new Button[3];
        partButtons[0] = b1p1Button;
        partButtons[1] = b1p2Button;
        partButtons[2] = b1p3Button;

        // tooltips occur on: all part buttons, Fuse button, Finished Image, bb1 child
        allTooltips = new Tooltip[16];
        allTooltips[0] = b1p1Button.gameObject.GetComponent<Tooltip>();
        allTooltips[1] = b1p2Button.gameObject.GetComponent<Tooltip>();
        allTooltips[2] = b1p3Button.gameObject.GetComponent<Tooltip>();
        allTooltips[3] = finishedImage.GetComponent<Tooltip>();
        allTooltips[4] = fuseButton.gameObject.GetComponent<Tooltip>();
        allTooltips[5] = bb1.GetComponent<Tooltip>();
        allTooltips[6] = yUp.GetComponent<Tooltip>();
        allTooltips[7] = yDown.GetComponent<Tooltip>();
        allTooltips[8] = xUp.GetComponent<Tooltip>();
        allTooltips[9] = xDown.GetComponent<Tooltip>();
        allTooltips[10] = zUp.GetComponent<Tooltip>();
        allTooltips[11] = zDown.GetComponent<Tooltip>();
        allTooltips[12] = bb1_b1p1_a1.GetComponent<Tooltip>();
        allTooltips[13] = bb1_b1p2_a1.GetComponent<Tooltip>();
        allTooltips[14] = bb1_b1p2_a2.GetComponent<Tooltip>();
        allTooltips[15] = bb1_b1p3_a1.GetComponent<Tooltip>();

        //make sure all tooltips are disabled on startup
        for (int i = 0; i < allTooltips.Length; i++)
        {
            allTooltips[i].enabled = false;
        }

    }

    public void disableTooltips()
    {
        allTooltips = FindObjectsOfType<Tooltip>();
        for(int i = 0; i < allTooltips.Length; i++)
        {
            allTooltips[i].enabled = false;
            for (int t = 0; t < allTooltips[i].displayTokens.Length; t++)
            {
                ConversationTrigger.RemoveToken(allTooltips[i].displayTokens[t]);
            }

        }
        // makes any currently displayed tooltip go away
        ConversationController.Disable();
    }

    public void enableTooltips()
    {
        allTooltips = FindObjectsOfType<Tooltip>();
        for (int i = 0; i < allTooltips.Length; i++)
        {
            allTooltips[i].enabled = true;
        }

    }


    // Update is called once per frame
    void Update()
    {
        //step = MOVEMENT_SPEED * Time.deltaTime;
        //bb1Start.transform.position = Vector3.MoveTowards(bb1Start.transform.position, baseStartPosition, step);
        // Wait till intro text is done, then enable Tooltip scripts on each of the objects that have them
        // Also after intro text is done, change ScrollingText's enableScroll to false
        if (!tooltipsEnabled && ConversationTrigger.GetToken("finished_cameraControls"))
        {
            tooltipsEnabled = true;
            scrollingText.enableScroll = false;
            for (int i = 0; i < allTooltips.Length; i++)
            {
                allTooltips[i].enabled = true;
            }

        }


        //if (!disableTutorial)
        //{
        //    // show Dresha moving starting part into position from bottom of screen
        //    if (!ConversationTrigger.GetToken("finishedMovingbb1") && ConversationTrigger.GetToken("finishedConst_1"))
        //    {
        //        step = MOVEMENT_SPEED * Time.deltaTime;
        //        bb1.transform.position = Vector3.MoveTowards(bb1.transform.position, baseStartPosition, step);
        //        if (bb1.transform.position.Equals(baseStartPosition))
        //        {
        //            Debug.Log("finished moving!");
        //            ConversationTrigger.AddToken("finishedMovingbb1");
        //        }
        //    }

        //    // draw player's attention to the part buttons at the bottom of the screen
        //    else if (!flashedPartButtons && ConversationTrigger.GetToken("finishedConst_2"))
        //    {
        //        flashedPartButtons = true;
        //        StartCoroutine(showImageAndAddToken(arrowPartButtons, SHOW_IMAGE_DURATION, "finishedFlashingPartButtons"));
        //        highlightPartButtons(2f);

        //    }

        //    // Dresha clicks the part button, part appears
        //    else if (!clickedB1P1Button && ConversationTrigger.GetToken("finishedConst_3"))
        //    {
        //        clickedB1P1Button = true;
        //        b1p1Button.onClick.Invoke();
        //        StartCoroutine(waitThenAddToken("finishedSelectingPart", 2f));
        //        StartCoroutine(waitThenMoveCamera(-32f, -13f, 0f, -70f, -25.5f, 13.76f, 1f, 1f));

        //    }

        //    // draw player's attention to the finished image at the top left of the screen
        //    else if (!flashedFinishedImage && ConversationTrigger.GetToken("finishedConst_4"))
        //    {
        //        flashedFinishedImage = true;
        //        StartCoroutine(showImageAndAddToken(arrowFinishedImageLeft.GetComponent<Image>(), SHOW_IMAGE_DURATION, "finishedFlashingFinishedImage"));
        //        StartCoroutine(showImage(arrowFinishedImageUp.GetComponent<Image>(), SHOW_IMAGE_DURATION));
        //        highlighter.HighlightTimed(finishedImage, 2);

        //    }

        //    // Dresha selects the black area on bb1
        //    else if (!selectedFuseTo && ConversationTrigger.GetToken("finishedConst_5"))
        //    {
        //        selectedFuseTo = true;
        //        //hardcoded normal here since we don't have pointer raycast
        //        selectPart.setFuseToNormal(Vector3.right);
        //        bb1_b1p2_a1 = GameObject.Find("bb1_b1p2_a1");
        //        StartCoroutine(waitThenSelectFuseTo(bb1_b1p2_a1, 1f));
        //        StartCoroutine(waitThenAddToken("finishedSelectingbb1_a1", 4f));
        //    }

        //    // Dresha selects the black area on b1p1
        //    else if (!selectedAC && ConversationTrigger.GetToken("finishedConst_6"))
        //    {
        //        selectedAC = true;
        //        b1p1 = GameObject.Find("b1p1Prefab(Clone)");
        //        b1p1_bb1_a1 = b1p1.transform.GetChild(1).gameObject;

        //        StartCoroutine(waitThenSelectObject(b1p1_bb1_a1, 2f));
        //        StartCoroutine(waitThenAddToken("finishedSelectingb1p1_a1", 4f));
        //    }

        //    // Dresha rotates once along y axis - should go so the black part is facing up
        //    else if (!rotatedOnceWrongPart && ConversationTrigger.GetToken("finishedConst_7"))
        //    {
        //        rotatedOnceWrongPart = true;
        //        StartCoroutine(rotateWrongPartScript());
        //    }

        //    //Then Dresha rotates once along z axis - should go so the black part is facing bb1
        //    else if (!rotatedTwiceWrongPart && ConversationTrigger.GetToken("finishedConst_8"))
        //    {
        //        rotatedTwiceWrongPart = true;
        //        StartCoroutine(rotateTwiceWrongPartScript());
        //    }

        //    //Then Fuse button is shown/pointed to
        //    else if (!flashedFuseButton && ConversationTrigger.GetToken("finishedConst_9"))
        //    {
        //        flashedFuseButton = true;
        //        StartCoroutine(showImageAndAddToken(arrowFuseButton.GetComponent<Image>(), SHOW_IMAGE_DURATION, "finishedFlashingFuseButton"));
        //        highlighter.HighlightTimed(fuseButton.gameObject, 2);
        //    }

        //    //Then Dresha tries to attach the wrong two parts
        //    else if (!attemptedWrongPartFuse && ConversationTrigger.GetToken("finishedConst_10"))
        //    {
        //        attemptedWrongPartFuse = true;
        //        StartCoroutine(waitThenInitiateFuse(1f));
        //        StartCoroutine(waitThenAddToken("finishedWrongPartFuseAttempt", 2f));
        //    }

        //    //Then Dresha selects a different part - the correct one (b1p2)
        //    else if (!clickedB1P2Button && ConversationTrigger.GetToken("finishedConst_11"))
        //    {
        //        clickedB1P2Button = true;
        //        b1p2Button.onClick.Invoke();
        //        StartCoroutine(waitThenAddToken("finishedSelectingSecondPart", 2f));
        //    }

        //    //Then Dresha selects the wrong black area on b1p2
        //    else if (!selectedSecondAC && ConversationTrigger.GetToken("finishedConst_12"))
        //    {
        //        selectedSecondAC = true;
        //        // move camera so the black area to be selected is visible
        //        StartCoroutine(waitThenMoveCamera(-6.8f, -61.9f, 0f, 1.48f, 17.63f, 51.15f, 1f, 1f));

        //        b1p2 = GameObject.Find("b1p2Prefab(Clone)");
        //        b1p2_bb1_a2 = b1p2.transform.GetChild(2).gameObject;

        //        StartCoroutine(waitThenSelectObject(b1p2_bb1_a2, 2f));
        //        StartCoroutine(waitThenAddToken("finishedSelectingb1p2_a2", 4f));
        //    }

        //    //Then Dresha rotates again
        //    else if (!rotatedOnceWrongFace && ConversationTrigger.GetToken("finishedConst_13"))
        //    {
        //        rotatedOnceWrongFace = true;
        //        // move camera so player can see how the part is aligned as it's rotated
        //        StartCoroutine(waitThenMoveCamera(39.7f, -3.1f, 0f, -85.65f, 96.72f, 19.76f, 1f, 1f));

        //        StartCoroutine(rotateWrongFaceScript());
        //    }

        //    //Then Dresha tries to attach to wrong black area
        //    else if (!attemptedWrongFaceFuse && ConversationTrigger.GetToken("finishedConst_14"))
        //    {
        //        attemptedWrongFaceFuse = true;
        //        fuseEvent.initiateFuse();
        //        StartCoroutine(waitThenAddToken("finishedWrongFaceFuseAttempt", 3f));
        //    }

        //    //Then Dresha selects the correct black area on b1p2
        //    else if (!selectedThirdAC && ConversationTrigger.GetToken("finishedConst_15"))
        //    {
        //        selectedThirdAC = true;
        //        // move camera so the black area to be selected is visible
        //        //StartCoroutine(waitThenMoveCamera(-6.8f, -61.9f, 0f, 1.48f, 17.63f, 51.15f, 1f, 1f));

        //        b1p2_bb1_a1 = b1p2.transform.GetChild(1).gameObject;

        //        StartCoroutine(waitThenSelectObject(b1p2_bb1_a1, 2f));
        //        selectPart.deselectObject(b1p2_bb1_a2);
        //        StartCoroutine(waitThenAddToken("finishedSelectingb1p2_a1", 4f));
        //    }

        //    //Then Dresha tries to fuse with incorrect rotation
        //    else if (!attemptedWrongRotationFuse && ConversationTrigger.GetToken("finishedConst_16"))
        //    {
        //        attemptedWrongRotationFuse = true;
        //        StartCoroutine(waitThenInitiateFuse(1f));
        //        StartCoroutine(waitThenAddToken("finishedWrongRotationFuseAttempt", 2f));
        //    }

        //    //Then Dresha enables your controls
        //    else if (!enabledControls && ConversationTrigger.GetToken("finishedConst_17"))
        //    {
        //        enabledControls = true;
        //        bottomPanel.blocksRaycasts = true;
        //        cameraControls.controlsDisabled = false;
        //        rotationScript.controlsDisabled = false;
        //        selectPart.controlsDisabled = false;
        //        enableControlsAndAddToken("finishedEnablingControls");
        //    }
        //    // Once player attaches their first part, Dresha congratulates them

        //    // Once player finishes building, Dresha says we've got more batteries to build




    }







    private void enableControlsAndAddToken(string token)
    {
        highlightGizmo(4f);
        for(int i = 0; i < partButtons.Length; i++)
        {
            highlighter.HighlightTimed(partButtons[i].gameObject, 4f);

        }
        highlighter.HighlightTimed(fuseButton.gameObject, 4f);
        highlighter.HighlightTimed(controlsButton.gameObject, 4f);
        ConversationTrigger.AddToken(token);
    }

    IEnumerator rotateWrongPartScript()
    {
        b1p1 = GameObject.Find("b1p1Prefab(Clone)");
        Highlighter.Highlight(yUp); 
        yield return new WaitForSeconds(2f);
        Highlighter.Unhighlight(yUp);
        rotationScript.runManualRotation(b1p1, 0,90,0);
        yield return new WaitForSeconds(2f);
        Highlighter.Highlight(zUp);

        ConversationTrigger.AddToken("finishedRotatingOnceWrongPart");

    }

    IEnumerator rotateTwiceWrongPartScript()
    {
        Highlighter.Unhighlight(zUp);
        rotationScript.runManualRotation(b1p1, 0, 0, -90);
        yield return new WaitForSeconds(2f);
        ConversationTrigger.AddToken("finishedRotatingTwiceWrongPart");
    }

    IEnumerator rotateWrongFaceScript()
    {
        b1p2 = GameObject.Find("b1p2Prefab(Clone)");
        Highlighter.Highlight(zDown);
        yield return new WaitForSeconds(2f);
        rotationScript.runManualRotation(b1p2, 0, 0, 90);
        yield return new WaitForSeconds(2f);
        Highlighter.Unhighlight(zDown);
        rotationScript.runManualRotation(b1p2, 0, 0, 90);
        yield return new WaitForSeconds(2f);

        ConversationTrigger.AddToken("finishedRotatingWrongFace");

    }

    IEnumerator rotateWrongRotationScript()
    {
        Highlighter.Highlight(yDown);
        yield return new WaitForSeconds(2f);
        rotationScript.runManualRotation(b1p2, 0, -90, 0);
        yield return new WaitForSeconds(2f);
        Highlighter.Unhighlight(yDown);
        rotationScript.runManualRotation(b1p2, 0, -90, 0);
        yield return new WaitForSeconds(2f);

        ConversationTrigger.AddToken("finishedRotatingWrongRotation");

    }

    IEnumerator waitThenMoveCamera(float rot_x, float rot_y, float rot_z, float pos_x, float pos_y, float pos_z, float secondsForRotating, float secondsForWaiting)
    {
        yield return new WaitForSeconds(secondsForWaiting);
        cameraControls.autoRotateCamera(rot_x, rot_y, rot_z, pos_x, pos_y, pos_z, secondsForRotating);

    }

    IEnumerator waitThenInitiateFuse(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        fuseEvent.initiateFuse();
    }

    IEnumerator waitThenSelectObject(GameObject toSelect, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        selectPart.selectObject(toSelect);

    }

    IEnumerator waitThenSelectFuseTo(GameObject toSelect, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        selectPart.selectFuseTo(toSelect);

    }

    IEnumerator waitThenAddToken(string token, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ConversationTrigger.AddToken(token);
        Debug.Log("Added token " + token + " successfully!");
    }

    IEnumerator showImage(Image imgToFlash, float time)
    {
        imgToFlash.enabled = true;
        yield return new WaitForSeconds(time);
        imgToFlash.enabled = false;

    }

    IEnumerator showImageAndAddToken(Image imgToFlash, float time, string token)
    {
        imgToFlash.enabled = true;
        yield return new WaitForSeconds(time);
        imgToFlash.enabled = false;

        ConversationTrigger.AddToken(token);
    }

    private void highlightPartButtons(float seconds) {
		foreach (Button b in partButtons) {
			highlighter.HighlightTimed(b.gameObject, seconds);
		}

        //	ConversationTrigger.AddToken("dreshaFlashedPartButtons");

    }
	

	private void highlightGizmo(float seconds) {
		// maybe should highlight only the sliders instead?
		foreach(Transform child in rotationGizmo.transform) {
			highlighter.HighlightTimed(child.gameObject, seconds); 
		}
	}


		
}
