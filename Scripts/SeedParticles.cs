using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SeedParticles : MonoBehaviour
{
    public static Action<Vector3[]> OnSeedsCollided;
    public static Action OnSeedsCollidedChicken;
    public static Action OnSeedsCollidedCow;
    public static Action OnSeedsCollidedPig;
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Chicken>())
            OnSeedsCollidedChicken?.Invoke();
        if (other.GetComponent<Cow>())
            OnSeedsCollidedCow?.Invoke();
        if (other.GetComponent<Pig>())
            OnSeedsCollidedPig?.Invoke();

        ParticleSystem ps = GetComponent<ParticleSystem>();


        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionAmount = ps.GetCollisionEvents(other, collisionEvents);
        Vector3[] collisionPositions = new Vector3[collisionAmount];

        for (int i = 0; i < collisionAmount; i++)
        {
            collisionPositions[i] = collisionEvents[i].intersection;
        }
        OnSeedsCollided?.Invoke(collisionPositions);

    }
}
