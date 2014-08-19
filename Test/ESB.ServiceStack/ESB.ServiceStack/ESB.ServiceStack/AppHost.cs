using Funq;
using ServiceStack;
using ESB.ServiceStack.ServiceInterface;

namespace ESB.ServiceStack
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("ESB.ServiceStack", typeof(MyServices).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.AddPlugin(new PostmanFeature());
            //this.AddPlugin(new CorsFeature());
        }
    }
}