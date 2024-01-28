using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class TowerFactory : GameObjectFactory
{
    [SerializeField] private Tower _default;
    [SerializeField] private Tower _freeze;

    public Tower GetDefault()
    {
        Tower instance = CreateGameObjectInstance(_default);
        instance.OriginFactory = this;
        return instance;
    }
    public Tower GetFreeze()
    {
        Tower instance = CreateGameObjectInstance(_freeze);
        instance.OriginFactory = this;
        return instance;
    }

    public float GetCost(TowerType type)
    {
        switch(type)
        {
            case TowerType.Default:
                return _default.Cost;
            case TowerType.Freeze:
                return _freeze.Cost;
        }
        return 0f;
    }

    public void Reclaim(Tower tower)
    {
        Destroy(tower.gameObject);
    }
}

