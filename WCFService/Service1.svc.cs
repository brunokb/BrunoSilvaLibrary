using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ModelService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ModelService.svc or ModelService.svc.cs at the Solution Explorer and start debugging.
    public class ModelService : IModelService
    {
      
        public List<int> GetList()
        {
            var li = new List<int>();
            li.Add(2);
            li.Add(4);
            li.Add(4);
            li.Add(5);
            li.Add(6);
            return li;
        }
    }
}
