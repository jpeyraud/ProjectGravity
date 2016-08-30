using UnityEngine;
using System.Collections;

public class ViveControllerClickEventSample : MonoBehaviour {

    // Get the id of tracked object (where this script is attached)
    private SteamVR_TrackedObject trackedObj;

    // Get the id of the current tracked object
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    // Get a the id of the Vive trigger button
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    public GameObject targetToInstanciate;

    public Color pointerColor;
    public float pointerThickness = 0.1f;
    public AxisType pointerFacingAxis = AxisType.ZAxis;
    public float pointerLength = 100f;
    public bool showPointerTip = true;
    public bool teleportWithPointer = true;
    public float blinkTransitionSpeed = 0.6f;

    public bool highlightGrabbableObject = true;
    public Color grabObjectHightlightColor;

    private SteamVR_TrackedObject trackedController;
    private SteamVR_Controller.Device device;

    private GameObject pointerHolder;
    private GameObject pointer;
    private GameObject pointerTip;

    private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);

    private float pointerContactDistance = 0f;
    private Transform pointerContactTarget = null;

    private Rigidbody controllerAttachPoint;
    private FixedJoint controllerAttachJoint;
    private GameObject canGrabObject;
    private Color[] canGrabObjectOriginalColors;
    private GameObject previousGrabbedObject;

    private Transform HeadsetCameraRig;
    private float HeadsetCameraRigInitialYPosition;
    private Vector3 TeleportLocation;

	private float currentDelta;
	private float SpawnTickTime = 1.0f;

	public GameObject SpawnableItem;

    public enum AxisType
    {
        XAxis,
        ZAxis
    }

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
		currentDelta = 0.0f;
        InitPointer();
    }
	
	// Update is called once per frame
	void Update () {
        if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }
		currentDelta += Time.deltaTime;
        // check if the trigger button is down or up
		if (controller.GetPressDown (triggerButton)) {
			

			if (currentDelta >= SpawnTickTime && SpawnableItem != null) {
				GameObject newItem = (GameObject)Instantiate (SpawnableItem, transform.position, transform.rotation);
				Vector3 force = gameObject.transform.forward;
				force.Scale (new Vector3 (10.0f, 10.0f, 10.0f));
				newItem.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
				currentDelta = 0.0f;
				// Do a raycast to see if a target has been hit
				/*RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (System.String.Equals(hit.transform.gameObject.tag, "Target")) {
                    // Destroy the object hit by the raycast and create a new one near it
                    Transform newPosition = getNewPos(hit.transform);
                    Destroy(hit.transform.gameObject);
                }
            }*/
			}
		}
    }

    private Transform getNewPos(Transform transform) {
        Transform trans = transform;

        switch ((int)(Random.value * 5f))
        {
            case 0:
                trans.Translate(Vector3.up * 2f);
                break;
            case 1:
                trans.Translate(Vector3.forward * 2f);
                break;
            case 2:
                trans.Translate(Vector3.left * 2f);
                break;
            case 3:
                trans.Translate(Vector3.down * 2f);
                break;
            case 4:
                trans.Translate(Vector3.back * 2f);
                break;
            case 5:
                trans.Translate(Vector3.right * 2f);
                break;
        }
        return trans;
    }


    // *********************** CODE FOUND ON INTERNET *********************** //

    float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
    {
        float actualLength = pointerLength;

        //reset if beam not hitting or hitting new target
        if (!hasRayHit || (pointerContactTarget && pointerContactTarget != collidedWith.transform))
        {
            pointerContactDistance = 0f;
            pointerContactTarget = null;
        }

        //check if beam has hit a new target
        if (hasRayHit)
        {
            if (collidedWith.distance <= 0)
            {

            }
            pointerContactDistance = collidedWith.distance;
            pointerContactTarget = collidedWith.transform;
        }

        //adjust beam length if something is blocking it
        if (hasRayHit && pointerContactDistance < pointerLength)
        {
            actualLength = pointerContactDistance;
        }

        return actualLength; ;
    }


    void SetPointerTransform(float setLength, float setThicknes)
    {
        //if the additional decimal isn't added then the beam position glitches
        float beamPosition = setLength / (2 + 0.00001f);

        if (pointerFacingAxis == AxisType.XAxis)
        {
            pointer.transform.localScale = new Vector3(setLength, setThicknes, setThicknes);
            pointer.transform.localPosition = new Vector3(beamPosition, 0f, 0f);
            pointerTip.transform.localPosition = new Vector3(setLength - (pointerTip.transform.localScale.x / 2), 0f, 0f);
        }
        else
        {
            pointer.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
            pointer.transform.localPosition = new Vector3(0f, 0f, -beamPosition);
            pointerTip.transform.localPosition = new Vector3(0f, 0f, setLength - (pointerTip.transform.localScale.z / 2));
        }

    }
    void InitPointer()
    {
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", pointerColor);

        pointerHolder = new GameObject();
        pointerHolder.transform.parent = this.transform;
        pointerHolder.transform.localPosition = Vector3.zero;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = pointerHolder.transform;
        pointer.GetComponent<MeshRenderer>().material = newMaterial;

        pointer.GetComponent<BoxCollider>().isTrigger = true;
        pointer.AddComponent<Rigidbody>().isKinematic = true;
        pointer.layer = 2;

        pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pointerTip.transform.parent = pointerHolder.transform;
        pointerTip.GetComponent<MeshRenderer>().material = newMaterial;
        pointerTip.transform.localScale = pointerTipScale;

        pointerTip.GetComponent<SphereCollider>().isTrigger = true;
        pointerTip.AddComponent<Rigidbody>().isKinematic = true;
        pointerTip.layer = 2;

        SetPointerTransform(pointerLength, pointerThickness);
        TogglePointer(true);
    }

    void TogglePointer(bool state)
    {
        pointer.gameObject.SetActive(state);
        bool tipState = (showPointerTip ? state : false);
        pointerTip.gameObject.SetActive(tipState);
    }


}
