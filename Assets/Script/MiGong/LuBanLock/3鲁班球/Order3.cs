using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order3 : MonoBehaviour
{
    public GameObject trans31;
    public GameObject trans32;
    public GameObject trans33;
    public GameObject trans34;
    public GameObject trans35;
    public GameObject trans36;

    private GameObject part31;
    private GameObject part32;
    private GameObject part33;
    private GameObject part34;
    private GameObject part35;
    private GameObject part36;

    //拖拽组件
    private Drag3 drag31;
    private Drag3 drag32;
    private Drag3 drag33;
    private Drag3 drag34;
    private Drag3 drag35;
    private Drag3 drag36;

    //点击组件
    private Click click31;
    private Click click32;
    private Click click33;
    private Click click34;
    private Click click35;
    private Click click36;

    //part原位置
    private Vector3 oldPosition31;
    private Vector3 oldPosition32;
    private Vector3 oldPosition33;
    private Vector3 oldPosition34;
    private Vector3 oldPosition35;
    private Vector3 oldPosition36;

    private GameObject rotatePanel;
    private GameObject selectedPart;

    private bool isRotated=false;

    private void Awake()
    {
        part31 = GameObject.Find("parent_31");
        part32 = GameObject.Find("parent_32");
        part33 = GameObject.Find("parent_33");
        part34 = GameObject.Find("parent_34");
        part35 = GameObject.Find("parent_35");
        part36 = GameObject.Find("parent_36");

        drag31 = part31.GetComponent<Drag3>();
        drag32 = part32.GetComponent<Drag3>();
        drag33 = part33.GetComponent<Drag3>();
        drag34 = part34.GetComponent<Drag3>();
        drag35 = part35.GetComponent<Drag3>();
        drag36 = part36.GetComponent<Drag3>();

        click31 = part31.GetComponent<Click>();
        click32 = part32.GetComponent<Click>();
        click33 = part33.GetComponent<Click>();
        click34 = part34.GetComponent<Click>();
        click35 = part35.GetComponent<Click>();
        click36 = part36.GetComponent<Click>();

        rotatePanel = GameObject.Find("RotateControl");
    }

    void Start()
    {
        trans31.SetActive(true);
        trans32.SetActive(false);
        trans33.SetActive(false);
        trans34.SetActive(false);
        trans35.SetActive(false);
        trans36.SetActive(false);

        drag31.enabled = false;
        drag32.enabled = false;
        drag33.enabled = false;
        drag34.enabled = false;
        drag35.enabled = false;
        drag36.enabled = false;

        click31.enabled = false;
        click32.enabled = false;
        click33.enabled = false;
        click34.enabled = false;
        click35.enabled = false;
        click36.enabled = false;

        oldPosition31 = part31.transform.position;
        oldPosition32 = part32.transform.position;
        oldPosition33 = part33.transform.position;
        oldPosition34 = part34.transform.position;
        oldPosition35 = part35.transform.position;
        oldPosition36 = part36.transform.position;
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
        if (part31.transform.position == oldPosition31)
        {
            click31.enabled = true;
            drag31.enabled = false;
        }
        else
        {
            click31.enabled = false;
        }

        if (part32.transform.position == oldPosition32)
        {
            click32.enabled = true;
            drag32.enabled = false;
        }
        else
        {
            click32.enabled = false;
        }

        if (part33.transform.position == oldPosition33)
        {
            click33.enabled = true;
            drag33.enabled = false;
        }
        else
        {
            click33.enabled = false;
        }

        if (part34.transform.position == oldPosition34)
        {
            click34.enabled = true;
            drag34.enabled = false;
        }
        else
        {
            click34.enabled = false;
        }

        if (part35.transform.position == oldPosition35)
        {
            click35.enabled = true;
            drag35.enabled = false;
        }
        else
        {
            click35.enabled = false;
        }

        if (part36.transform.position == oldPosition36)
        {
            click36.enabled = true;
            drag36.enabled = false;
        }
        else
        {
            click36.enabled = false;
        }

        //启动拖拽组件（保证每个part到指定位置后才能被拖拽）以及碰撞检测
        if (selectedPart == part31) drag31.enabled = true;
        if (selectedPart == part32) drag32.enabled = true;
        if (selectedPart == part33) drag33.enabled = true;
        if (selectedPart == part34) drag34.enabled = true;
        if (selectedPart == part35) drag35.enabled = true;
        if (selectedPart == part36) drag36.enabled = true;

        //组装顺序
        if (triggerCheck3_1.instance.shiwu.activeSelf)
        {
            trans32.SetActive(true);
        }

        if (triggerCheck3_2.instance.shiwu.activeSelf)
        {
            trans32.SetActive(false);

            if (triggerCheck3_2.instance.shiwu.transform.localPosition == Vector3.zero)
                trans33.SetActive(true);
        }

        if (triggerCheck3_3.instance.shiwu.activeSelf)
        {
            trans33.SetActive(false);

            if (triggerCheck3_3.instance.shiwu.transform.localPosition == Vector3.zero)
                trans34.SetActive(true);
        }

        if (triggerCheck3_4.instance.shiwu.activeSelf)
        {
            trans34.SetActive(false);

            if (triggerCheck3_2.instance.shiwu.transform.localPosition == new Vector3(36f, 0f, -18f) && !isRotated)
            {
                trans35.SetActive(true);
                isRotated = true;
            }
        }

        if (triggerCheck3_5.instance.shiwu.activeSelf)
        {
            trans35.SetActive(false);

            if (triggerCheck3_5.instance.shiwu.transform.localPosition == Vector3.zero)
                trans36.SetActive(true);
        }

        if (triggerCheck3_6.instance.shiwu.activeSelf)
        {
            trans36.SetActive(false);
        }

    }
}
