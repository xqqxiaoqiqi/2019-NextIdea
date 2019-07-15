using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperation : MonoBehaviour
{

    /// <summary>
    /// 镜头移动所需参数
    /// </summary>
    private float LeftBorder;
    private float RightBorder;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MovableInterval;

    /// <summary>
    /// 镜头缩放参数
    /// </summary>
    [SerializeField] private float ZoomSpeed;
    [SerializeField] private float MinOrthoSize;
    [SerializeField] private float MaxOrthoSize;

    [SerializeField] private ElementManager _ElementManager;

    [SerializeField] private InventoryManager _InventoryManager;

    //供其余/本身检测鼠标焦点所用，当鼠标焦点从背景图转移到其余位置时，应重新设置此参数
    [HideInInspector] public bool IsFocusOnBackground;

    //判断当前状态下摄像机是否可以移动
    [HideInInspector] public bool CanCameraMove;

    private Camera mainCamera;


    private void Start()
    {
        CanCameraMove = true;

        IsFocusOnBackground = true;

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

        if (!IsFocusOnBackground) return;

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
        if (!Input.GetMouseButton(0) || _ElementManager.IsDragging || _InventoryManager.IsDraggingElement) return;

        //取消选中状态。
        //if (ElementManager != null) ElementManager.ClearSelectedElement();

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
