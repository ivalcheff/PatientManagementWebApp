using AutoMapper;

namespace PatientManagementApp.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);

    }
}
