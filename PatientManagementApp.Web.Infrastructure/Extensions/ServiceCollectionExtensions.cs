using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository;
using PatientManagementApp.Data.Repository.Interfaces;


namespace PatientManagementApp.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            //use user manager instead
            Type[] typesToExclude = new Type[] { typeof(ApplicationUser) };

            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                            !t.Name.ToLower().EndsWith("attribute"))
                .ToArray();

            foreach (Type type in modelTypes)
            {
                if (!typesToExclude.Contains(type))
                {
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType = typeof(BaseRepository<,>);

                    //get only the properties with name ID 
                    PropertyInfo? idPropInfo = type
                        .GetProperties()
                        .SingleOrDefault(p => p.Name.ToLower() == "id");

                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;

                    if (idPropInfo == null)
                    {
                        constructArgs[1] = typeof(object);
                    }
                    else
                    {
                        constructArgs[1] = idPropInfo.PropertyType;
                    }

                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }
        }
    }
}
