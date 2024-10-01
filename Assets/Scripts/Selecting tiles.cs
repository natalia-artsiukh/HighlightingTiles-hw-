using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TielsRecol : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layer;

    private Renderer _currentTileRenderer;
    private Renderer _previousTileRenderer;
    private Color _currentTileColor;
    private Color _previousTileColor;
    private bool _currentTileExists;
    private bool _previousTileExists;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        _currentTileExists = Physics.Raycast(ray, out var raycastHit, 100f, _layer);

        if (_currentTileExists)
        {
            _currentTileRenderer = raycastHit.collider.gameObject.GetComponent<Renderer>();
            _currentTileColor = _currentTileRenderer.material.color;
        }

        bool previousNotEqualsCurrent = _currentTileRenderer != _previousTileRenderer;

        if (_previousTileExists && previousNotEqualsCurrent)
        {
            _previousTileRenderer.material.color = _previousTileColor;
        }

        if (_currentTileExists && previousNotEqualsCurrent)
        {
            _currentTileRenderer.material.color = NewTitleColor();
        }

        if (previousNotEqualsCurrent)
        {
            _previousTileRenderer = _currentTileRenderer;
            _previousTileColor = _currentTileColor;
            _previousTileExists = _currentTileExists;
        }


        // Alternative solution
        /*
        if (Physics.Raycast(ray, out var selectTile, 100f, _layer))
        {
            _currentTileRenderer = selectTile.collider.gameObject.GetComponent<Renderer>();
            _currentTileColor = _currentTileRenderer.material.color;
            
            if (_previousTileRenderer == null)
            {
                _previousTileRenderer = _currentTileRenderer;
                _previousTileColor = _currentTileColor;
                _currentTileRenderer.material.color = NewTitleColor();
            } else if (_previousTileRenderer != _currentTileRenderer)
            {
                _previousTileRenderer.material.color = _previousTileColor;
                _previousTileRenderer = _currentTileRenderer;
                _previousTileColor = _currentTileColor;
                _currentTileRenderer.material.color = NewTitleColor();
            }
        
        } else if (_previousTileRenderer != null)
        {
            _previousTileRenderer.material.color = _previousTileColor;
            _previousTileRenderer = null;
        } */ 
    }
    
    private Color NewTitleColor()
    {
        var newColor = new Color((_currentTileColor.r * 0.9f),(_currentTileColor.g * 0.9f), (_currentTileColor.b * 0.9f));
        return newColor;
    }

}
