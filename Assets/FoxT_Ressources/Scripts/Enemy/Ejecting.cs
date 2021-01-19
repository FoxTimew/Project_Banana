using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejecting : MonoBehaviour
{
    public AnimationCurve pousseForce;
    public Vector2 direction;
    public float timeElapsed, bonusForce, forcePoussee, mauditBonusPoussee, damage;
    Rigidbody2D rb;
    BoxCollider2D selfCol;
    public bool pousseeCharmEnable;
    [SerializeField]
    LayerMask enemyLayer, wallLayer;

	/*private void OnEnable()
	{
        RaycastHit2D wallDetector = Physics2D.Raycast(this.transform.position, direction, (AreaUnderCurve(pousseForce, 1, 1) / 10000) * 3, wallLayer);
        if (wallDetector.collider != null && wallDetector.collider.gameObject.tag == "Wall")
        {
            Debug.Log("NOCIE");
            wallDetector.collider.GetComponentInChildren<WallRebound>().obj.Add(this.selfCol);
            wallDetector.collider.GetComponentInChildren<WallRebound>().UpdateAnimation();
        }
        Debug.Log((AreaUnderCurve(pousseForce, 1, 1) /10000) * 3);
	}*/

	private void Start()
	{
        rb = this.GetComponent<Rigidbody2D>();
        selfCol = this.GetComponent<BoxCollider2D>();
    }
	void Update()
    {
        if (!this.GetComponent<EnemySys>().dying)
        {
            if (pousseForce.Evaluate(timeElapsed) <= 0)
            {
                this.GetComponent<Ejecting>().enabled = false;
                return;
            }
            else Poussee();
        }
    }

    void Poussee()
    {
        rb.velocity = Vector2.zero;
        this.GetComponent<EnemySys>().isEjected = true;
        rb.AddForce(direction * (pousseForce.Evaluate(timeElapsed) + forcePoussee + bonusForce + (pousseForce.Evaluate(timeElapsed) * mauditBonusPoussee)) * Time.deltaTime);
        timeElapsed += Time.deltaTime;
        if (timeElapsed > pousseForce.keys[pousseForce.keys.Length - 1].time)
        {
            StartCoroutine(this.GetComponentInChildren<Unit>().UpdtaePath());
            this.GetComponent<EnemySys>().isEjected = false;
            this.GetComponent<Ejecting>().enabled = false;
        }

        if (!pousseeCharmEnable) return;
        Collider2D[] enemyCharm = Physics2D.OverlapBoxAll(this.transform.position,selfCol.size, 0f, enemyLayer);
        if (enemyCharm == null) return;
        foreach (Collider2D obj in enemyCharm)
        {
           if (obj.gameObject != this.gameObject) obj.gameObject.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(damage), 0f);
        }
    }

    public float AreaUnderCurve(AnimationCurve curve, float w, float h)
    {
        float areaUnderCurve = 0f;
        var keys = curve.keys;

        for (int i = 0; i < keys.Length - 1; i++)
        {
            // Calculate the 4 cubic Bezier control points from Unity AnimationCurve (a hermite cubic spline) 
            Keyframe K1 = keys[i];
            Keyframe K2 = keys[i + 1];
            Vector2 A = new Vector2(K1.time * w, K1.value * h);
            Vector2 D = new Vector2(K2.time * w, K2.value * h);
            float e = (D.x - A.x) / 3.0f;
            float f = h / w;
            Vector2 B = A + new Vector2(e, e * f * K1.outTangent);
            Vector2 C = D + new Vector2(-e, -e * f * K2.inTangent);

            /*
             * The cubic Bezier curve function looks like this:
             * 
             * f(x) = A(1 - x)^3 + 3B(1 - x)^2 x + 3C(1 - x) x^2 + Dx^3
             * 
             * Where A, B, C and D are the control points and, 
             * for the purpose of evaluating an instance of the Bezier curve, 
             * are constants. 
             * 
             * Multiplying everything out and collecting terms yields the expanded polynomial form:
             * f(x) = (-A + 3B -3C + D)x^3 + (3A - 6B + 3C)x^2 + (-3A + 3B)x + A
             * 
             * If we say:
             * a = -A + 3B - 3C + D
             * b = 3A - 6B + 3C
             * c = -3A + 3B
             * d = A
             * 
             * Then we have the expanded polynomal:
             * f(x) = ax^3 + bx^2 + cx + d
             * 
             * Whos indefinite integral is:
             * a/4 x^4 + b/3 x^3 + c/2 x^2 + dx + E
             * Where E is a new constant introduced by integration.
             * 
             * The indefinite integral of the quadratic Bezier curve is:
             * (-A + 3B - 3C + D)/4 x^4 + (A - 2B + C) x^3 + 3/2 (B - A) x^2 + Ax + E
             */

            float a, b, c, d;
            a = -A.y + 3.0f * B.y - 3.0f * C.y + D.y;
            b = 3.0f * A.y - 6.0f * B.y + 3.0f * C.y;
            c = -3.0f * A.y + 3.0f * B.y;
            d = A.y;

            /* 
             * a, b, c, d, now contain the y component from the Bezier control points.
             * In other words - the AnimationCurve Keyframe value * h data!
             * 
             * What about the x component for the Bezier control points - the AnimationCurve
             * time data?  We will need to evaluate the x component when time = 1.
             * 
             * x^4, x^3, X^2, X all equal 1, so we can conveniently drop this coeffiecient.
             * 
             * Lastly, for each segment on the AnimationCurve we get the time difference of the 
             * Keyframes and multiply by w.
             * 
             * Iterate through the segments and add up all the areas for 
             * the total area under the AnimationCurve!
             */

            float t = (K2.time - K1.time) * w;

            float area = ((a / 4.0f) + (b / 3.0f) + (c / 2.0f) + d) * t;

            areaUnderCurve += area;
        }
        return areaUnderCurve;
    }
}
