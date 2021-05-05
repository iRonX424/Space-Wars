using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroungScrollSpeed = 0.1f;
    Vector2 offset;
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        offset = new Vector2(0f,backgroungScrollSpeed);
    }

    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;        
    }
}
