namespace mediator2;

public class MediatorFlow< TQuery,  TResult> : Requist<TQuery, TResult>
{
    private readonly RequistValidator<TQuery> _validator;
    private readonly Requist<TQuery, TResult> _handler;

    public MediatorFlow(RequistValidator<TQuery> validator, Requist<TQuery, TResult> handler)
    {
        _validator = validator;
        _handler = handler;
    }

    public TResult Handle(TQuery query)
    {
       
        if ( !_validator.Handle(query))
        {
            throw new Exception("Validation failed");
        }
        return _handler.Handle(query);
    }
}