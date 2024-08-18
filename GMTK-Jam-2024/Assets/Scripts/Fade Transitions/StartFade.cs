using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    [SerializeField] Fade fade;
    [SerializeField] GameObject black;

    void Start()
    {
        black.SetActive(true);
        fade.HideUI();
    }
}
