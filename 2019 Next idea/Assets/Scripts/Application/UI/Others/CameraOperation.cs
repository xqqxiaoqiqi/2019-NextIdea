/**
 * 作用 ： 进行缩放，横移等摄像机上的操作
 * 挂载位置 ： MainCamera
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraOperation : MonoBehaviour
{
    /// <summary>
    /// 镜头移动所需参数
    /// </summary>
    private float LeftBorder = 0;
    private float RightBorder = 0;
    [SerializeField]
    private float MoveSpeed = 0;
    [SerializeField]
    private float MovableInterval =0;
    /// <summary>
    /// 镜头缩放参数
    /// </summary>
    [SerializeField]
    private float ZoomSpeed = 0;
    [SerializeField]
    private float MinOrthoSize = 0;
    [SerializeField]
    private float MaxOrthoSize = 0;

    //判断当前状态下摄像机是否可以移动
    [HideInInspector]
    public bool CanCameraMoving=false;

    [HideInInspector] public bool CanCameraZooming=false;

    private Camera mainCamera;

    private void Start()
    {
        CanCameraMoving = true;
        mainCamera = transform.GetComponent<Camera>();
        LeftBorder = transform.position.x - MovableInterval;
        RightBorder = transform.position.x + MovableInterval;
    }

    void Update()
    {
        Zooming();
        Moving();
    }

    #region Normal Moving
    private void Zooming()
    {
        if (!CanCameraZooming) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > MinOrthoSize)
        {
            // Debug.Log("Zoom in");
            mainCamera.orthographicSize -= ZoomSpeed * Time.deltaTime;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < MaxOrthoSize)
        {
            //Debug.Log("Zoom OUT!");
            mainCamera.orthographicSize += ZoomSpeed * Time.deltaTime;
        }
    }

    private void Moving()
    {
        //TODO :: 不可移动条件不完善
        if (!Input.GetMouseButton(0) || !CanCameraMoving) return;

        if (Input.GetAxis("Mouse X") > 0 && transform.position.x > LeftBorder)
        {
            //Debug.Log("Moving Left");
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Mouse X") < 0 && transform.position.x < RightBorder)
        {
            //Debug.Log("Moving Right");
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
    }

    #endregion
}