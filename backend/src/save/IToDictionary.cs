using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriApp.Save;

public interface ISaveObject
{
    public Dictionary<string, string> ToDictionary();
    public void FromDictionary(Dictionary<string, string> data);
}