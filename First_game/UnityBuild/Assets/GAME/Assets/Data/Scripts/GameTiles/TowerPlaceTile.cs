using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TowerPlaceTile : GameTile
{
    [SerializeField] private GameObject _model;
    private Vector3 _towerOffset => new Vector3(0f, _model.transform.localScale.y, 0f);
    private Tower _tower;
    public bool isTowerPlaced => _tower != null ? true : false;

    private void Awake()
    {

    }

    public void PlaceTower(Tower tower)
    {
        _tower = tower;
        _tower.transform.localPosition = transform.localPosition + _towerOffset;
    }
}
