using _Scripts.Patterns;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class PooledMoneyStack : PooledObject
{
    private RectTransform targetScreenTransform;

    private static Camera cam;
    
    private Rigidbody rb;
    private Vector3 defaultScale;
    
    private bool fly;
    
    public void Init(RectTransform targetScreenTransform)
    {
        defaultScale = transform.localScale;
        
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        
        this.targetScreenTransform = targetScreenTransform;
        fly = true;
        rb.angularVelocity = Random.insideUnitSphere * 10f * Random.value;
    }

    private void FixedUpdate()
    {
        if(!fly) return;

        Vector3 targetPos = targetScreenTransform.position;
        targetPos.z = 1f;
        targetPos = cam.ScreenToWorldPoint(targetPos);
        
        rb.AddForce((targetPos - transform.position) * Random.value, ForceMode.Acceleration);

        if (fly && Vector3.Distance(targetPos, transform.position) <= 0.2f)
        {
            fly = false;
            transform.DOMove(targetPos, 0.45f).SetEase(Ease.InCubic);
            transform.DOScale(defaultScale * 0.5f, 0.5f).OnComplete(ResetObject);
        }
    }

    public override void ResetObject()
    {
        transform.localScale = defaultScale;
        rb.velocity = Vector3.zero;
        
        base.ResetObject();
    }
}
