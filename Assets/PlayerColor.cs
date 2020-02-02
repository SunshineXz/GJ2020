using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer rend;
    private static float variation = 0f;
    void Start()
    {
        rend.materials[0].SetColor("_BaseColor",Random.ColorHSV(variation, variation+0.5f, 1f, 1f, 0.5f, 1f));
        variation = 0.5f;
    }
}
