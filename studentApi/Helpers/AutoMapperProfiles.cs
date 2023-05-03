using AutoMapper;
using studentApi.Models;

namespace studentApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GetStudentDetailResult, StudentDetail>();
        }
    }
}