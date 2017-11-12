using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
    public class DirectGraphNode
    {
        #region Field
        private string _nodeName;
        private List<DirectGraphNode> _preNodes;
        private List<DirectGraphNode> _nextNode;
        private Fraction _fluxNumber;
        #endregion

        #region Properity
        public string NodeName { get { return _nodeName; } }
        public DirectGraphNode[] PreNodes { get { return _preNodes.ToArray(); } }
        public DirectGraphNode[] NextNodes { get { return _nextNode.ToArray(); } }
        
        public int InDegree { get { return _preNodes.Count; } }

        public int OutDegree { get { return _nextNode.Count; } }

        public Fraction FluxNumber { set { _fluxNumber = value; } get { return _fluxNumber; } }

        #endregion

        #region Constructor
        public DirectGraphNode(string nodeName)
        {
            _nodeName = nodeName;
            _preNodes = new List<DirectGraphNode>();
            _nextNode = new List<DirectGraphNode>();
            _fluxNumber = new Fraction();

        }
        #endregion

        public void AddNext(DirectGraphNode node)
        {
            _nextNode.Add(node);
        }

        public void AddPre(DirectGraphNode node)
        {
            _preNodes.Add(node);
        }
    }
}
