using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order1 : MonoBehaviour
{
    public GameObject trans11;
    public GameObject trans12;
    public GameObject trans13;

    private GameObject part11;
    private GameObject part12;
    private GameObject part13;

    //拖拽组件
    private Drag1 drag11;
    private Drag1 drag12;
    private Drag1 drag13;

    //点击组件
    private Click click11;
    private Click click12;
    private Click click13;

    private Vector3 oldPosition11;
    private Vector3 oldPosition12;
    private Vector3 oldPosition13;

    private GameObject rotatePanel;
    private GameObject selectedPart;

    private void Awake()
    {
        part11 = GameObject.Find("parent_11");
        part12 = GameObject.Find("parent_12");
        part13 = GameObject.Find("parent_13");

        drag11 = part11.GetComponent<Drag1>();
        drag12 = part12.GetComponent<Drag1>();
        drag13 = part13.GetComponent<Drag1>();

        click11 = part11.GetComponent<Click>();
        click12 = part12.GetComponent<Click>();
        click13 = part13.GetComponent<Click>();

        rotatePanel = GameObject.Find("RotateControl");
    }

    void Start()
    {
        trans11.SetActive(true);
        trans12.SetActive(false);
        trans13.SetActive(false);

        drag11.enabled=false;
        drag12.enabled=false;
        drag13.enabled=false;

        click11.enabled=false;
        click12.enabled=false;
        click13.enabled=false;

        oldPosition11 = part11.transform.position;
        oldPosition12 = part12.transform.position;
        oldPosition13 = part13.transform.position;
    }

    void Update()
    {
        //启动旋转按钮
        Ray ray = new Ray(new Vector3(-125, 40, 0), Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f))
        {
            selectedPart = hit.collider.gameObject;
            Debug.Log(selectedPart.name);
        }
        else
        {
            selectedPart = null;
        }

        if (selectedPart != null)
        {
            rotatePanel.SetActive(true);
        }
        else
        {
            rotatePanel.SetActive(false);
        }

        //启动点击组件（保证每个part只有在原位置才能被点击）
        if (part11.transform.position == oldPosition11)
        {
            click11.enabled = true;
            drag11.enabled = false;
        }
        else
        {
            click11.enabled = false;
        }

        if (part12.transform.position == oldPosition12)
        {
            click12.enabled = true;
            drag12.enabled = false;
        }
        else
        {
            click12.enabled = false;
        }

        if (part13.transform.position == oldPosition13)
        {
            click13.enabled = true;
            drag13.enabled = false;
        }
        else
        {
            click13.enabled = false;
        }
        
        //启动拖拽组件（保证每个part到指定位置后才能被拖拽）
        if(selectedPart == part11)
        {
            drag11.enabled = true;
        }
        if(selectedPart == part12)
        {
            drag12.enabled = true;
        }
        if(selectedPart == part13)
        {
            drag13.enabled = true;
        }

        //组装顺序
        if (triggerCheck1_1.instance.shiwu.activeSelf)
        {
            trans12.SetActive(true);
        }
        if (triggerCheck1_2.instance.shiwu.activeSelf)
        {
            trans12.SetActive(false);

            if(triggerCheck1_2.instance.shiwu.transform.localPosition == Vector3.zero)
                trans13.SetActive(true);
        }
        if (triggerCheck1_3.instance.shiwu.activeSelf)
        {
            trans13.SetActive(false);
        }

    }
}
