using HyperRPG.Engine.Visual;
using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour
{
    public PlayerController player;

    [Header("Wartosci")]
    [SerializeField] private int level;
    public int currentExp;
    public int expToNextLevel;
    public int globalExp;

    public int Level { get { return level; } private set { level = value; } }
    public int CurrentExp {  get { return currentExp; } private set { currentExp = value; } }
    public int ExpToNextLevel { get { return expToNextLevel; } private set { expToNextLevel = value; } }

    public void AddExp(int amount)
    {
        globalExp += amount;
        CurrentExp += amount;

        Popup.Create(transform.position, amount.ToString(), Color.yellow, transform);

        while (CurrentExp >= ExpToNextLevel)
        {
            StartCoroutine(OnLevelUp()); 
            CurrentExp -= ExpToNextLevel;
            ExpToNextLevel += Mathf.RoundToInt(ExpToNextLevel * 0.23f); 
        }

        //update hud

    }

    public IEnumerator OnLevelUp()
    {
        Level++;
        Popup.Create(transform.position, "LEVEL UP!", Color.red, transform, 7);
        yield return new WaitForSeconds(0.2f);
        Popup.Create(transform.position, "LEVEL UP!", Color.yellow, transform, 7);
        yield return new WaitForSeconds(0.2f);
        Popup.Create(transform.position, "LEVEL UP!", Color.red, transform, 7);
    }


}


// exp bedzie naliczany przez sztywna wartosc expa za kazdego moba * expRate, ktory bedzie sie zwiekszal z kazdym lvlem?

// private float expRate;

// albo exp normalnie, tzn: za kazdego zabitego moba sztywna wartosc expa, po prostu z kazdym lvlem bedzie zwiekszac sie 
// wartosc jaka trzeba wypelnic, aby wbic nastepny lvl, tylko wykminic w jaki sposob ta wartosc ma sie zwiekszac

// wtedy dodaæ metode, która po inkrementacji lvla bêdzie siê wykonywaæ i bedzie zwiekszac expToNextLevel o jakas wartosc
