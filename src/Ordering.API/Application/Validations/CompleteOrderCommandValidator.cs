namespace eShop.Ordering.API.Application.Validations;

public class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
{
    public CompleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Geçersiz sipariş numarası girildi.");
        
        //Bütün kurallar eklenebilir.
    }
}
