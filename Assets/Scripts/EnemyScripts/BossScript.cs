using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : GeneralEnemyScript, IDamageable
{
    [SerializeField] float _vulnTimer = 6;
    private float countdown = 0;
    private bool isVuln = false;
    private BossFSM fsm;

    private void Start()
    {
        fsm = (BossFSM)_enemyFSM;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && isVuln)
        {
            //Explode();
            Invulnrable();
        }
    }

    public new void TakeDamage(int damage, Vector3 dir, bool isExplosiveDamage)
    {
        Debug.Log("Boss is hit!");
        if (isVuln)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                Death(isExplosiveDamage);
            }
            else
            {
                Invulnrable();
            }
        }

        else if (isExplosiveDamage)
        {
            Vulnrable();
        }
    }

    private void Vulnrable()
    {
        isVuln = true;
        countdown = _vulnTimer;
        _animator.SetBool("isStunned", true);
        fsm.SwitchState(BossEnemyStates.Stunn);
    }

    private void Invulnrable()
    {
        isVuln = false;
        _animator.SetBool("isStunned", false);
        fsm.SwitchState(BossEnemyStates.Shoot);
    }

    public void ActivateBoss()
    {
        fsm.SwitchState(BossEnemyStates.Shoot);
    }
}
