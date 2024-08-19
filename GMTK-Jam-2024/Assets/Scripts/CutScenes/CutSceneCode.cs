using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneCode : MonoBehaviour
{
    [SerializeField] List<GameObject> slides;

    [SerializeField] Fade fade;

    private bool changing = true;

    private void OnEnable()
    {
        Fade.onZeroAlpha += slideNotInView;
        Fade.onOneAlpha += slideInView;
    }
    private void OnDisable()
    {
        Fade.onZeroAlpha -= slideNotInView;
        Fade.onOneAlpha -= slideInView;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic("CutSceneMusic");

        for (int i = 0; i < slides.Count; i++)
        {
            if (i == 0)
            {
                slides[i].SetActive(true);
            }
            else
            {
                slides[i].SetActive(false);
            }
        }
        
        fade.ShowUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!changing)
            {
                changing = true;
                fade.HideUI();
            }
        }
    }

    private void slideInView()
    {
        changing = false;
    }

    private void slideNotInView()
    {
        for (int i = 0; i < slides.Count; i++)
        {
            if (slides[i].activeSelf)
            {
                if (slides[slides.Count - 1].activeSelf)
                {
                    SceneManager.LoadScene("MainGameLoop");
                }
                else
                {
                    slides[i].SetActive(false);
                    slides[i + 1].SetActive(true);
                    break;
                }
            }
        }
        fade.ShowUI();
    }
}
