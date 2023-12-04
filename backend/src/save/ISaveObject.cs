using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriApp.Save;

public interface ISaveObject
{
    /// <summary>
    /// converts the object to a dictionary of type {variable name}, {value}
    /// </summary>
    /// <returns>dictionary of data</returns>
    public Dictionary<string, string> ToDictionary();
    
    /// <summary>
    /// sets the object values with given the data. Casting is done in here so each object can cast itself.
    /// </summary>
    /// <param name="data">dictionary of type {variable name}, {value}</param>
    public void FromDictionary(Dictionary<string, string> data);
}