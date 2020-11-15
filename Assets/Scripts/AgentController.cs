using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class AgentController : Agent
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int damage;
    [SerializeField] private bool debug;
    [SerializeField] private float firerate;
    [SerializeField] private float rewardDeadline;

    private Timer shotTimer;
    private Timer rewardTimer;
    private Vector3 startPos;

    #region ML
    public override void Initialize() // Typ start
    {
        startPos = transform.position;
        shotTimer = new Timer(firerate);
        rewardTimer = new Timer(rewardDeadline);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            Shoot();
        }
    }

    public override void OnEpisodeBegin()
    {
        Reset();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;

        if (Input.GetKey(KeyCode.Space) && debug && shotTimer.Done)
        {
            print("meme");
            actionsOut[0] = 1;
        }
    }
    #endregion

    private void Update()
    {
        shotTimer.DecrementTimer(Time.deltaTime);
        rewardTimer.DecrementTimer(Time.deltaTime);

        if (rewardTimer.Done)
        {
            AddReward(-0.1f);
            print(GetCumulativeReward());
            EndEpisode();
        }
    }

    private void Shoot()
    {
        int layermask = 1 << LayerMask.NameToLayer("Enemy");

        Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        if (Physics.Raycast(shootPoint.position, transform.forward, out RaycastHit hit, 1000f, layermask))
        {
            hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            AddReward(0.1f);
            print(GetCumulativeReward());
        }

        Debug.DrawRay(shootPoint.position, transform.forward, Color.blue, 0.2f);

        shotTimer.Reset();
    }

    private void Reset()
    {
        shotTimer.Reset();
        rewardTimer.Reset();
        transform.position = startPos;

        print("EPISODE BEGIN");
    }
}
