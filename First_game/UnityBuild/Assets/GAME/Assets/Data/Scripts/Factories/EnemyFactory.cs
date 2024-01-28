using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [Serializable]
    class EnemyConfig
    {
        public Enemy Prefab;
        [Range(0.1f, 7f)] public float Speed = 1f;
        [Range(10f, 1000f)] public float Health = 100f;
        [Range(0f, 1000f)] public float Reward = 100f;
        [Range(0f, 100f)] public float Damage = 100f;
    }

    [SerializeField]
    private EnemyConfig _default, _fast, _boss;

    public Enemy Get(EnemyType type)
    {
        EnemyConfig config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Initialize(config.Health, config.Speed, config.Reward, config.Damage);
        return instance;
    }

    private EnemyConfig GetConfig(EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Default:
                return _default;
            case EnemyType.Fast:
                return _fast;
            case EnemyType.Boss:
                return _boss;
        }
        Debug.LogError($"No config for {type}");
        return _default;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}

