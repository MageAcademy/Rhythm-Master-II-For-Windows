using System.Collections.Generic;

public class MusicInformation
{
    public float DecisionTimeOffset;
    public List<int> IndexList = new List<int>();
    public float TimeIntervalPerBeat;
    public List<float> TimeList = new List<float>();

    public static MusicInformation Load(int musicIndex)
    {
        MusicInformation musicInformation = new MusicInformation();
        string[] rows = PreloadedResources.Instance.musicInformations[musicIndex].text.Split('\n');
        musicInformation.DecisionTimeOffset = float.Parse(rows[1].Split(',')[1]);
        musicInformation.TimeIntervalPerBeat = float.Parse(rows[2].Split(',')[1]);
        int rowCount = int.Parse(rows[0].Split(',')[1]);
        for (int a = 3; a < rowCount; ++a)
        {
            string[] columns = rows[a].Split(',');
            musicInformation.IndexList.Add(int.Parse(columns[0]));
            musicInformation.TimeList.Add(float.Parse(columns[1]));
        }

        return musicInformation;
    }
}