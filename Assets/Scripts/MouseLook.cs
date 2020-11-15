using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //selection part//

    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    //selection part//

    [SerializeField] private float _mouseSensetive = 100;
    [SerializeField] private Transform _body;
    private float _xRoatation = 0f;
    private float _mouseX;
    private float _mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //selection part //

        if (_selection != null)
        {
            var selectionRender = _selection.GetComponent<Renderer>();
            selectionRender.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
           
        }
        
        //selection part //

         _mouseX = Input.GetAxis("Mouse X") * _mouseSensetive * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensetive * Time.deltaTime;

        _xRoatation -= _mouseY;
        _xRoatation = Mathf.Clamp(_xRoatation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRoatation, 0f, 0f);
        _body.Rotate(Vector3.up * _mouseX);
    }
}
