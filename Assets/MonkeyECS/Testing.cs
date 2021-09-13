using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
public class Testing : MonoBehaviour
{

    public Mesh mesh;
    public Material material;
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;


        EntityArchetype entityArchetype = entityManager.CreateArchetype(
                typeof(LevelComponent),
                typeof(Translation),
                typeof(RenderMesh), //in order to render
                typeof(LocalToWorld), //in order to view
                typeof(RenderBounds)
            );



        NativeArray<Entity> entityArray = new NativeArray<Entity>(1, Allocator.Temp);
        //Entity entity = entityManager.CreateEntity(typeof(LevelComponent));
        //Entity entity = entityManager.CreateEntity(entityArchetype);
        entityManager.CreateEntity(entityArchetype, entityArray);


        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelComponent { level = Random.Range(10, 20) });


            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh =  mesh,
                material = material
            });
        }
        entityArray.Dispose();
    }

}
