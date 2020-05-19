using Scarred.Systems;
using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class Hero : MonoBehaviour, IConvertGameObjectToEntity
{
    Animator upperAnimator;
    Animator lowerAnimator;

    private Entity entity;
    private Entity upperAnimation;
    private Entity lowerAnimation;
    private EntityManager entityManager;

    private const string UpperAnimatorGameObjectContainerName = "UpperBody";
    private const string LowerAnimatorGameObjectContainerName = "LowerBody";

    // Start is called before the first frame update
    void Start()
    {
        upperAnimator = GameObject.Find($"Hero/{UpperAnimatorGameObjectContainerName}").GetComponentInChildren<Animator>();
        lowerAnimator = GameObject.Find($"Hero/{LowerAnimatorGameObjectContainerName}").GetComponentInChildren<Animator>();

        entityManager.AddComponentData(entity, new InputData { crouch = false, interact = false, look = Vector2.zero, movement = Vector2.zero, run = false });
        entityManager.AddComponentData(entity, new MoveData { direction = float3.zero, speed = 0 });
        entityManager.AddComponentData(entity, new FaceTowardsData { faceTowards = float3.zero });

        var lowerAnimationArchetype = entityManager.CreateArchetype(typeof(AnimationTriggerData), typeof(LowerAnimationTag), typeof(InputData));
        var upperAnimationArchetype = entityManager.CreateArchetype(typeof(AnimationTriggerData), typeof(UpperAnimationTag), typeof(InputData));

        upperAnimation = entityManager.CreateEntity(upperAnimationArchetype);
        entityManager.AddComponentData(upperAnimation, new AnimationTriggerData { name = AnimationTriggerNames.UpperIdle });
        entityManager.AddComponentData(upperAnimation, new UpperAnimationTag());
        entityManager.AddComponentData(upperAnimation, new InputData { crouch = false, interact = false, look = Vector2.zero, movement = Vector2.zero, run = false });

        lowerAnimation = entityManager.CreateEntity(lowerAnimationArchetype);
        entityManager.AddComponentData(lowerAnimation, new AnimationTriggerData { name = AnimationTriggerNames.UpperIdle });
        entityManager.AddComponentData(lowerAnimation, new LowerAnimationTag());
        entityManager.AddComponentData(lowerAnimation, new InputData { crouch = false, interact = false, look = Vector2.zero, movement = Vector2.zero, run = false });
    }

    // Update is called once per frame
    void Update()
    {
        var translation = entityManager.GetComponentData<Translation>(entity);
        var rotation = entityManager.GetComponentData<Rotation>(entity);
        transform.position = new Vector2(translation.Value.x, translation.Value.y);
        transform.rotation = rotation.Value;

        var upperAnimationData = entityManager.GetComponentData<AnimationTriggerData>(upperAnimation);
        var lowerAnimationData = entityManager.GetComponentData<AnimationTriggerData>(lowerAnimation);

        var upperAnimationName = Enum.GetName(typeof(AnimationTriggerNames), upperAnimationData.name);
        var lowerAnimationName = Enum.GetName(typeof(AnimationTriggerNames), lowerAnimationData.name);

        upperAnimator.SetTrigger(upperAnimationName);
        lowerAnimator.SetTrigger(lowerAnimationName);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (!enabled)
        {
            return;
        }
        this.entity = entity;
        entityManager = dstManager;
    }
}
