using UnityEngine;

public static class Rigidbody2DExt
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0f, ForceMode2D mode = ForceMode2D.Impulse)
    {
        var explosionDir = body.position - explosionPosition;
        var explosionDist = explosionDir.magnitude;

        if(upwardsModifier == 0)
        {
            explosionDir /= explosionDist;
        } else
        {
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        body.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDist)) * explosionDir, mode);
    }
}
