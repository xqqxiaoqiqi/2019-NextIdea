using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{

    private Transform mainCamera;
    private CameraOperation camOper;


    public bool CanDrag = false;
    public bool IsDragging = false;
    private Transform SelectedElement;
    private Vector3 OrigionPosition;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        camOper = mainCamera.GetComponent<CameraOperation>();
    }

    private void Update()
    {
        OnSelectElement();


        OnDragElement();

    }

    #region Drag Operation

    /// <summary>
    /// 检测是否点击到元件
    /// </summary>
    private void OnSelectElement()
    {

        if (!Input.GetMouseButtonDown(0)) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction , out hit);
        //Debug.DrawLine(ray.origin , hit.point , Color.black , 10);
        //Debug.Log(hit.transform.name);
        if (hit.transform != null && hit.transform.CompareTag("Wire"))
        {
            //进入选中状态
            //Invoke("SetCanDrag" , 1f);
            CanDrag = true;
            SetSelectedElement(hit.transform);

            //TODO:: 开启元件闪烁脚本动画
            Debug.Log("Element Name: " + hit.transform.name);

            //弹出操作按钮
        }
        else
        {
            //取消选中效果
            ClearSelectedElement();

        }
    }

    //private void SetCanDrag()
    //{
    //    CanDrag = true;
    //}

    /// <summary>
    /// 拖动Element
    /// </summary>
    private void OnDragElement()
    {
        if (Input.GetMouseButtonDown(0) && CanDrag)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, ray.direction, out hit);

            if ( SelectedElement != null && SelectedElement == hit.transform)
            {
                IsDragging = true;
            }
        }

        //TODO :: 添加Offset，使拖动效果更加自然
        if (Input.GetMouseButton(0) && IsDragging)
        {
            //拖动操作
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SelectedElement.position = new Vector3(pos.x, pos.y, -1);

        }

        //放开后检测当前位置
        if (Input.GetMouseButtonUp(0) && IsDragging)
        {
            //将元件放置到新的位置
            //TODO :: 目前射线检测位置为当前选中元件的中点，加入Offset参数后将其变为当前鼠标点击位置
            RaycastHit hit;
            Vector3 ori = new Vector3(SelectedElement.position.x, SelectedElement.position.y, -0.5f);
            Vector3 dir = new Vector3(SelectedElement.position.x, SelectedElement.position.y, 1);
            Physics.Raycast(ori, SelectedElement.forward, out hit);

            //Debug.DrawLine(ori, hit.point, Color.red, 10);
            //Debug.Log(hit.transform.name + "   " + hit.transform.position);

            if (hit.transform != null && hit.transform.CompareTag("Land"))
            {
                SelectedElement.parent = hit.transform;
                SelectedElement.localPosition = new Vector3(0, 0, -0.1f);
            }
            else
            {
                SelectedElement.position = OrigionPosition;
            }


            //clear all info
            CanDrag = false;
            IsDragging = false;
            ClearSelectedElement();
        }

    }

    #endregion

    public Transform GetSelectedElement()
    {
        return SelectedElement;
    }

    public void SetSelectedElement(Transform element)
    {
        OrigionPosition = element.position;

        SelectedElement = element;

        //TODO :: 在此处开启Element被选中动画
        //Debug.Log("Got yah!");
    }

    public void ClearSelectedElement()
    {
        SelectedElement = null;

        OrigionPosition = Vector3.zero;

        camOper.IsFocusOnBackground = true;

        //TODO :: 由Element中提供接口，关闭闪烁动画
        Debug.Log("Clear!");
    }

}
