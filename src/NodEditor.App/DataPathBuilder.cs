using System;
using System.Collections.Generic;
using System.Linq;
using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class DataPathBuilder
    {
        // public List<DataPath> CreateDataPath(IFlowNode flowNode)
        // {
        //     var dataPaths = new Dictionary<Guid, DataPath>();
        //
        //     for (int i = 0; i < flowNode.Inputs.Count; i++)
        //     {
        //         CreateDataPath(flowNode.Inputs[i].Node, dataPaths);
        //     }
        //     
        //     return dataPaths.Values.ToList();
        // }
        //
        // private void CreateDataPath(INode dataNode, Dictionary<Guid, DataPath> dataPaths)
        // {
        //     if (dataPaths.ContainsKey(dataNode.Guid))
        //     {
        //         return;
        //     }
        //
        //     var dataPath = new DataPath(dataNode.Guid);
        //     
        //     dataPaths.Add(dataPath.Guid, dataPath);
        // }
    }
}