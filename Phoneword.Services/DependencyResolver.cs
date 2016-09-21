using Phoneword.IocResolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.Services
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductService, ProductService>();
            registerComponent.RegisterType<IUserService, UserService>();
            registerComponent.RegisterType<ITokenService, TokenService>();
        }
    }
}
