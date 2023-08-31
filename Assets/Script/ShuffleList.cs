using System.Collections.Generic;

public abstract class SuffleList
{
    public static List<E> ShuffleListItems<E>(List<E> inputlist)
    {
        List<E> originalList = new List<E>();   
        originalList.AddRange(inputlist);
        List<E> randomList = new List<E>();
        System.Random r = new System.Random();
        int randomIndex = 0;
        while (originalList.Count > 0)
        {
            randomIndex = r.Next(originalList.Count);
            randomList.Add(originalList[randomIndex]);
            originalList.RemoveAt(randomIndex);
        }
        return randomList;
    }
}