using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SimpleCamControl : MonoBehaviour
{
    private Camera MainCam;
    [Range(0, 25)]
    public float Speed = 3;

    void Start()
    {
        MainCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // receives inputs from WASD and Scrollwheel and multiplies the values with Speed variable
        float UpDown = Speed * Input.GetAxis("Vertical");
        float SideWays = Speed * Input.GetAxis("Horizontal");
        float Scroll = Speed * Input.mouseScrollDelta.y * -1;

        // inputs variables above to the used components
        MainCam.orthographicSize += Scroll;
        transform.position += new Vector3(SideWays, 0, UpDown);
    }
}
