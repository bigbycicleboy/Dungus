using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    public enum EventType
    {
        None,
        TriggerEnter,
        TriggerExit,
        CollisionEnter,
        CollisionExit
    }

    public EventType eventType = EventType.None;
    public bool useTagFilter = false;
    public string tagFilter = "Untagged";
    public bool useLayerFilter = false;
    public LayerMask layerFilter;
    public UnityEvent EventTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (eventType != EventType.TriggerEnter) return;
        if (useTagFilter && other.CompareTag(tagFilter) == false) return;
        if (useLayerFilter && (layerFilter.value & (1 << other.gameObject.layer)) == 0) return;

        EventTrigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (eventType != EventType.TriggerExit) return;
        if (useTagFilter && other.CompareTag(tagFilter) == false) return;
        if (useLayerFilter && (layerFilter.value & (1 << other.gameObject.layer)) == 0) return;

        EventTrigger.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (eventType != EventType.CollisionEnter) return;
        if (useTagFilter && collision.gameObject.CompareTag(tagFilter) == false) return;
        if (useLayerFilter && (layerFilter.value & (1 << collision.gameObject.layer)) == 0) return;

        EventTrigger.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (eventType != EventType.CollisionExit) return;
        if (useTagFilter && collision.gameObject.CompareTag(tagFilter) == false) return;
        if (useLayerFilter && (layerFilter.value & (1 << collision.gameObject.layer)) == 0) return;

        EventTrigger.Invoke();
    }
}