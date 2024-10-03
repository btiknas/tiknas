using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Tiknas.Timing;

namespace Tiknas.AspNetCore.Mvc.ModelBinding;

public class TiknasDateTimeModelBinder : IModelBinder
{
    private readonly DateTimeModelBinder _dateTimeModelBinder;
    private readonly IClock _clock;

    public TiknasDateTimeModelBinder(IClock clock, DateTimeModelBinder dateTimeModelBinder)
    {
        _clock = clock;
        _dateTimeModelBinder = dateTimeModelBinder;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        await _dateTimeModelBinder.BindModelAsync(bindingContext);
        if (bindingContext.Result.IsModelSet && bindingContext.Result.Model is DateTime dateTime)
        {
            bindingContext.Result = ModelBindingResult.Success(_clock.Normalize(dateTime));
        }
    }
}
