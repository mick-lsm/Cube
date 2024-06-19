using Cube.Utils;
using System.Collections;
using UnityEngine;

namespace Cube.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PreventPlayerHardPush : MonoBehaviour
    {
        public float reactionForceScale = 1;

        protected Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            var pRb = collision.rigidbody;
            if(collision.gameObject.GetTeam() == Entity.Team.Player && pRb != null)
            {
                var dir = ((Vector2)transform.position - collision.GetContact(0).point).normalized;

                var pV = pRb.velocity.x;

                const float forceAdjustment = 0.6f;

                rb.AddForce(reactionForceScale * Mathf.Abs(dir.x) * pV * forceAdjustment / Time.deltaTime * Vector2.left);
            }
        }
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if(collision.gameObject.GetTeam() == Entity.Team.Player && collision.rigidbody != null)
        //    {
        //        lastCoroutine = StartCoroutine(StartPreventPush(collision.rigidbody));
        //    }
        //}
        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.gameObject.GetTeam() == Entity.Team.Player && collision.rigidbody != null)
        //    {
        //        Debug.Log("Exit");
        //        StopCoroutine(lastCoroutine);
        //    }
        //}
        //private IEnumerator StartPreventPush(Rigidbody2D pusher)
        //{
        //    while (true)
        //    {
        //        var vOfP = pusher.velocity;
        //        var vOfM = rb.velocity;

        //        var difference = vOfM.x - vOfP.x;
        //        var value = vOfP.x * difference;

        //        rb.AddForce(Vector2.left * value / Time.fixedDeltaTime);
        //        //Debug.Log($"{vOfM.x} - {vOfP.x} = " + (vOfM.x - vOfP.x).ToString());

        //        yield return new WaitForFixedUpdate();
        //    }
        //}
    }
}
