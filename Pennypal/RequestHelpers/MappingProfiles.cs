namespace Pennypal.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.CategoryName,
            opt
                => opt.MapFrom(src => src.Category.Name));
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.Id, act
                => act.Ignore())
            .ForAllMembers(opts 
                => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Category, Expense>();
        CreateMap<CreateExpenseDto, Expense>();
    }
}