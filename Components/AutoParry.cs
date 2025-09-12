using SilksongUtils.Patches;
using UnityEngine;

namespace SilksongUtils.Components
{
    internal class AutoParry : MonoBehaviour
    {
        private HeroController hc = null;
        private BoxCollider2D hcCollider = null;
        private CircleCollider2D collider = null;

        private void Awake()
        {
            hc = HeroController.instance;
            hcCollider = hc.GetComponent<BoxCollider2D>();

            collider = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            collider.radius = hcCollider.size.x + 5f;
        }

        private void FixedUpdate()
        {
            DebugDrawColliderRuntime.AddOrUpdate(gameObject, DebugDrawColliderRuntime.ColorType.Enemy, false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Plugin.configAutoParry.Value) return;

            //Plugin.Logger.LogInfo("Collider Layer: " + LayerMask.LayerToName(other.gameObject.layer));
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy Attack")) return;

            if (!hc.CanAttack()) return;

            if (other.transform.position.x > hc.transform.position.x)
            {
                hc.FaceRight();
            }
            else
            {
                hc.FaceLeft();
            }

            ReversePatch.HeroController_DoAttack(hc);
        }
    }
}
