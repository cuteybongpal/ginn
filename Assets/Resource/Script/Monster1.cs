using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class Monster1 : MonsterController
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Damaged(int damage)
    {
        StartCoroutine(SpeedBoost());
        base.Damaged(damage);
    }
    IEnumerator SpeedBoost()
    {
        Speed += 3;
        yield return new WaitForSeconds(3);
        Speed -= 3;

    }
}
