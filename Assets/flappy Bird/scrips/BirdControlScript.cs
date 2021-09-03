using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using UnityEngine.UI;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

namespace FlappyBird
{
    public class BirdControlScript : Agent
    {
        [Header("Ref")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PipeControler pipeControler;

        [Header("Jump Control")]
        [SerializeField] private float jumpForce = 4f;
        [SerializeField] private float VelocityCap = 4f;

        public Vector3 startPos;

        void Update() 
        {
            if (rb.position.y <= -0.808f)
            {
                AddReward(-1.0f);
                EndEpisode();
            }
            if (rb.position.y >= 4.05f)
            {
                AddReward(-1.0f);
                EndEpisode();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        public override void Initialize()
        {
            startPos = rb.position;
            rb.position = startPos;
        }
        public override void OnEpisodeBegin()
        {
            rb.position = startPos;

            startPos = rb.position;

            rb.velocity = Vector3.zero;

            pipeControler.ClearAllPipes();
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            AddReward(0.1f);

            if (Mathf.FloorToInt(actions.DiscreteActions[0]) !=1f ) 
            {
                return; 
            }

            Jump();
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discreteActionsOut = actionsOut.DiscreteActions;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            
        }
        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, VelocityCap);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            AddReward(-1.0f);
            EndEpisode();
        }
    }
}
