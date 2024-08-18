using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneCode : MonoBehaviour
{
    [SerializeField] List<GameObject> slides;

    void Start()
    {
        slides[0].SetActive(true);
        slides[1].SetActive(false);
        slides[2].SetActive(false);
        slides[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
