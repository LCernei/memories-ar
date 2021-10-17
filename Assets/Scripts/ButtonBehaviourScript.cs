using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ButtonBehaviourScript : MonoBehaviour
{
    private List<GameObject> planes;
    // Start is called before the first frame update
    void Start()
    {
        planes ??= GameObject.FindGameObjectsWithTag("plane")
            .OrderBy(go => go.name).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                if (raycastHit.collider.gameObject != this.gameObject)
                    return;
                NativeGallery.GetImageFromGallery(ImagePicked, "Pick an memory");
            }
        }
    }

    private void ImagePicked(string path)
    {
        var imageBytes = File.ReadAllBytes(path);
        var returningTex = new Texture2D(2, 2);
        returningTex.LoadImage(imageBytes);
        this.gameObject.GetComponent<Renderer>().material.mainTexture = returningTex;
        Debug.Log($"Aici: {path}");
    }
}
