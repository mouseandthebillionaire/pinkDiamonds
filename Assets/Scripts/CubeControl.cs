using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    Vector3              mousePositionOffset;
    public LibPdInstance pdPatch;
    public string        pdControl_x;
    public string        pdControl_y;

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pdPatch = GameObject.Find("AudioControl").GetComponent<LibPdInstance>();
        // Apply Initial settings based on starting position
        ApplyEffect(this.transform.position.x, pdControl_x);
        ApplyEffect(this.transform.position.y, pdControl_y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        mousePositionOffset = this.gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        this.gameObject.transform.position = GetMouseWorldPosition() + mousePositionOffset;
        ApplyEffect(transform.position.x, pdControl_x);
        ApplyEffect(transform.position.y, pdControl_y);
        

    }

    private void ApplyEffect(float _num, string _effectAxis)
    {
        float effectAmt = (_num + 5f) / 10f;
        pdPatch.SendFloat(_effectAxis, effectAmt);
    }
}
