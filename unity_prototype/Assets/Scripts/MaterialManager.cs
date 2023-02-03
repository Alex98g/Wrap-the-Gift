using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private Renderer _renderWrap;
    private Renderer[] _renderRibbons;
    private bool isInited = false;

    public GameObject objWrap;
    public GameObject objRibbon;

    public Color WrapColor
    {
        get => _renderWrap.material.GetColor("_Color");
        set => _renderWrap.material.SetColor("_Color", value);
    }

    public Color RibbonColor
    {
        get => _renderRibbons[0].material.GetColor("_Color");
        set
        {
            foreach (Renderer ribbon in _renderRibbons)
            {
                ribbon.material.SetColor("_Color", value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isInited)
        {
            Init();
        }
    }

    public void Init()
    {
        objWrap = gameObject.transform.Find("BaseWrap").gameObject;
        objRibbon = gameObject.transform.Find("Ribbon").gameObject;

        _renderWrap = objWrap.GetComponent<Renderer>();
        _renderRibbons = objRibbon.GetComponentsInChildren<Renderer>();
        isInited = true;
    }
}
