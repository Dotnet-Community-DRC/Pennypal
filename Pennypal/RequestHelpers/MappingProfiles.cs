namespace Pennypal.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.Id, act
                => act.Ignore())
            .ForAllMembers(opts 
                => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateExpenseDto, Expense>().IncludeMembers(x => x.Category);
    }
}