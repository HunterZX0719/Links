using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class FlowerSpriteManager : Singleton<FlowerSpriteManager>
{
    public List<Sprite> flowerSprites = new List<Sprite>();

    public List<Sprite> GetRandomList(int count)
    {
        List<Sprite> result = new List<Sprite>();
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            result.Add(flowerSprites[random.NextInt(flowerSprites.Count - 1)]);
        }
        return result;
    }
    
    public List<Sprite> GetUnrepeatedRandomList(int count)
    {
        List<Sprite> result = new List<Sprite>();
        while (result.Count < count)
        {
            var sprite = flowerSprites[UnityEngine.Random.Range(0, flowerSprites.Count - 1)];
            if (!result.Contains(sprite))
            {
                result.Add(sprite);
            }
        }
        return result;
    }
}
