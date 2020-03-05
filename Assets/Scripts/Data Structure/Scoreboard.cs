public class Scoreboard
{
    public static Scoreboard Instance = new Scoreboard();

    public int BadCount;
    public int GreatCount;
    public int MissCount;
    public int PerfectCount;

    public int this[int index]
    {
        get
        {
            if (index == 0)
            {
                return BadCount;
            }
            else if (index == 1)
            {
                return GreatCount;
            }
            else if (index == 2)
            {
                return MissCount;
            }
            else
            {
                return PerfectCount;
            }
        }

        set
        {
            if (index == 0)
            {
                BadCount = value;
            }
            else if (index == 1)
            {
                GreatCount = value;
            }
            else if (index == 2)
            {
                MissCount = value;
            }
            else
            {
                PerfectCount = value;
            }
        }
    }
}