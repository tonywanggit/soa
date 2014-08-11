using JN.ESB.Core.Service.Common;

namespace JN.ESB.UDDI.Core.DataAccess
{
    partial class ServiceDirectoryDCDataContext
    {
        public ServiceDirectoryDCDataContext() :
            base(EsbConfig.getConnStringByDBName("ServiceDirectoryDb"), mappingSource)
        {
            OnCreated();
        }
    }
}