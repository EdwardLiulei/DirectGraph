using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
    public class DirectGraph
    {
        #region Field

        private List<DirectGraphNode> _nodeList;

        #endregion

        #region Constructor

        public DirectGraph(List<string> nodeList,List<List<string>> preNodesList,List<List<string>> nextNodesList)
        {
            _nodeList = new List<DirectGraphNode>();
            foreach (var nodeName in nodeList)
            {
                DirectGraphNode node = new DirectGraphNode(nodeName);
                _nodeList.Add(node);
            }

            for (int i = 0; i < nodeList.Count; i++)
            {
                if (preNodesList.Count >= i)
                {
                    List<string> preNodes = preNodesList[i];
                    foreach (var preNode in preNodes)
                    {
                        DirectGraphNode node = GetNodeByName(preNode);
                        if (node == null)
                            throw new Exception("Can not find Node: "+ preNode);
                        _nodeList[i].AddPre(node);
                    }
                }

                if (nextNodesList.Count >= i)
                {
                    List<string> nextNodes = nextNodesList[i];
                    foreach (var nextNode in nextNodes)
                    {
                        DirectGraphNode node = GetNodeByName(nextNode);
                        if (node == null)
                            throw new Exception("Can not find Node: " + nextNode);
                        _nodeList[i].AddNext(node);
                    }
                }
            }

            CalculateNumbers();
        }


        public DirectGraph(List<KeyValuePair<string,string>> edgeList)
        {
            _nodeList = new List<DirectGraphNode>();
            List<string> nodes = new List<string>();
            nodes.AddRange(edgeList.Select(p => p.Key));
            nodes.AddRange(edgeList.Select(p => p.Value));
            nodes.Distinct();

            foreach (var nodeName in nodes)
            {
                DirectGraphNode node = new DirectGraphNode(nodeName);
                _nodeList.Add(node);
            }

            foreach (KeyValuePair<string, string> pair in edgeList)
            {
                var keyNode = GetNodeByName(pair.Key);
                var valueNode = GetNodeByName(pair.Value);
                keyNode.AddNext(valueNode);
                valueNode.AddPre(keyNode);
            }

            CalculateNumbers();
        }
        #endregion


        public DirectGraphNode GetNodeByName(string name)
        {
            return _nodeList.Find(p => p.NodeName.Equals(name));
        }

        private void CalculateNumbers()
        {
            foreach (DirectGraphNode node in _nodeList)
            {
                if (node.InDegree == 0)
                {
                    node.FluxNumber = new Fraction(1, 1);
                    CalculateNumberForNode(node);
                }
            }
        }

        private void CalculateNumberForNode(DirectGraphNode node)
        {
            if (node.OutDegree == 0)
                return;
            foreach (DirectGraphNode nextNode in node.NextNodes)
            {
                nextNode.FluxNumber = nextNode.FluxNumber + node.FluxNumber.DivideBy((ulong)node.OutDegree);
                CalculateNumberForNode(nextNode);
            }
        }
    }
}
