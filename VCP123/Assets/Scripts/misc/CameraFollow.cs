using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float minXClamp = -118.2f;
    public float maxXClamp = 240.17f;
    public float minYClamp = -5f;
    public float maxYClamp = 5.40f;
    // Start is called before the first frame update
    //void Start()

    // Update is called once per frame
    //void Update()

    // Runs as fast your cpu runs - means that the time between updates is not standard
    //private void Update()

    // This function runs on a fixed update cycle which means that the time between updates remains standard
    // private void FixedUpdate()

    //This function always runs after fixed update - unity specifies this is where camera movement should happen
    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;

        cameraPos.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;

    }



}
