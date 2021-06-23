using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple camera controller.
/// only meant for topdown in orthograpic mode
/// Can be use on mobile and pc
/// </summary>
[RequireComponent(typeof(Camera))]
public class SimpleCamControl : MonoBehaviour
{
    private Camera MainCam;
    [Range(-25, 25)]
    public float XSpeed = 3;
    [Range(-25, 25)]
    public float YSpeed = 3;

    [Range(1, 10)]
    public float TouchZoomSpeed;

    private float ZoomMinBound = 1;
    private float ZoomMaxBound = 50;

    public Vector3 TopBoundry;
    public Vector3 BottomBoundry;

    void Start()
    {
        MainCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    /// <summary>
    /// Control method used for PC and Mobile
    /// </summary>
    private void Controls()
    {
        if (Input.touchSupported)
        {
            // mobile only controls
            if (Input.touchCount == 1)
            {
                Touch Touch1 = Input.GetTouch(0);
            }
            else if (Input.touchCount == 2)
            {
                Touch Touch1 = Input.GetTouch(0);
                Touch Touch2 = Input.GetTouch(1);

                Vector2 tZeroPrevious = Touch1.position - Touch1.deltaPosition;
                Vector2 tOnePrevious = Touch2.position - Touch2.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(Touch1.position, Touch2.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {
            // PC only controls
            // receives inputs from WASD and Scrollwheel and multiplies the values with Speed variable
            float UpDown = XSpeed * Input.GetAxis("Horizontal");
            float SideWays = YSpeed * Input.GetAxis("Vertical");
            float Scroll = YSpeed * Input.mouseScrollDelta.y * -1;

            // inputs variables above to the used components
            MainCam.orthographicSize += Scroll;

            transform.position += new Vector3(SideWays, 0, UpDown);

        }
    }

    /// <summary>
    /// Zoom in function meant for mobile controles
    /// </summary>
    /// <param name="deltaMagnitudeDiff"></param>
    /// <param name="speed"></param>
    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        MainCam.orthographicSize += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        MainCam.orthographicSize = Mathf.Clamp(MainCam.orthographicSize, ZoomMinBound, ZoomMaxBound);
    }
}
