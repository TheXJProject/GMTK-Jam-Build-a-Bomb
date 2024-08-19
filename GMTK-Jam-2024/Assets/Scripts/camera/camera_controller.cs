using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    List<float> bombDiameters = new List<float>();
    int currentLevelFocused;

    private void OnEnable()
    {
        layer_controller.onNewLayerCreated += RecordBombDiameter;
    }

    private void OnDisable()
    {
        layer_controller.onNewLayerCreated -= RecordBombDiameter;
    }

    private void Update()
    {
        gameObject.GetComponent<Camera>().orthographicSize = bombDiameters[currentLevelFocused] / 1.44f;
        Vector3 offset = GetBombScreenOffset();
        transform.position = offset;
    }

    void RecordBombDiameter(float diameter)
    {
        bombDiameters.Add(diameter);
    }

    Vector3 GetBombScreenOffset()
    {
        Vector3 offset = new Vector2();
        float ratio = (float)Screen.width / (float)Screen.height;

        offset.y = -(10f/72f * bombDiameters[currentLevelFocused]);
        offset.x = (bombDiameters[currentLevelFocused] * ratio * 17.5f) / 72f;
        offset.z = -10;

        return offset;
    }
}
