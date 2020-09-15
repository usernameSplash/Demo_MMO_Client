using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        base.Init();
        Dir = MoveDirection.None;
        State = CreatureState.Idle;
    }

    protected override void UpdateController()
    {
        // GetDirectionInput();
        base.UpdateController();
    }

    protected override void UpdateAnimation() { }

    public override void OnDamaged()
    {
        GameObject effect = Managers.Resource.Instantiate("Effect/DeathEffect");
        effect.transform.position = transform.position;
        effect.GetComponent<Animator>().Play("DeathEffectStart");
        GameObject.Destroy(effect, 0.5f);

        Managers.Object.Remove(gameObject);
        Managers.Resource.Destroy(gameObject);
    }
}
