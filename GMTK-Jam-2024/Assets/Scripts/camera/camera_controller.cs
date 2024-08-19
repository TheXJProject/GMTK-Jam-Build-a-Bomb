using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public float zoomSpeed = 1.0f;
    
    List<float> bombDiameters = new List<float>();
    int currentLevelFocused;

    bool firstCameraAssign = true;

    float targetScale;
    float currentScale;

    Vector3 targetPos;
    Vector3 currentPos;

    private void OnEnable()
    {
        layer_controller.onNewLayerCreated += RecordBombDiameter;
        button_for_layers.onLayerSelected += ChangeLayerFocus;
    }

    private void OnDisable()
    {
        layer_controller.onNewLayerCreated -= RecordBombDiameter;
        button_for_layers.onLayerSelected -= ChangeLayerFocus;
    }

    private void Update()
    {
        currentScale = gameObject.GetComponent<Camera>().orthographicSize;
        targetScale = bombDiameters[currentLevelFocused] / 1.44f;
        gameObject.GetComponent<Camera>().orthographicSize = currentScale + ((targetScale - currentScale) * zoomSpeed * Time.deltaTime);

        currentPos = transform.position;
        targetPos = GetBombScreenOffset();
        transform.position = currentPos + ((targetPos - currentPos) * zoomSpeed * Time.deltaTime);

    }

    void ChangeLayerFocus(int newLayer)
    {
        currentLevelFocused = newLayer;
    }

    void RecordBombDiameter(float diameter)
    {
        bombDiameters.Add(diameter);
        ChangeLayerFocus(bombDiameters.Count - 1);
        
        if (firstCameraAssign)
        {
            firstCameraAssign = false;
            transform.position = GetBombScreenOffset();
        }
    }

    Vector3 GetBombScreenOffset()
    {
        Vector3 offset = new Vector3();
        float ratio = (float)Screen.width / (float)Screen.height;

        offset.y = -(10f/72f * bombDiameters[currentLevelFocused]);
        offset.x = (bombDiameters[currentLevelFocused] * ratio * 17.5f) / 72f;
        offset.z = -10;

        return offset;
    }
}
