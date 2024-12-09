using Assets.Scripts.Abstract;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpellController : MonoBehaviour
{
    [Header("Spell Configurations")]
    [SerializeField] private SpellInfos fireballSpellInfo;
    [SerializeField] private SpellInfos barrageSpellInfo;

    [Header("UI References")]
    [SerializeField] private Image fireballCoolDownImg;
    [SerializeField] private Image barrageCoolDownImg;

    private float _fireballTimer = 0f;
    private float _barrageTimer = 0f;
    private List<GameObject> _enemies;

    private void Awake()
    {
        SetCooldown();
    }

    private void Update()
    {
        _enemies = GameSignals.Instance.onReturnEnemies?.Invoke();

        _fireballTimer -= Time.deltaTime;
        _barrageTimer -= Time.deltaTime;

        _fireballTimer = Mathf.Clamp(_fireballTimer, 0f, fireballSpellInfo.coolDown);
        _barrageTimer = Mathf.Clamp(_barrageTimer, 0f, barrageSpellInfo.coolDown);

        SetCooldown();
    }

    public void FireballSpell()
    {
        if (_fireballTimer <= 0f && _enemies.Count > 0)
        {
            CastFireball();
            _fireballTimer = fireballSpellInfo.coolDown;
        }
    }

    public void BarrageSpell()
    {
        if (_barrageTimer <= 0f && _enemies.Count > 0)
        {
            CastBarrage();
            _barrageTimer = barrageSpellInfo.coolDown;
        }
    }

    private void CastFireball()
    {
        GameObject enemy = _enemies[Random.Range(0, _enemies.Count)];
        if (enemy != null)
        {
            GameObject fireball = PoolSignals.Instance.onGetObjFromPool?.Invoke("Fireball");
            fireball.transform.position = transform.position + new Vector3(0, 10, 0);
            fireball.GetComponent<SpellsAbs>().Initialize(enemy.transform.position, fireballSpellInfo.speed, fireballSpellInfo.damage);
        }
    }

    private void CastBarrage()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                GameObject icicle = PoolSignals.Instance.onGetObjFromPool?.Invoke("Icicle");
                icicle.transform.position = transform.position + new Vector3(0, 10, 0);

                Vector3 direction = enemy.transform.position - icicle.transform.position;
                icicle.transform.rotation = Quaternion.LookRotation(direction);

                icicle.GetComponent<SpellsAbs>().Initialize(enemy.transform.position, barrageSpellInfo.speed, barrageSpellInfo.damage);
            }
        }
    }

    private void SetCooldown()
    {
        fireballCoolDownImg.fillAmount = _fireballTimer / fireballSpellInfo.coolDown;
        barrageCoolDownImg.fillAmount = _barrageTimer / barrageSpellInfo.coolDown;
    }
}
