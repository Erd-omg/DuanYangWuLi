using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture PrevRT;
    public RenderTexture CurrentRT;
    public RenderTexture TempRT;
    public Shader DrawShader;
    public Shader RippleShader;

    private Material RippleMat;
    private Material DrawMat;
    [Range(0, 1.0f)]
    public float DrawRadius = 0.2f;
    public int TextureSize = 512;

    // 定义淡蓝色和更浅的蓝色
    private Color lightBlue = new Color(0.2f, 0.4f, 0.6f, 1f);
    private Color lighterBlue = new Color(0.4f, 0.6f, 0.8f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        CurrentRT = CreateRT();
        PrevRT = CreateRT();
        TempRT = CreateRT();

        DrawMat = new Material(DrawShader);
        RippleMat = new Material(RippleShader);

        // 设置 DrawShader 中的涟漪颜色
        DrawMat.SetColor("_RippleColor", lighterBlue);

        // 创建一个淡蓝色的纹理
        Texture2D blueTexture = new Texture2D(1, 1);
        blueTexture.SetPixel(0, 0, lightBlue);
        blueTexture.Apply();

        // 将淡蓝色纹理绘制到 RenderTexture 上
        Graphics.Blit(blueTexture, CurrentRT);
        Graphics.Blit(blueTexture, PrevRT);

        // 确保材质应用到渲染器
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = CurrentRT;
        }
    }

    public RenderTexture CreateRT()
    {
        // 使用支持颜色的纹理格式
        RenderTexture rt = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.ARGB32);
        rt.Create();
        return rt;
    }

    private void DrawAt(float x, float y, float radius)
    {
        // 原来的贴图
        DrawMat.SetTexture("_SourceTex", CurrentRT);
        // 绘制的位置和大小
        DrawMat.SetVector("_Pos", new Vector4(x, y, radius));
        // 提交
        Graphics.Blit(null, TempRT, DrawMat);

        // 进行交换
        RenderTexture rt = TempRT;
        TempRT = CurrentRT;
        CurrentRT = rt;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                DrawAt(hit.textureCoord.x, hit.textureCoord.y, DrawRadius);
            }
        }

        RippleMat.SetTexture("_PrevRT", PrevRT);
        RippleMat.SetTexture("_CurrentRT", CurrentRT);
        Graphics.Blit(null, TempRT, RippleMat);

        Graphics.Blit(TempRT, PrevRT);

        RenderTexture rt = PrevRT;
        PrevRT = CurrentRT;
        CurrentRT = rt;
    }
}