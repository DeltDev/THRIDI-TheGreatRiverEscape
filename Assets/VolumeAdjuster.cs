using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeAdjuster : MonoBehaviour
{
    public Volume volume;
    // Start is called before the first frame update
    void Start()
    {
        volume.weight = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsNightVisionObtained()) return;
        volume.weight = 0;
        Destroy(this);
    }
}
