using System;
using System.Collections.Generic;
using System.Text;

namespace JN.ESB.Core.Service.Common
{
    [Serializable]
    public class UDDI对象
    {
        private string _Url;
        private int _ServiceStatus;
        private int _ServiceType;
        private string _MethodName;
        private Guid _TemplateID;
        private string _PersonalName;
        private string _PersonalMail;
        private Guid _ServiceID;

        public string Url
        {
            get
            {
                //throw new System.NotImplementedException();
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }

        public int ServiceStatus
        {
            get
            {
                //throw new System.NotImplementedException();
                return _ServiceStatus;
            }
            set
            {
                _ServiceStatus = value;
            }
        }

        public int ServiceType
        {
            get
            {
                //throw new System.NotImplementedException();
                return _ServiceType;
            }
            set
            {
                _ServiceType = value;
            }
        }

        public string MethodName
        {
            get
            {
                //throw new System.NotImplementedException();
                return _MethodName;
            }
            set
            {
                _MethodName = value;
            }
        }

        public Guid ServiceID
        {
            get
            {
                return _ServiceID;
            }
            set
            {
                _ServiceID = value;
            }
        }

        public Guid TemplateID
        {
            get
            {
                return _TemplateID;
            }
            set
            {
                _TemplateID = value;
            }
        }

        public string PersonalName
        {
            get
            {
                return _PersonalName;
            }
            set
            {
                _PersonalName = value;
            }
        }

        public string PersonalMail
        {
            get
            {
                return _PersonalMail;
            }
            set
            {
                _PersonalMail = value;
            }
        }

        /// <summary>
        /// 如果服务状态为停用的话，则将服务地址指向平台内部地址
        /// </summary>
        public void UpdateStatus()
        {
            if (_ServiceStatus == (int)服务状态.停用)
            {
                _MethodName = "CallMethod";
                _Url = "http://localhost/EsbSampleService/SampleWebService.asmx";

                if (_ServiceType == (int)服务类型.WCF)
                {
                    _Url = "http://localhost:8080/EsbWCFService/SampleWcfService.svc";
                }
            }
        }

        
    }
}
