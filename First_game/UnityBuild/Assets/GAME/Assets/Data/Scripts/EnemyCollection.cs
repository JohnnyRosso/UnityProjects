using System;
using System.Collections.Generic;

[Serializable]
public class EnemyCollection
{
    private List<Enemy> _enemies = new List<Enemy>();
    public Action<float> EnemyDead;
    public Action<float> EnemyAchivedBase;
    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Dead += OnEnemyDead;
        enemy.AchievedBase += OnBaseAchived;
    }

    public void Clear()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Dead -= OnEnemyDead;
            _enemies[i].AchievedBase -= OnBaseAchived;
            _enemies[i].Recycle();
        }
        _enemies.Clear();
    }

    public void GameUpdate()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].GameUpdate())
            {
                _enemies[i].Dead -= OnEnemyDead;
                _enemies[i].AchievedBase -= OnBaseAchived;
                int lastIndex = _enemies.Count - 1;
                _enemies[i] = _enemies[lastIndex];
                _enemies.RemoveAt(lastIndex);
                i -= 1;
            }
        }
    }

    private void OnEnemyDead(float reward)
    {
        EnemyDead?.Invoke(reward);
    }
    private void OnBaseAchived(float damage)
    {
        EnemyAchivedBase?.Invoke(damage);
    }
}
