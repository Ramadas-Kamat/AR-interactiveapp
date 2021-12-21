using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour {

    private enum State : int {
        Idle = 0,
        FindingGround = 1,
        PickMesh = 2
    }

    // Editor Fields
    public ARSessionOrigin sessionOrigin;
    public ARPointCloudManager pointCloudManager;
    public ARPlaneManager planeManager;
    public GameObject asset;
    public ModelDialog dialog;

    // Variables
    private NativeArray<XRRaycastHit> raycastHits = new NativeArray<XRRaycastHit>();
    private Ray inputRay;
    private State state = State.Idle;

    // Unity Messages
    private void Start() {
        state = State.FindingGround;
    }

    private void OnEnable() {
        SetEvents();
    }

    private void OnDisable() {
        ClearEvents();
    }

    private void Update() {
        if (state == State.FindingGround) {
            RaycastDetectAndPlace();
        }
        if (state == State.PickMesh) {
            RaycastPickMesh();
        }
    }

    // Methods
    private void RaycastDetectAndPlace() {
        if (Input.GetMouseButtonDown(0))
        {
            inputRay = sessionOrigin.camera.ScreenPointToRay(Input.mousePosition);
            raycastHits = planeManager.Raycast(inputRay, TrackableType.All, Allocator.Temp);
            if (raycastHits.Length > 0) {
                asset.transform.position = raycastHits[0].pose.position;
                state = State.PickMesh;
            }
        }
    }

    private void RaycastPickMesh() {
        if (Input.GetMouseButtonDown(0) && !dialog.isVisible) {
            inputRay = sessionOrigin.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            LayerMask layerMask = LayerMask.GetMask(new string[] { "3D Model" });
            bool collided = Physics.Raycast(inputRay, out raycastHit, 10, layerMask);
            if (collided) {
                dialog.Set(raycastHit.collider.name);
            }
        }
    }

    private void SetEvents() {
        pointCloudManager.pointCloudsChanged += OnPointCloudsChanged;
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void ClearEvents() {
        pointCloudManager.pointCloudsChanged -= OnPointCloudsChanged;
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPointCloudsChanged(ARPointCloudChangedEventArgs pEventArgs) {
        // Debug.Log("Point Cloud Changed");
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs pEventArgs) {
        // Debug.Log($"AR Planes Changed. Added {pEventArgs.added.Count}, Updated {pEventArgs.updated.Count}, Removed {pEventArgs.removed.Count}");
    }
}
