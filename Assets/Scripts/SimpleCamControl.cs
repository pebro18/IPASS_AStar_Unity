using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SimpleCamControl : MonoBehaviour
{
    private Camera MainCam;
    [Range(-25, 25)]
    public float XSpeed = 3;
    [Range(-25, 25)]
    public float YSpeed = 3;

    void Start()
    {
        MainCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // receives inputs from WASD and Scrollwheel and multiplies the values with Speed variable
        float UpDown = XSpeed * Input.GetAxis("Horizontal");
        float SideWays = YSpeed * Input.GetAxis("Vertical");
        float Scroll = YSpeed * Input.mouseScrollDelta.y * -1;

        // inputs variables above to the used components
        MainCam.orthographicSize += Scroll;
        transform.position += new Vector3(SideWays, 0, UpDown);
    }
}
