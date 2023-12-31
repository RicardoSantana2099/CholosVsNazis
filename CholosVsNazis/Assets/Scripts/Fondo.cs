using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        
       
    }

    private void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;

     
    }
}
