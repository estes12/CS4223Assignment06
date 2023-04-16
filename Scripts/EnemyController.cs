using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public Camera eyes;
    public NavMeshAgent enemy;
    public ThirdPersonCharacter controllerScript;
    public Transform targetTransform;

    void Start()
    {
        
        enemy.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            enemy.destination = targetTransform.position;
            controllerScript.Move(enemy.desiredVelocity, false, false);
        }
        else
        {
            controllerScript.Move(Vector3.zero, false, false);
        }  
    }

}
