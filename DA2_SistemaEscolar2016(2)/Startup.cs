using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DA2_SistemaEscolar2016_2_.Startup))]
namespace DA2_SistemaEscolar2016_2_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
