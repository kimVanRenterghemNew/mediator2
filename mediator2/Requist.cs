namespace mediator2;

public interface Requist<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}


public interface RequistValidator<in TQuery>
{
    bool Handle(TQuery query);
}