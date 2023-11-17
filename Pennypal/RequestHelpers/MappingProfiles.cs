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
        CreateMap<Expense, CreateExpenseDto>()
            .ForMember(dest => dest.CategoryName, opt 
                => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CurrencyCode, opt 
                => opt.MapFrom(src => src.Currency.ToString()))
            .ForMember(dest => dest.StatusCode, opt 
                => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<Expense, UpdateExpenseDto>()
            .ForMember(dest => dest.CategoryName, opt 
                => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CurrencyCode, opt 
                => opt.MapFrom(src => src.Currency.ToString()))
            .ForMember(dest => dest.StatusCode, opt 
                => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateExpenseDto, Expense>()
            .ForMember(dest => dest.Currency, opt
                => opt.MapFrom(src => Enum.Parse(typeof(Currency), src.CurrencyCode)))
            .ForMember(dest => dest.Status, opt
                => opt.MapFrom(src => Enum.Parse(typeof(Status), src.StatusCode)));
        CreateMap<UpdateExpenseDto, Expense>()
            .ForMember(dest => dest.Currency, opt
                => opt.MapFrom(src => Enum.Parse(typeof(Currency), src.CurrencyCode)))
            .ForMember(dest => dest.Status, opt 
                => opt.MapFrom(src => Enum.Parse(typeof(Status), src.StatusCode)));
    }
}