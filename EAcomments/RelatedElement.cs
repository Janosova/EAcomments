using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAcomments
{
    public class RelatedElement
    {
        public int connectorID { get; set; }
        public int connectedToID { get; set; }
        public string connectedToGUID { get; set; }

        public RelatedElement(int connectorID, int connetedToID, string connectedToGUID)
        {
            this.connectorID = connectorID;
            this.connectedToID = connetedToID;
            this.connectedToGUID = connectedToGUID;
        }
    }
}
