/**
 * 作用 ： 将UI元素拖拽至指定位置，判断位置合法后，生成对应拖拽的Wire
 * 挂载位置 ： 对应UIPrefeb
 * */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
public class ElementUIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool dragOnSurfaces = true;

    private CameraOperation cameraManager;

    [HideInInspector]
    public ElementInventory inventory;

    [HideInInspector]
    public Transform ElementsParent;

    [HideInInspector]
    public GameObject WirePhysicPrefeb;

    private Dictionary<int, GameObject> m_DraggingIcons = new Dictionary<int, GameObject>();
    private Dictionary<int, RectTransform> m_DraggingPlanes = new Dictionary<int, RectTransform>();


    private void Start()
    {
        cameraManager = Camera.main.GetComponent<CameraOperation>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcons[eventData.pointerId] = new GameObject("icon");

        m_DraggingIcons[eventData.pointerId].transform.SetParent(canvas.transform, false);
        m_DraggingIcons[eventData.pointerId].transform.SetAsLastSibling();

        var image = m_DraggingIcons[eventData.pointerId].AddComponent<Image>();
        // The icon will be under the cursor.
        // We want it to be ignored by the event system.
        var group = m_DraggingIcons[eventData.pointerId].AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlanes[eventData.pointerId] = transform as RectTransform;
        else
            m_DraggingPlanes[eventData.pointerId] = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);

        Camera.main.GetComponent<CameraOperation>().CanCameraMoving = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_DraggingIcons[eventData.pointerId] != null)
            SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            m_DraggingPlanes[eventData.pointerId] = eventData.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcons[eventData.pointerId].GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlanes[eventData.pointerId], eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlanes[eventData.pointerId].rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcons[eventData.pointerId] != null)
            Destroy(m_DraggingIcons[eventData.pointerId]);

        m_DraggingIcons[eventData.pointerId] = null;


        //Instantiate

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 InstantPos = new Vector3(mousePos.x, mousePos.y, -0.1f);
        Vector3 InstantDir = new Vector3(mousePos.x, mousePos.y, 1f);



        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out hit);
        //Debug.DrawLine(ray.origin, hit.point, Color.black, 10);
        //Debug.Log(hit.transform.name);

        try
        {
            if (hit.transform && hit.transform.CompareTag("Land"))
            {
                //Put the wire there
                GameObject GO = Instantiate(WirePhysicPrefeb, InstantPos, Quaternion.Euler(Vector3.zero), ElementsParent) as GameObject;
                GO.transform.parent = hit.transform;
                GO.transform.localPosition = new Vector3(0, 0, -0.1f);

                inventory.RemoveElement(transform.gameObject);
            }
            else
            {
                //TODO :: Make a Warning UI to tell the player current place have sth else.
            }
    }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            Camera.main.GetComponent<CameraOperation>().CanCameraMoving = true;
        }

    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        var t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
