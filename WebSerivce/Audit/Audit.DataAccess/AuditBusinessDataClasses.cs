using JN.ESB.Core.Service.Common;
namespace JN.ESB.Audit.DataAccess
{
    partial class AuditBusinessDataClassesDataContext
    {
        public AuditBusinessDataClassesDataContext() :
            base(EsbConfig.getConnStringByDBName("EsbAuditDB"), mappingSource)
        {
            OnCreated();
        }
    }
}
