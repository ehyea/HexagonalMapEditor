using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexagonal
{
    public class Cluster
    {
        [JsonProperty]
        public String id;
        [JsonProperty]
        public Dictionary<String,float> wordWeightMap;
        [JsonProperty]
        public Dictionary<String, List<String>> fileWordMap;
        [JsonProperty]
        public Dictionary<String,List<String>> previewMap;
        [JsonProperty]
        public Dictionary<String, float> reprezentativeWordsMap;
        [JsonProperty]
        public List<String> reprezentativeWords;
        [JsonProperty]
        public float queryScore;

        public string GetReprezentativeWord(int index)
        {
            if (reprezentativeWords!= null && index< reprezentativeWords.Count)
            {
                return reprezentativeWords[index];
            }
            return null;
        }
    }
}
