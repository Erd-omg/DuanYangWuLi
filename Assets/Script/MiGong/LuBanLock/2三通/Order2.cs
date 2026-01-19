using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order2 : MonoBehaviour
{
    public GameObject trans21;
    public GameObject trans22;
    public GameObject trans23;

    private GameObject part21;
    private GameObject part22;
    private GameObject part23;

    //拖拽组件
    private Drag2 drag21;
    private Drag2 drag22;
    private Drag2 drag23;

    //点击组件
    private Click click21;
    private Click click22;
    private Click click23;

    private Vector3 oldPosition21;
    private Vector3 oldPosition22;
    private Vector3 oldPosition23;

    private GameObject rotatePanel;
    private GameObject selectedPart;

    private void Awake()
    {
        part21 = GameObject.Find("parent_21");
        part22 = GameObject.Find("parent_22");
        part23 = GameObject.Find("parent_23");

        drag21 = part21.GetComponent<Drag2>();
        drag22 = part22.GetComponent<Drag2>();
        drag23 = part23.GetComponent<Drag2>();

        click21 = part21.GetComponent<Click>();
        click22 = part22.GetComponent<Click>();
        click23 = part23.GetComponent<Click>();

        rotatePanel = GameObject.Find("RotateControl");
    }

    void Start()
    {
        trans21.SetActive(true);
        trans22.SetActive(false);
        trans23.SetActive(false);

        drag21.enabled = false;
        drag22.enabled = false;
        drag23.enabled = false;

        click21.enabled = false;
        click22.enabled = false;
        click23.enabled = false;

        oldPosition21 = part21.transform.position;
        oldPosition22 = part22.transform.position;
        oldPosition23 = part23.transform.position;
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
        if (part21.transform.position == oldPosition21)
        {
            click21.enabled = true;
            drag21.enabled = false;
        }
        else
        {
            click21.enabled = false;
        }

        if (part22.transform.position == oldPosition22)
        {
            click22.enabled = true;
            drag22.enabled = false;
        }
        else
        {
            click22.enabled = false;
        }

        if (part23.transform.position == oldPosition23)
        {
            click23.enabled = true;
            drag23.enabled = false;
        }
        else
        {
            click23.enabled = false;
        }

        //启动拖拽组件（保证每个part到指定位置后才能被拖拽）
        if (selectedPart == part21)
        {
            drag21.enabled = true;
        }
        if (selectedPart == part22)
        {
            drag22.enabled = true;
        }
        if (selectedPart == part23)
        {
            drag23.enabled = true;
        }

        //组装顺序
        if (triggerCheck2_1.instance.shiwu.activeSelf)
        {
            trans22.SetActive(true);
        }
        if (triggerCheck2_2.instance.shiwu.activeSelf)
        {
            trans22.SetActive(false);

            if (triggerCheck2_2.instance.shiwu.transform.localPosition == Vector3.zero)
                trans23.SetActive(true);
        }
        if (triggerCheck2_3.instance.shiwu.activeSelf)
        {
            trans23.SetActive(false);
        }

    }
}
