namespace Pennypal.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.CategoryName,
            opt
                => opt.MapFrom(src => src.Category.Name));
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>();
        CreateMap<Category, UpdateCategoryDto>()
            .ForMember(dest => dest.Id, act
                => act.Ignore())
            .ForAllMembers(opts 
                => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Category, Expense>();
        CreateMap<Expense, CreateExpenseDto>();
        CreateMap<Expense, UpdateExpenseDto>()
            .ForMember(dest => dest.Id, exp
                => exp.Ignore())
            .ForMember(des => des.Category, exp 
                => exp.Ignore());
    }
}